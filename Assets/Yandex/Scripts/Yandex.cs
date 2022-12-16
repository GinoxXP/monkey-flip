using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour
{
    public Texture PlayerPhoto { get; private set; }

    public UserData User { get; private set; }

    public event Action UserAuthorizated;

    public event Action UserDataReceived;

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

    public void GetPlayerData()
    {
        try
        {
            GetPlayerDataExternal();
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

    #region fromJS

    public void UserAuthorizationCompleated()
    {
        UserAuthorizated?.Invoke();
    }

    public void SetPlayerDataInternal(string json)
    {
        User = JsonUtility.FromJson<UserData>(json);
        UserDataReceived?.Invoke();
    }

    public void SetPlayerPhotoInternal(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    public void SetLeaderboardInternal(string json)
    {

    }

    #endregion

    #region ToJS

    [DllImport("__Internal")]
    private static extern void AuthExternal();

    [DllImport("__Internal")]
    private static extern void GetPlayerDataExternal();

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdvExternal();

    [DllImport("__Internal")]
    private static extern void GetLeaderboardExternal();

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
        }
    }

    private void Start()
    {
        Authorization();
    }

    public struct UserData
    {
        public string id;
        public string name;
        public string avatarUrlSmall;
        public string avatarUrlMedium;
        public string avatarUrlLarge;
    }

}
