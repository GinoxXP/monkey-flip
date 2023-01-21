using UnityEngine;
using Zenject;

public class LeaderboardButton : MonoBehaviour
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
