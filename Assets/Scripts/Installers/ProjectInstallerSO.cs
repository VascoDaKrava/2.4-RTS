using Abstractions;
using UnityEngine;
using UserControlSystem;
using Zenject;


[CreateAssetMenu(fileName = "ProjectInstallerSO", menuName = "RTS/Installers/ProjectInstallerSO")]
public sealed class ProjectInstallerSO : ScriptableObjectInstaller<ProjectInstallerSO>
{
    [SerializeField] private AttackerValue _attackableleValue;
    [SerializeField] private DamagableValue _damagableValue;
    [SerializeField] private SelectableValue _selectableleValue;
    [SerializeField] private Vector3Value _vector3Value;

    public override void InstallBindings()
    {
        Container
            .Bind<Vector3Value>()
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
            .Bind<IAwaitable<IAttackable>>()
            .FromInstance(_attackableleValue);

        Container
            .Bind<IAwaitable<Vector3>>()
            .FromInstance(_vector3Value);
    }
}
