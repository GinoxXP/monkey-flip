using System;
using System.Collections;
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
    private float speedChanging;
    [Space]
    [SerializeField]
    private LightScheme dayScheme;
    [SerializeField]
    private LightScheme nightScheme;

    public event Action<List<Color>> TimeCycleChange;

    private int lastScore;
    private bool isNight;
    private IEnumerator timeChanging;

    private void SetDay()
    {
        isNight = false;

        timeChanging = TimeChanging(dayScheme, nightScheme);
        StartCoroutine(timeChanging);

    }

    private void SetNight()
    {
        isNight = true;

        timeChanging = TimeChanging(nightScheme, dayScheme);
        StartCoroutine(timeChanging);
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

    private IEnumerator TimeChanging(LightScheme lightScheme, LightScheme lastLightScheme)
    {
        var progress = 0f;
        var temporaryLightScheme = new LightScheme()
        {
            SkyColor = lastLightScheme.SkyColor,
            LightColor = lastLightScheme.LightColor,
        };
        temporaryLightScheme.BackgroundColors = new ();
        for (int i = 0; i < lastLightScheme.BackgroundColors.Count; i++)
            temporaryLightScheme.BackgroundColors.Add(lastLightScheme.BackgroundColors[i]);

        while (progress <= 1)
        {
            camera.backgroundColor = Color.Lerp(lastLightScheme.SkyColor, lightScheme.SkyColor, progress);
            light.color = Color.Lerp(lastLightScheme.LightColor, lightScheme.LightColor, progress);

            for (int i = 0; i < temporaryLightScheme.BackgroundColors.Count; i++)
                temporaryLightScheme.BackgroundColors[i] = Color.Lerp(lastLightScheme.BackgroundColors[i], lightScheme.BackgroundColors[i], progress);

            TimeCycleChange?.Invoke(temporaryLightScheme.BackgroundColors);

            progress += speedChanging * Time.deltaTime;
            yield return null;
        }
        timeChanging = null;
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
public struct LightScheme
{
    [SerializeField]
    private List<Color> backgroundColors;
    [SerializeField]
    private Color skyColor;
    [SerializeField]
    private Color lightColor;

    public List<Color> BackgroundColors { get => backgroundColors; set => backgroundColors = value; }

    public Color SkyColor { get => skyColor; set => skyColor = value; }

    public Color LightColor { get => lightColor; set => lightColor = value; }
}
