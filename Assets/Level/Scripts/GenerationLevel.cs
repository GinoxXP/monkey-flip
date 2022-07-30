using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenerationLevel : MonoBehaviour
{
    private DiContainer container;
    private MoveLevel moveLevel;
    private DifficultyManager difficultyManager;

    private System.Random random;

    [SerializeField]
    private List<GameObject> branchPrefabs = new List<GameObject>();
    [SerializeField]
    private int generationDistance;
    [SerializeField]
    private AnimationCurve maxRandomOffsetCurve;
    [SerializeField]
    private Transform branchesParent;

    public void Generate(int? distance = null)
    {
        if(distance.HasValue)
            CreateBranch(distance.Value);
        else
            CreateBranch(generationDistance * 2);
    }

    private void CreateBranch(int distance)
    {
        var randomIndex = random.Next(branchPrefabs.Count);
        var branch = container.InstantiatePrefab(branchPrefabs[randomIndex], branchesParent);

        var randomOffset = (float)(random.NextDouble() - 0.5f) * maxRandomOffsetCurve.Evaluate(difficultyManager.Difficulty);
        var newPosition = new Vector3(-distance + randomOffset, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;

        moveLevel.Branches.Add(branch.transform);
    }

    private void Start()
    {
        random = new System.Random();
        Generate(generationDistance);
        Generate(generationDistance * 2);
    }

    [Inject]
    private void Init(MoveLevel moveLevel, DiContainer container, DifficultyManager difficultyManager)
    {
        this.moveLevel = moveLevel;
        this.container = container;
        this.difficultyManager = difficultyManager;
    }
}
