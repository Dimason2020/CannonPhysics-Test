using QFSW.MOP2;
using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    [SerializeField] private MasterObjectPooler masterObjectPooler;
    [SerializeField] private InputHandler inputHandler;

    public override void InstallBindings()
    {
        Container.Bind<MasterObjectPooler>().FromInstance(masterObjectPooler);
        Container.Bind<InputHandler>().FromInstance(inputHandler);
    }
}