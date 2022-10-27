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

    private void Start()
    {
        Segments.Add(firstSegment);
    }
}
