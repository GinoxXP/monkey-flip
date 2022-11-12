using System;
using UnityEngine;
using Zenject;

public class PerlinNoiseGeneration : MonoBehaviour
{
    private DiContainer container;
    private DifficultyManager difficultyManager;
    private Level level;
    private ScoreManager scoreManager;
    private System.Random random;

    [SerializeField]
    private float xScale;
    [SerializeField]
    private float yScale;
    [SerializeField]
    private float levelSegmentLineY;
    [Space]
    [SerializeField]
    private GameObject segmentPrefab;
    [SerializeField]
    private Transform segmentsParent;
    [SerializeField]
    private float segmentDistance;
    [SerializeField]
    private AnimationCurve maxRandomDistanceOffsetCurve;

    public int Seed { get; private set; }

    public void SetSeed(int? seed = null)
    {
        if (seed.HasValue)
        {
            seed = seed.Value;
        }
        else
        {
            double ticks = DateTime.Now.TimeOfDay.Ticks;
            seed = (int)Math.Pow(ticks, 0.5);
        }

        Seed = seed.Value;

        random = new System.Random(Seed);
    }

    public Transform Generate()
    {
        return CreateSegment(segmentDistance);
    }

    private Transform CreateSegment(float distance)
    {
        var segment = container.InstantiatePrefab(segmentPrefab, segmentsParent);

        var randomOffset = (float)random.NextDouble() * maxRandomDistanceOffsetCurve.Evaluate(difficultyManager.Difficulty);
        var lastPosition = level.LastCreatedSegment.position;

        var newPosition = new Vector3(lastPosition.x - distance - randomOffset, segment.transform.position.y, segment.transform.position.z);
        segment.transform.position = newPosition;

        var segmentComponent = segment.GetComponent<Segment>();

        var branchHeight = levelSegmentLineY + GetBranchHeight(scoreManager.Score);
        segmentComponent.Branch.SetHeight(branchHeight);

        return segment.transform;
    }

    private float GetBranchHeight(int x)
    {
        float height = yScale * (Mathf.PerlinNoise(x * xScale, Seed) - 0.5f);
        return height;
    }

    [Inject]
    private void Init(
        DiContainer container,
        DifficultyManager difficultyManager,
        Level level,
        ScoreManager scoreManager)
    {
        this.container = container;
        this.difficultyManager = difficultyManager;
        this.level = level;
        this.scoreManager = scoreManager;
    }
}
