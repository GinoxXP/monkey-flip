using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DayCycleController : MonoBehaviour
{
    private new Light light;
    private new Camera camera;
    private ScoreManager scoreManager;

    [SerializeField]
    private int stepChanging;
    [SerializeField]
    private LightingScheme dayScheme;
    [SerializeField]
    private LightingScheme nightScheme;

    public event Action<List<Color>> TimeCycleChange;

    private int lastScore;
    private bool isNight;

    private void SetDay()
    {
        isNight = false;

        TimeCycleChange?.Invoke(dayScheme.BackgroundColors);
        camera.backgroundColor = dayScheme.SkyColor;
        light.color = dayScheme.LightColor;
    }

    private void SetNight()
    {
        isNight = true;

        TimeCycleChange?.Invoke(nightScheme.BackgroundColors);
        camera.backgroundColor = nightScheme.SkyColor;
        light.color = nightScheme.LightColor;
    }

    private void ChangeTime()
    {
        if (isNight)
            SetDay();
        else
            SetNight();
    }

    private void Start()
    {
        scoreManager.NewScoreAvailable += OnNewScoreAvailabled;
    }

    private void OnNewScoreAvailabled()
    {
        var score = scoreManager.Score;

        if (score % stepChanging < lastScore % stepChanging)
            ChangeTime();

        lastScore = score;
    }

    [Inject]
    private void Init(Light light, Camera camera, ScoreManager scoreManager)
    {
        this.light = light;
        this.camera = camera;
        this.scoreManager = scoreManager;
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
