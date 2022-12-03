using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MusicButton : MonoBehaviour
{
    private const string VOLUME_PARAMETER = "Volume";
    private Image image;
    private bool isOn;

    [SerializeField]
    private Sprite onStateSprite;
    [SerializeField]
    private Sprite offStateSprite;
    [SerializeField]
    private AudioMixer musicMixer;

    private bool IsOn
    {
        get => isOn;
        set
        {
            isOn = value;
            if (isOn)
            {
                image.sprite = onStateSprite;
                musicMixer.SetFloat(VOLUME_PARAMETER, 1);
            }
            else
            {
                image.sprite = offStateSprite;
                musicMixer.SetFloat(VOLUME_PARAMETER, 0);
            }
        }
    }

    public void Click()
        => IsOn = !IsOn;

    private void Start()
    {
        image = GetComponent<Image>();
    }
}
