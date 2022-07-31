using System;
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
            NewScoreAvailable?.Invoke();

            if (score > BestScore)
            {
                BestScore = value;
                NewBestScoreAvailable?.Invoke();
            }
        }
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        set => PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
    }

    public event Action NewBestScoreAvailable;
    public event Action NewScoreAvailable;
}
