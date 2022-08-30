using UnityEngine;
using Zenject;

public class WardrobeButton : MonoBehaviour
{
    private PauseController pauseController;

    private void OnPauseChanged(bool isPause)
    {
        gameObject.SetActive(isPause);
    }

    private void OnDestroy()
    {
        pauseController.PauseChanged -= OnPauseChanged;
    }

    [Inject]
    private void Init(PauseController pauseController)
    {
        this.pauseController = pauseController;

        this.pauseController.PauseChanged += OnPauseChanged;
    }
}
