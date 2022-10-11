using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private int defaultTimeIndex;
    [Space]
    [SerializeField]
    private List<LightScheme> lightSchemes;

    public event Action<List<Color>> TimeCycleChange;

    private int lastScore;
    private IEnumerator timeChanging;
    private int timeIndex;
    private LightScheme lastLightScheme;

    private void NextTime()
    {
        timeIndex++;

        if (timeIndex >= lightSchemes.Count)
            timeIndex = 0;

        timeChanging = TimeChanging(lightSchemes[timeIndex]);
        StartCoroutine(timeChanging);
    }

    private void OnNewScoreAvailabled()
    {
        var score = scoreManager.Score;

        if (score % stepChanging < lastScore % stepChanging)
            NextTime();

        lastScore = score;
    }

    private LightScheme CloneLightSceme(LightScheme lightScheme)
    {
        var cloneLightScheme = new LightScheme()
        {
            SkyColor = lightScheme.SkyColor,
            LightColor = lightScheme.LightColor,
        };
        cloneLightScheme.BackgroundColors = new();
        for (int i = 0; i < lightScheme.BackgroundColors.Count; i++)
            cloneLightScheme.BackgroundColors.Add(lightScheme.BackgroundColors[i]);

        return cloneLightScheme;
    }

    private IEnumerator TimeChanging(LightScheme lightScheme)
    {
        var progress = 0f;

        var newLightScheme = new LightScheme();

        while (progress <= 1)
        {
            camera.backgroundColor = Color.Lerp(lastLightScheme.SkyColor, lightScheme.SkyColor, progress);
            light.color = Color.Lerp(lastLightScheme.LightColor, lightScheme.LightColor, progress);

            for (int i = 0; i < lightScheme.BackgroundColors.Count; i++)
                newLightScheme.BackgroundColors[i] = Color.Lerp(lastLightScheme.BackgroundColors[i], lightScheme.BackgroundColors[i], progress);

            TimeCycleChange?.Invoke(newLightScheme.BackgroundColors);

            progress += speedChanging * Time.deltaTime;
            yield return null;
        }
        timeChanging = null;

        lastLightScheme = CloneLightSceme(newLightScheme);
    }

    private void Start()
    {
        timeIndex = defaultTimeIndex;
        lastLightScheme = CloneLightSceme(lightSchemes.First());

        scoreManager.NewScoreAvailable += OnNewScoreAvailabled;
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
    private string name;
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
