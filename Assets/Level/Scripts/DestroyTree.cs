using UnityEngine;
using Zenject;

public class DestroyTree : MonoBehaviour
{
    private GenerationLevel generationLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tree>(out _))
        {
            generationLevel?.Generate();
            Destroy(other.gameObject);
        }
    }

    [Inject]
    private void Init(
        GenerationLevel generationLevel)
    {
        this.generationLevel = generationLevel;
    }
}
