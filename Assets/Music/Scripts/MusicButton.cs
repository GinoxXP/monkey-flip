using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class MusicButton : MonoBehaviour
{
    private const string VOLUME_PARAMETER = "Volume";
    private const float DISABLE_MUSIC_VOLUME = -80;
    private const float ENABLE_MUSIC_VOLUME = 0;

    private PauseController pauseController;
    private Image image;

    private bool isOn = true;

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
                musicMixer.SetFloat(VOLUME_PARAMETER, ENABLE_MUSIC_VOLUME);
            }
            else
            {
                image.sprite = offStateSprite;
                musicMixer.SetFloat(VOLUME_PARAMETER, DISABLE_MUSIC_VOLUME);
            }
        }
    }

    public void Click()
        => IsOn = !IsOn;

    private void OnPauseChanged(bool isPause)
    {
        gameObject.SetActive(isPause);
    }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    [Inject]
    private void Init(PauseController pauseController)
    {
        this.pauseController = pauseController;
        pauseController.PauseChanged += OnPauseChanged;
    }
}
