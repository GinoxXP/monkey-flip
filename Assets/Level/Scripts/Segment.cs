using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private Branch branch;

    public double RandomValue { get; set; }

    public Branch Branch => branch;
}
