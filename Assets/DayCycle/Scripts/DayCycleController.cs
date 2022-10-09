using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    [SerializeField]
    private List<Color> dayColors = new ();
    [SerializeField]
    private List<Color> nightColors = new ();

    public event Action<List<Color>> TimeCycleChange;

    public void SetDay()
    {
        TimeCycleChange?.Invoke(dayColors);
    }

    public void SetNight()
    {
        TimeCycleChange?.Invoke(nightColors);
    }

    private void Start()
    {
        SetNight();
    }
}
