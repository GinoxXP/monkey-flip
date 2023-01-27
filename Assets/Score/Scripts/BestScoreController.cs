using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public class BestScoreController : MonoBehaviour
{
    private Score score;

    [SerializeField]
    private TMP_Text scoreText;

    private void UpdateText()
    {
        scoreText.text = score.BestScoreValue.ToString();
    }

    void Start()
    {
        UpdateText();
        score.NewBestScoreAvailable += UpdateText;
    }

    private void OnDestroy()
    {
        score.NewBestScoreAvailable -= UpdateText;
    }

    [Inject]
    private void Init(Score scoreManager)
    {
        this.score = scoreManager;
    }
}
