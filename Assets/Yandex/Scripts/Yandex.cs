using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Yandex : MonoBehaviour
{
    public string PlayerName { get; private set; }

    public Texture PlayerPhoto { get; private set; }

    public object LoadedData { get; private set; }


    public void Authorize()
    {
        AuthExternal();
    }

    public void Save(object obj)
    {
        var jsonString = JsonUtility.ToJson(obj);
        SaveDataExternal(jsonString);
    }

    public async Task<T> Load<T>()
    {
        LoadDataExternal();

        var task = new Task<T>(() =>
        {
            while (LoadedData == null) { }
            return (T)LoadedData;
        });
        return await task;
    }

    #region fromJS

    public void LoadDataInternal(string json)
    {
        LoadedData = JsonUtility.FromJson<object>(json);
    }

    public void SetPlayerNameInternal(string name)
    {
        PlayerName = name;
    }

    public void SetPlayerPhotoInternal(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    #endregion

    #region ToJS

    [DllImport("__Internal")]
    private static extern void AuthExternal();

    [DllImport("__Internal")]
    private static extern void SaveDataExternal(string jsonData);

    [DllImport("__Internal")]
    private static extern void LoadDataExternal();

    [DllImport("__Internal")]
    private static extern void GetPlayerDataExternal();

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
