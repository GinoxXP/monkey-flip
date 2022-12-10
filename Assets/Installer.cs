using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField]
    private SkinData skinData;
    [SerializeField]
    private Light directionLight;
    [SerializeField]
    private new Camera camera;

    public override void InstallBindings()
    {
        Container.Bind<SmoothJump>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveLevel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DifficultyManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Score>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BananaView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BananaBalance>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PauseController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SkinController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DayCycleController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Level>().FromComponentInHierarchy().AsSingle();
        Container.Bind<JumpAssistant>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PerlinNoiseGeneration>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LootCreation>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Monkey>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ReloadScene>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Yandex>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Light>().FromInstance(directionLight).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();

        Container.Bind<SkinData>().FromScriptableObject(skinData).AsSingle();
    }
}