using Abstractions;
using System;
using UnityEngine;
using UserControlSystem;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "ProjectInstallerSO", menuName = "RTS/Installers/ProjectInstallerSO")]
    public sealed class ProjectInstallerSO : ScriptableObjectInstaller<ProjectInstallerSO>
    {
        [SerializeField] private AttackerValue _attackableleValue;
        [SerializeField] private DamagableValue _damagableValue;
        [SerializeField] private SelectableValue _selectableleValue;
        [SerializeField] private GroundPointValue _vector3Value;

        public override void InstallBindings()
        {
            Container
                .Bind<GroundPointValue>()
                .FromInstance(_vector3Value);

            Container
                .Bind<DamagableValue>()
                .FromInstance(_damagableValue);

            Container
                .Bind<AttackerValue>()
                .FromInstance(_attackableleValue);

            Container
                .Bind<SelectableValue>()
                .FromInstance(_selectableleValue);

            Container
                .Bind<IAwaitable<IDamagable>>()
                .FromInstance(_damagableValue);

            Container
                .Bind<IAwaitable<IAttacker>>()
                .FromInstance(_attackableleValue);

            Container
                .Bind<IAwaitable<Vector3>>()
                .FromInstance(_vector3Value);

            Container
                .Bind<IObservable<ISelectable>>()
                .FromInstance(_selectableleValue);
        }
    }
}