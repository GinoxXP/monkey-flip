using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
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

    private void Start()
    {
        Segments.Add(firstSegment);
    }
}
