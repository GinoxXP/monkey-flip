using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GenerationLevel : MonoBehaviour
{
    private MoveLevel moveLevel;

    [SerializeField]
    private List<GameObject> branches = new List<GameObject>();

    public void Generate()
    {
        var randomIndex = Random.Range(0, branches.Count - 1);
        var branch = Instantiate(branches[randomIndex]);

        branch.GetComponent<Branch>().GenerationLevel = this;

        var newPosition = new Vector3(-5, branch.transform.position.y, branch.transform.position.z);
        branch.transform.position = newPosition;
        branch.transform.parent = transform;

        moveLevel.Branches.Add(branch.transform);
    }

    private void Start()
    {
        Generate();
    }

    [Inject]
    private void Init(MoveLevel moveLevel)
    {
        this.moveLevel = moveLevel;
    }
}
