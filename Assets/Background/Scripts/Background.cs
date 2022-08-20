using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float xMin;

    public List<Transform> Transforms => transforms;
    public float XMax => xMax;
    public float XMin => xMin;

}
