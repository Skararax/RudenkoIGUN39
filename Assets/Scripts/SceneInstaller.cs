using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private BattleController _battleControllerPrefab;
    [SerializeField] private Battlefield _battlefieldPrefab;
    [SerializeField] private ChessValidator _chessValidatorPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BattleController>()
            .FromComponentInNewPrefab(_battleControllerPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<Battlefield>()
            .FromComponentInNewPrefab(_battlefieldPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<ChessValidator>()
            .FromComponentInNewPrefab(_chessValidatorPrefab)
            .AsSingle();

        Container.Bind<Camera>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}
