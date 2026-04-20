using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CharacterStateMachine _characterStateMachine;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private UI _ui;

    public override void InstallBindings()
    {
        Container.Bind<Idle>().AsSingle();
        Container.Bind<Search>().AsSingle();
        Container.Bind<Collect>().AsSingle();
        Container.Bind<Return>().AsSingle();

        Container.Bind<CharacterStateMachine>().FromInstance(_characterStateMachine).AsSingle();
        Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle();
        Container.Bind<UI>().FromInstance(_ui).AsSingle();
    }

}
