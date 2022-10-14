using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Background : MonoBehaviour
{
    private DayCycleController dayCycleController;

    [SerializeField]
    private List<Transform> transforms;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private int depthLevel;

    public List<Transform> Transforms => transforms;
    public float XMax => xMax;
    public float XMin => xMin;

    private List<SpriteRenderer> spriteRenderers = new();

    private void OnTimeCycleChanged(List<Color> colors)
    {
        var color = colors[depthLevel];

        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
        }
    }

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();

        dayCycleController.TimeCycleChange += OnTimeCycleChanged;
    }

    [Inject]
    private void Init(DayCycleController dayCycleController)
    {
        this.dayCycleController = dayCycleController;
    }
}
