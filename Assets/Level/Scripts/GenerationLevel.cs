using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenerationLevel : MonoBehaviour
{
    private MoveLevel moveLevel;
    private System.Random random;

    [SerializeField]
    private List<GameObject> branches = new List<GameObject>();

    public void Generate()
    {
        var randomIndex = random.Next(branches.Count);
        var branch = Instantiate(branches[randomIndex]);

        branch.GetComponentInChildren<Branch>().GenerationLevel = this;

        var newPosition = new Vector3(-5, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;
        branch.transform.parent = transform;

        moveLevel.Branches.Add(branch.transform);
    }

    private void Start()
    {
        random = new System.Random();
        Generate();
    }

    [Inject]
    private void Init(MoveLevel moveLevel)
    {
        this.moveLevel = moveLevel;
    }
}
