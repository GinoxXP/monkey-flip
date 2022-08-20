using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;
    [SerializeField]
    private Vector3 maxPosition;
    [SerializeField]
    private Vector3 minPosition;

    public List<Transform> Transforms => transforms;
    public Vector3 MaxPosition => maxPosition;
    public Vector3 MinPosition => minPosition;

}
