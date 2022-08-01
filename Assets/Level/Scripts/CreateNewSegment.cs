using UnityEngine;
using Zenject;

public class CreateNewSegment : MonoBehaviour
{
    private GenerationLevel generationLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SmoothJump>(out _))
        {
            generationLevel?.Generate();
        }
    }

    [Inject]
    private void Init(
        GenerationLevel generationLevel)
    {
        this.generationLevel = generationLevel;
    }
}
