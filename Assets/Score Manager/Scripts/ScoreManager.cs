using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string BEST_SCORE_KEY = "BestScore";

    private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            if(score > BestScore)
            {
                BestScore = value;
                IsNewRecord = true;
            }
        }
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        set => PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
    }

    public bool IsNewRecord { get; private set; }
}
