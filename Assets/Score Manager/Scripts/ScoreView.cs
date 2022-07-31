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

    private void Update()
    {
        scoreText.text = scoreManager.Score.ToString();
        bestScoreText.text = scoreManager.BestScore.ToString();
    }

    [Inject]
    private void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }
}
