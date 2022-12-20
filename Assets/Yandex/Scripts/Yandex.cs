using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour
{
    public Texture PlayerPhoto { get; private set; }

    public LeaderboardEntry PlayerLeaderboardEntry { get; private set; }

    public Leaderboard PlayerLeaderboard { get; private set; }

    public event Action PlayerAuthorizated;

    public event Action PlayerDataReceived;

    public event Action LeaderboardReceived;

    public event Action LeaderboardEntryReceived;

    public event Action PlayerPhotoDownloaded;

    public void Authorization()
    {
        try
        {
            AuthExternal();
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void ShowFullscreenAdv()
    {
        try
        {
            ShowFullscreenAdvExternal();
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void GetPlayerPhoto()
    {
        try
        {
            GetPlayerPhotoExternal();
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void GetLeaderboard()
    {
        try
        {
            GetLeaderboardExternal();
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void SetToLeaderboard(int score)
    {
        SetToLeaderboardExternal(score);
    }

    #region fromJS

    public void UserAuthorizationCompleated()
    {
        PlayerAuthorizated?.Invoke();
    }

    public void SetPlayerPhotoInternal(string url)
    {
        StartCoroutine(DownloadImage(url));
        PlayerDataReceived?.Invoke();
    }

    public void SetLeaderboardInternal(string json)
    {
        PlayerLeaderboard = JsonUtility.FromJson<Leaderboard>(json);
        LeaderboardReceived?.Invoke();
    }

    public void SetLeaderboardEntryInternal(string json)
    {
        PlayerLeaderboardEntry = JsonUtility.FromJson<LeaderboardEntry>(json);
        LeaderboardEntryReceived?.Invoke();
    }

    #endregion

    #region ToJS

    [DllImport("__Internal")]
    private static extern void AuthExternal();

    [DllImport("__Internal")]
    private static extern void GetPlayerPhotoExternal();

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdvExternal();

    [DllImport("__Internal")]
    private static extern void GetLeaderboardExternal();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboardExternal(int score);

    #endregion

    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new WebException(request.error);
        }
        else
        {
            PlayerPhoto = ((DownloadHandlerTexture)request.downloadHandler).texture;
            PlayerPhotoDownloaded?.Invoke();
        }
    }

    private void OnDestroy()
    {
        PlayerAuthorizated -= GetLeaderboard;
    }

    private void Awake()
    {
        PlayerAuthorizated += GetLeaderboard;
    }

    private void Start()
    {
        Authorization();
    }

    [Serializable]
    public struct Leaderboard
    {
        public LeaderboardEntry[] entries;
    }

    [Serializable]
    public struct LeaderboardEntry
    {
        public int score;

        public string extraData;

        public int rank;

        public PlayerEntry player;

        public string formattedScore;
    }

    [Serializable]
    public struct PlayerEntry
    {
        public string lang;

        public string publicName;

        public string uniqueID;

        public ScopePermission scopePermissions;
    }

    [Serializable]
    public struct ScopePermission
    {
        public string avatar;

        public string public_name;
    }
}
