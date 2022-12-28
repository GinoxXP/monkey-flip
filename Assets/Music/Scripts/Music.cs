using UnityEngine;

public class Music : MonoBehaviour
{
    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        AudioListener.volume = silence ? 0 : 1;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    private void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Awake()
    {
        var musics = FindObjectsOfType<Music>();
        if (musics.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
