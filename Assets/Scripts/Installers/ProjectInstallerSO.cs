using UnityEngine;
using UserControlSystem;
using Utils;
using Zenject;


[CreateAssetMenu(fileName = "ProjectInstallerSO", menuName = "RTS/Installers/ProjectInstallerSO")]
public sealed class ProjectInstallerSO : ScriptableObjectInstaller<ProjectInstallerSO>
{
    [SerializeField] private AssetsContext _legacyContext;

    [SerializeField] private AttackerValue _attackableleValue;
    [SerializeField] private DamagableValue _damagableValue;
    [SerializeField] private SelectableValue _selectableleValue;
    [SerializeField] private Vector3Value _vector3Value;

    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        Container.Bind<Vector3Value>().FromInstance(_vector3Value);
        Container.Bind<DamagableValue>().FromInstance(_damagableValue);
        Container.Bind<AttackerValue>().FromInstance(_attackableleValue);
        Container.Bind<SelectableValue>().FromInstance(_selectableleValue);
    }
}
