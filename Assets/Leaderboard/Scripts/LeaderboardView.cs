using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

public class LeaderboardView : MonoBehaviour
{
    private Yandex yandex;

    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text playerScore;
    [SerializeField]
    private GameObject leaderboardEntryPrefab;
    [SerializeField]
    private Transform leaderboardEntryParent;

    public void Open()
    {
        gameObject.SetActive(true);
        yandex.GetLeaderboard();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    private void Fill()
    {
        var playerEntry = yandex.PlayerLeaderboardEntry;
        playerName.text = playerEntry.player.publicName;
        playerScore.text = playerEntry.formattedScore;

        var leaderboard = yandex.PlayerLeaderboard;
        foreach (var entry in leaderboard.entries)
        {
            var entryObject = Instantiate(leaderboardEntryPrefab, leaderboardEntryParent);
            var entryComponent = entryObject.GetComponent<LeaderboardEntry>();
            entryComponent.Name = entry.player.publicName;
            entryComponent.Score = entry.formattedScore;
        }
    }

    private void OnDestroy()
    {
        yandex.LeaderboardEntryReceived -= Fill;
    }

    private void Awake()
    {
        yandex.LeaderboardEntryReceived += Fill;
    }

    [Inject]
    private void Init(Yandex yandex)
    {
        this.yandex = yandex;
    }
}
