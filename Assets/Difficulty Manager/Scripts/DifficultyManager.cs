using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private float difficultyStep;
    [SerializeField]
    private float startDifficulty;
    [SerializeField]
    private float maxDifficulty;
    [SerializeField]
    private float difficulty;

    public float Difficulty { get => difficulty; private set => difficulty = value; }

    public void AddDifficulty(int levels = 1)
    {
        if (Difficulty >= maxDifficulty)
        {
            Difficulty = maxDifficulty;
            return;
        }

        for (int i = 0; i < levels; i++)
        {
            Difficulty += difficultyStep;
        }
    }

    private void Start()
    {
        Difficulty += startDifficulty;
    }
}
