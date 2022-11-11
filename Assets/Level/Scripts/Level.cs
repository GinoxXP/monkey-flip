using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    private LevelGenerator generationLevel;
    private DifficultyManager difficultyManager;

    [SerializeField]
    private Transform firstSegment;

    /// <summary>
    /// Segment collection. The newer the segment, the lower the index.
    /// </summary>
    public List<Transform> Segments { get; set; } = new List<Transform>();

    public Transform LastCreatedSegment => Segments.FirstOrDefault();

    public Transform CurrentSegment { get; set; }

    public Transform NextSegment
    {
        get
        {
            if (Segments.Count < 2)
                return null;

            var currentIndex = Segments.FindIndex(x => x == CurrentSegment);

            if (currentIndex < 1)
                return null;

            return Segments[currentIndex - 1];
        }
    }

    public void DestroySegment(Transform transform)
    {
        Segments.Remove(transform);
        Destroy(transform.gameObject);
    }

    public void CreateSegment()
    {
        generationLevel.Generate();
        difficultyManager.AddDifficulty();
    }

    private void Start()
    {
        Segments.Add(firstSegment);
    }

    [Inject]
    private void Init(
        LevelGenerator generationLevel,
        DifficultyManager difficultyManager)
    {
        this.generationLevel = generationLevel;
        this.difficultyManager = difficultyManager;
    }
}
