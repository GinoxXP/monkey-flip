using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour
{
    public string PlayerName { get; private set; }

    public Texture PlayerPhoto { get; private set; }

    #region ToJS

    [DllImport("__Internal")]
    private static extern void GetPlayerData();

    #endregion

    #region fromJS

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void SetPlayerPhoto(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

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
}
