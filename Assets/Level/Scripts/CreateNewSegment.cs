using UnityEngine;
using Zenject;

public class CreateNewSegment : MonoBehaviour
{
    private GenerationLevel generationLevel;
    private DifficultyManager difficultyManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SmoothJump>(out _))
        {
            generationLevel?.Generate();
            difficultyManager.AddDifficulty();
        }
    }

    [Inject]
    private void Init(
        GenerationLevel generationLevel,
        DifficultyManager difficultyManager)
    {
        this.generationLevel = generationLevel;
        this.difficultyManager = difficultyManager;
    }
}
