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
    private List<GameObject> segmentsPrefab = new List<GameObject>();
    [SerializeField]
    private float generationDistance;
    [SerializeField]
    private AnimationCurve maxRandomOffsetCurve;
    [SerializeField]
    private Transform branchesParent;

    public void Generate(float? distance = null)
    {
        if(distance.HasValue)
            CreateBranch(distance.Value);
        else
            CreateBranch(generationDistance * 2);
    }

    private void CreateBranch(float distance)
    {
        var randomIndex = random.Next(segmentsPrefab.Count);
        var branch = container.InstantiatePrefab(segmentsPrefab[randomIndex], branchesParent);

        var randomOffset = (float)(random.NextDouble() - 0.5f) * maxRandomOffsetCurve.Evaluate(difficultyManager.Difficulty);
        var newPosition = new Vector3(-distance + randomOffset, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;

        moveLevel.Segments.Add(branch.transform);
    }

    private void Start()
    {
        random = new System.Random();
        Generate(generationDistance);
    }

    [Inject]
    private void Init(MoveLevel moveLevel, DiContainer container, DifficultyManager difficultyManager)
    {
        this.moveLevel = moveLevel;
        this.container = container;
        this.difficultyManager = difficultyManager;
    }
}
