using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LeaderboardView : MonoBehaviour
{
    private Yandex yandex;

    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text playerScore;
    [SerializeField]
    private TMP_Text playerRank;
    [SerializeField]
    private RawImage playerAvatar;
    [SerializeField]
    private GameObject leaderboardEntryPrefab;
    [SerializeField]
    private Transform leaderboardEntryParent;

    private List<LeaderboardEntry> leaderbordEntries = new List<LeaderboardEntry>();

    public void Open()
    {
        gameObject.SetActive(true);
        yandex.GetLeaderboard();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        ClearLeaderboard();
    }


    private void FillPlayerEntry()
    {
        var playerEntry = yandex.PlayerLeaderboardEntry;
        playerName.text = playerEntry.player.publicName;
        playerScore.text = playerEntry.formattedScore;
    }

    private void FillPlayerPhoto()
    {
        playerAvatar.texture = yandex.PlayerPhoto;
    }

    private void FillLeaderboard()
    {
        var leaderboard = yandex.PlayerLeaderboard;
        foreach (var entry in leaderboard.entries)
        {
            var entryObject = Instantiate(leaderboardEntryPrefab, leaderboardEntryParent);
            var entryComponent = entryObject.GetComponent<LeaderboardEntry>();
            entryComponent.Rank = entry.rank.ToString();
            entryComponent.Name = entry.player.publicName;
            entryComponent.Score = entry.formattedScore;
            leaderbordEntries.Add(entryComponent);
        }
    }

    private void ClearLeaderboard()
    {
        for (int i = leaderbordEntries.Count - 1; i > 0; i--)
        {
            Destroy(leaderbordEntries[i].gameObject);
            leaderbordEntries.RemoveAt(i);
        }
    }

    private void OnDestroy()
    {
        yandex.LeaderboardEntryReceived -= FillPlayerEntry;
        yandex.LeaderboardReceived -= FillLeaderboard;
        yandex.PlayerDataReceived -= FillPlayerPhoto;
    }

    private void Awake()
    {
        yandex.LeaderboardEntryReceived += FillPlayerEntry;
        yandex.LeaderboardReceived += FillLeaderboard;
        yandex.PlayerDataReceived += FillPlayerPhoto;
    }

    [Inject]
    private void Init(Yandex yandex)
    {
        this.yandex = yandex;
    }
}
