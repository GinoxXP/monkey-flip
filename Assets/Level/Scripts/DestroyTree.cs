using UnityEngine;
using Zenject;

public class DestroyTree : MonoBehaviour
{
    private GenerationLevel generationLevel;
    private MoveLevel moveLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Segment>(out _))
        {
            generationLevel?.Generate();
            moveLevel.Branches.Remove(other.transform);
            Destroy(other.gameObject);
        }
    }

    [Inject]
    private void Init(
        GenerationLevel generationLevel,
        MoveLevel moveLevel)
    {
        this.generationLevel = generationLevel;
        this.moveLevel = moveLevel;
    }
}
