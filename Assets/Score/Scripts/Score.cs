using System;
using UnityEngine;
using Zenject;

public class Score : MonoBehaviour
{
    private const string BEST_SCORE_KEY = "BestScore";

    private Yandex yandex;

    [SerializeField]
    private AudioSource newBestScore;

    private int scoreValue;
    private bool isSetNewRecord;

    public int ScoreValue
    {
        get => scoreValue;
        set
        {
            scoreValue = value;
            NewScoreAvailable?.Invoke();

            if (scoreValue > BestScoreValue)
            {
                BestScoreValue = value;
                NewBestScoreAvailable?.Invoke();

                if (!isSetNewRecord)
                {
                    newBestScore.Play();
                    isSetNewRecord = true;
                }
            }
        }
    }

    public int BestScoreValue
    {
        get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
            yandex.SetToLeaderboard(value);
        }
    }

    public event Action NewBestScoreAvailable;
    public event Action NewScoreAvailable;

    [Inject]
    private void Init(Yandex yandex)
    {
        this.yandex = yandex;
    }
}
