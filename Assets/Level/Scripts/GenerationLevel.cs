using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenerationLevel : MonoBehaviour
{
    private DiContainer container;
    private MoveLevel moveLevel;

    private System.Random random;

    [SerializeField]
    private List<GameObject> branches = new List<GameObject>();

    public void Generate()
    {
        var randomIndex = random.Next(branches.Count);
        var branch = container.InstantiatePrefab(branches[randomIndex], transform);

        var newPosition = new Vector3(-5, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;

        moveLevel.Branches.Add(branch.transform);
    }

    private void Start()
    {
        random = new System.Random();
    }

    [Inject]
    private void Init(MoveLevel moveLevel, DiContainer container)
    {
        this.moveLevel = moveLevel;
        this.container = container;
    }
}
