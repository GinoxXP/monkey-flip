using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class JumpIndicatorController : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Color lowPowerColor;
    [SerializeField]
    private Color hightPowerColor;
    [SerializeField]
    private Image indicatorImage;
    [SerializeField]
    private GameObject indicator;

    private IEnumerator indicatorUpdateCoroutine;

    private void Start()
    {
        indicator.SetActive(false);

        playerController.StartClick += OnJumpPowerStartDetected;
        playerController.StopClick += OnJumpPowerStopDetected;
    }

    private void OnJumpPowerStartDetected()
    {
        indicator.SetActive(true);
        indicatorUpdateCoroutine = IndicatorUpdate();
        StartCoroutine(indicatorUpdateCoroutine);
    }

    private void OnJumpPowerStopDetected()
    {
        indicator.SetActive(false);
        StopCoroutine(IndicatorUpdate());
        indicatorUpdateCoroutine = null;
    }

    private IEnumerator IndicatorUpdate()
    {
        while (true)
        {
            var value = Mathf.Clamp(playerController.Power, 0, playerController.MaxPower);
            slider.value = value;
            var color = Color.Lerp(lowPowerColor, hightPowerColor, value);
            indicatorImage.color = color;

            yield return null;
        }
    }

    [Inject]
    private void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
