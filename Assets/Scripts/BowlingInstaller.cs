using Zenject;
using UnityEngine;

public class BowlingInstaller : MonoInstaller
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _tossPoint;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Pin[] _pins;
    [SerializeField] private TossBall _tossBall;

    public override void InstallBindings()
    {
        Container.Bind<Transform>()
        .FromInstance(_tossPoint)
        .WhenInjectedInto<Ball>();

        Container.Bind<Ball>()
            .FromInstance(_ball)
            .AsSingle();

        Container.Bind<PinManager>()
        .AsSingle()
        .WithArguments(_pins)
        .NonLazy();

        Container.Bind<Camera>()
        .FromInstance(_mainCamera)
        .AsSingle();

        Container.Bind<TossBall>()
            .FromInstance(_tossBall)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<GameLoop>()
            .AsSingle()
            .NonLazy();

        Container.Bind<ScoreManager>()
        .AsSingle()
        .NonLazy();
    }
}
