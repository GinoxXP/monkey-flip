using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField]
    private SkinData skinData;

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
        Container.Bind<SkinController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<SkinData>().FromScriptableObject(skinData).AsSingle();
    }
}