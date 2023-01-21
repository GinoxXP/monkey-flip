using TMPro;
using UnityEngine;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private Score score;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text bestScoreText;

    public void Revalidate()
    {
        EnableBestScoreText();
        Start();
    }

    private void UpdateText()
    {
        scoreText.text = score.ScoreValue.ToString();
        bestScoreText.text = score.BestScoreValue.ToString();
    }

    private void DisableBestScoreText()
    {
        if (bestScoreText.gameObject.activeSelf)
            bestScoreText.gameObject.SetActive(false);
    }

    private void EnableBestScoreText()
    {
        bestScoreText.gameObject.SetActive(true);
    }

    private void Start()
    {
        UpdateText();

        if (score.BestScoreValue == 0)
            DisableBestScoreText();

        score.NewScoreAvailable += UpdateText;
        score.NewBestScoreAvailable += DisableBestScoreText;
        score.NeedRevalidate += Revalidate;
    }

    private void OnDestroy()
    {
        score.NewScoreAvailable -= UpdateText;
        score.NewBestScoreAvailable -= DisableBestScoreText;
        score.NeedRevalidate -= Revalidate;
    }

    [Inject]
    private void Init(Score scoreManager)
    {
        this.score = scoreManager;
    }
}
