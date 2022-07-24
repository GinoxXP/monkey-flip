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
    private List<GameObject> branches = new List<GameObject>();
    [SerializeField]
    private AnimationCurve maxRandomOffsetCurve;

    public void Generate()
    {
        var randomIndex = random.Next(branches.Count);
        var branch = container.InstantiatePrefab(branches[randomIndex], transform);

        var randomOffset = (float) (random.NextDouble() - 0.5f) * maxRandomOffsetCurve.Evaluate(difficultyManager.Difficulty);
        var newPosition = new Vector3(-5 + randomOffset, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;

        moveLevel.Branches.Add(branch.transform);
    }

    private void Start()
    {
        random = new System.Random();
    }

    [Inject]
    private void Init(MoveLevel moveLevel, DiContainer container, DifficultyManager difficultyManager)
    {
        this.moveLevel = moveLevel;
        this.container = container;
        this.difficultyManager = difficultyManager;
    }
}
