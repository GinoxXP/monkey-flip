using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;

    private void Start()
    {
        UpdateText();

        if (scoreManager.BestScore == 0)
            DisableBestScoreText();

        scoreManager.NewScoreAvailable += UpdateText;
        scoreManager.NewBestScoreAvailable += DisableBestScoreText;
    }

    private void UpdateText()
    {
        scoreText.text = scoreManager.Score.ToString();
        bestScoreText.text = scoreManager.BestScore.ToString();
    }

    private void DisableBestScoreText()
    {
        if(bestScoreText.gameObject.activeSelf)
            bestScoreText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        scoreManager.NewScoreAvailable -= UpdateText;
        scoreManager.NewBestScoreAvailable -= DisableBestScoreText;
    }

    [Inject]
    private void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
