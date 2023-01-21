using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CreditsButton : MonoBehaviour
{
    private PauseController pauseController;
    private void OnPauseChanged(bool pause)
    {
        gameObject.SetActive(pause);
    }

    private void OnDestroy()
    {
        pauseController.PauseChanged -= OnPauseChanged;
    }

    [Inject]
    private void Init(PauseController pauseController)
    {
        this.pauseController = pauseController;
        pauseController.PauseChanged += OnPauseChanged;
    }
}
