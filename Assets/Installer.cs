using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SmoothJump>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveLevel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GenerationLevel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DifficultyManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BananaView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BananaBalanceManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PauseController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SkinManager>().FromComponentInHierarchy().AsSingle();
    }
}