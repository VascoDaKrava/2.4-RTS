using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Commands;
using UniRx;
using UnityEngine;
using UserControlSystem.UI.Model;
using UserControlSystem.UI.View;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public sealed class BottomRightPresenter : MonoBehaviour, ICommandButtonsPresenter
    {
        [SerializeField] private CommandButtonsView _view;

        [Inject] private IObservable<ISelectable> _selectable;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        public Subject<bool> IsCommandPending => _model.IsCommandPending;

        private void Start()
        {
            _view.Clear();
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.Subscribe(OnSelected);
        }

        private void OnDestroy()
        {
            _view.OnClick -= _model.OnCommandButtonClicked;
            _model.OnCommandSent -= _view.UnblockAllInteractions;
            _model.OnCommandCancel -= _view.UnblockAllInteractions;
            _model.OnCommandAccepted -= _view.BlockInteractions;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }

            _currentSelectable = selectable;

            _view.Clear();

            if (selectable != null)
            {
                _view.MakeLayout(
                    new List<ICommandExecutor<ICommand>>((selectable as Component)
                    .GetComponentsInParent<ICommandExecutor<ICommand>>())
                    );
            }
        }
    }
}