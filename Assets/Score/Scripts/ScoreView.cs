using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private Score scoreManager;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;

    private void Start()
    {
        UpdateText();

        if (scoreManager.BestScoreValue == 0)
            DisableBestScoreText();

        scoreManager.NewScoreAvailable += UpdateText;
        scoreManager.NewBestScoreAvailable += DisableBestScoreText;
    }

    private void UpdateText()
    {
        scoreText.text = scoreManager.ScoreValue.ToString();
        bestScoreText.text = scoreManager.BestScoreValue.ToString();
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
    private void Init(Score scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
