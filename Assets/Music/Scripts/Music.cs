using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    private const string MUSIC_KEY = "MUSIC_LEVEL";
    private const string SOUND_KEY = "SOUND_LEVEL";

    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundMixer;

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

    private void Start()
    {
        var enableMusic = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
        var enableSound = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;

        musicMixer.SetFloat(MusicButton.VOLUME_PARAMETER, enableMusic ? MusicButton.ENABLE_VOLUME : MusicButton.DISABLE_VOLUME);
        soundMixer.SetFloat(MusicButton.VOLUME_PARAMETER, enableSound ? MusicButton.ENABLE_VOLUME : MusicButton.DISABLE_VOLUME);
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
