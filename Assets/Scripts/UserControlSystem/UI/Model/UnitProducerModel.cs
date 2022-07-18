using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public sealed class UnitProducerModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }

        [Inject]
        public void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected
                .Select(selectable => selectable as Component)
                .Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}