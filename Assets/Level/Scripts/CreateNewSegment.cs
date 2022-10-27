using UnityEngine;
using Zenject;

public class CreateNewSegment : MonoBehaviour
{
    private LevelGenerator generationLevel;
    private DifficultyManager difficultyManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SmoothJump>(out _))
        {
            generationLevel.Generate();
            difficultyManager.AddDifficulty();
        }
    }

    [Inject]
    private void Init(
        LevelGenerator generationLevel,
        DifficultyManager difficultyManager)
    {
        this.generationLevel = generationLevel;
        this.difficultyManager = difficultyManager;
    }
}
