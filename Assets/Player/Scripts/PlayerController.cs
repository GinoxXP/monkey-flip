using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SmoothJump player;

    [SerializeField]
    private float powerAccumulationSpeed;
    [SerializeField]
    private float maxPower;
    [SerializeField]
    private float minPower;

    public float MaxPower => maxPower;

    private float power;
    private IEnumerator clickTimer;
    private bool isAccumulationing;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnStartClick();
        }

        if (context.canceled && isAccumulationing)
        {
            OnStopClick();
        }
    }

    private void OnStartClick()
    {
        power = minPower;
        isAccumulationing = true;
        clickTimer = ClickTimer();
        StartCoroutine(clickTimer);
    }

    private void OnStopClick()
    {
        isAccumulationing = false;
        StopCoroutine(clickTimer);
        TransmitPower();
        power = 0;
    }

    private IEnumerator ClickTimer()
    {
        while (true)
        {
            power += powerAccumulationSpeed * Time.deltaTime;

            if(power >= maxPower)
            {
                power = maxPower;
                OnStopClick();
            }

            yield return null;
        }
    }

    private void TransmitPower()
    {
        player.Jump(power);
    }

    [Inject]
    private void Init(SmoothJump player)
    {
        this.player = player;
    }
}
