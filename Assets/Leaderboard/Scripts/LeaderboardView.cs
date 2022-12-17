using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class LeaderboardView : MonoBehaviour
{
    private Yandex yandex;

    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text playerScore;

    public void Open()
    {
        gameObject.SetActive(true);
        yandex.GetLeaderboard();
        Fill();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void Fill()
    {
        var entry = yandex.PlayerLeaderboardEntry;
        playerName.text = entry.player.publicName;
        playerScore.text = entry.score.ToString();
    }

    [Inject]
    private void Init(Yandex yandex)
    {
        this.yandex = yandex;
    }
}
