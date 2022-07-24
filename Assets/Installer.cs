using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SmoothJump>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveLevel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GenerationLevel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<RotateCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DifficultyManager>().FromComponentInHierarchy().AsSingle();
    }
}