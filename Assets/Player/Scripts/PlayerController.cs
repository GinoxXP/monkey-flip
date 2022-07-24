using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private DifficultyManager difficultyManager;

    [SerializeField]
    [Range(0,1)]
    private float powerAccumulationSpeed;
    [SerializeField]
    [Range(0, 1)]
    private float maxPower;
    [SerializeField]
    [Range(0, 1)]
    private float minPower;

    public float MaxPower => maxPower;

    public bool IsCanJump { get; set; } = true;

    private float power;
    private IEnumerator clickTimer;
    private bool isAccumulationing;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!IsCanJump)
            return;

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
        difficultyManager.AddDifficulty();
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
        smoothJump.Jump(power);
        moveLevel.Move(power);
    }

    [Inject]
    private void Init(SmoothJump smoothJump, MoveLevel moveLevel, DifficultyManager difficultyManager)
    {
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.difficultyManager = difficultyManager;
    }
}
