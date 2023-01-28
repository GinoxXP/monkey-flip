using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MusicButton : MonoBehaviour
{
    public const string VOLUME_PARAMETER = "Volume";
    public const float DISABLE_VOLUME = -80;
    public const float ENABLE_VOLUME = 0;

    private bool isOn = true;

    [SerializeField]
    private GameObject line;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private string KEY;

    private bool IsOn
    {
        get => isOn;
        set
        {
            isOn = value;
            line.SetActive(!isOn);
            PlayerPrefs.SetInt(KEY, isOn ? 1 : 0);

            if (isOn)
                musicMixer.SetFloat(VOLUME_PARAMETER, ENABLE_VOLUME);
            else
                musicMixer.SetFloat(VOLUME_PARAMETER, DISABLE_VOLUME);
        }
    }

    public void Click()
        => IsOn = !IsOn;

    private void Start()
    {
        IsOn = PlayerPrefs.GetInt(KEY, 1) == 1;
    }
}
