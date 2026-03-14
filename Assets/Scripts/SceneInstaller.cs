using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Battlefield _battlefieldPrefab;
    [SerializeField] private InputActionAsset _inputActionAsset;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BattleController>()
            .AsSingle()
            .WithArguments(_inputActionAsset)
            .NonLazy();

        Container.Bind<Battlefield>()
            .FromComponentInNewPrefab(_battlefieldPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<ChessValidator>()
            .AsSingle()
            .NonLazy();

        Container.Bind<Camera>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}
