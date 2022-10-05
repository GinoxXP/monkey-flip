using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public event Action<bool> PauseChanged;

    public bool CurrentPauseState { get; private set; }

    public void SetPause(bool isPause)
    {
        CurrentPauseState = isPause;
        PauseChanged?.Invoke(isPause);
    }

    private void Start()
    {
        SetPause(true);
    }
}
