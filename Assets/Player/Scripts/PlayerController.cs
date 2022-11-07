using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SmoothJump smoothJump;
    private MoveLevel moveLevel;
    private PauseController pauseController;
    private JumpAssistant jumpAssistant;

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

    public float Power { get; private set; }

    public event Action StartClick;
    public event Action StopClick;

    private IEnumerator clickTimer;
    private bool isAccumulationing;

    public void OnClick(InputAction.CallbackContext context)
    {
        var pointerPosition = Pointer.current.position.ReadValue();
        if (IsPointerOverUIObject(pointerPosition))
            return;

        if (pauseController.CurrentPauseState)
            pauseController.SetPause(false);

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
        StartClick?.Invoke();
        smoothJump.Animator.SetTrigger("PrepareJump");
        Power = minPower;
        isAccumulationing = true;
        clickTimer = ClickTimer();
        StartCoroutine(clickTimer);
    }

    private void OnStopClick()
    {
        if (!isAccumulationing)
            return;

        StopClick?.Invoke();
        smoothJump.Animator.SetTrigger("Flip");
        isAccumulationing = false;
        StopCoroutine(clickTimer);
        TransmitPower();
        Power = 0;
    }

    private IEnumerator ClickTimer()
    {
        while (true)
        {
            Power += powerAccumulationSpeed * Time.deltaTime;

            if(Power >= maxPower)
            {
                Power = maxPower;
                OnStopClick();
            }

            yield return null;
        }
    }

    private void TransmitPower()
    {
        var assistedPower = jumpAssistant.GetAssistedPower(Power);
        smoothJump.Jump(assistedPower);
        moveLevel.Move();
    }

    private bool IsPointerOverUIObject(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    [Inject]
    private void Init(SmoothJump smoothJump, MoveLevel moveLevel, PauseController pauseController, JumpAssistant jumpAssistant)
    {
        this.smoothJump = smoothJump;
        this.moveLevel = moveLevel;
        this.pauseController = pauseController;
        this.jumpAssistant = jumpAssistant;
    }
}
