using TMPro;
using UnityEngine;
using Zenject;

public class ScoreController : MonoBehaviour
{
    private Score score;

    [SerializeField]
    private TMP_Text scoreText;

    private void UpdateText()
    {
        scoreText.text = score.ScoreValue.ToString();
    }

    void Start()
    {
        UpdateText();
        score.NewScoreAvailable += UpdateText;
    }

    void OnDestroy()
    {
        score.NewScoreAvailable -= UpdateText;
    }

    [Inject]
    private void Init(Score scoreManager)
    {
        this.score = scoreManager;
    }
}
