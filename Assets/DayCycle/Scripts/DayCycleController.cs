using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DayCycleController : MonoBehaviour
{
    private new Light light;
    private new Camera camera;

    [SerializeField]
    private LightingScheme dayScheme;
    [SerializeField]
    private LightingScheme nightScheme;

    public event Action<List<Color>> TimeCycleChange;

    public void SetDay()
    {
        TimeCycleChange?.Invoke(dayScheme.BackgroundColors);
        camera.backgroundColor = dayScheme.SkyColor;
        light.color = dayScheme.LightColor;
    }

    public void SetNight()
    {
        TimeCycleChange?.Invoke(nightScheme.BackgroundColors);
        camera.backgroundColor = nightScheme.SkyColor;
        light.color = nightScheme.LightColor;
    }

    private void Start()
    {
        SetNight();
    }

    [Inject]
    private void Init(Light light, Camera camera)
    {
        this.light = light;
        this.camera = camera;
    }
}

[Serializable]
public struct LightingScheme
{
    [SerializeField]
    private List<Color> backgroundColors;
    [SerializeField]
    private Color skyColor;
    [SerializeField]
    private Color lightColor;

    public List<Color> BackgroundColors { get => backgroundColors; }

    public Color SkyColor { get => skyColor; }

    public Color LightColor { get => lightColor; }
}
