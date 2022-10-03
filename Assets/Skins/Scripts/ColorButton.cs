using System;
using UnityEngine;
using UnityEngine.UI;
using static SkinData;

public class ColorButton : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject selectIcon;
    [SerializeField]
    private GameObject lockIcon;

    public Color Color { get => image.color; set => image.color = value; }

    public bool IsSelected { get => selectIcon.activeSelf; set => selectIcon.SetActive(value); }

    public bool IsLocked { get => lockIcon.activeSelf; set => lockIcon.SetActive(value); }

    public ColorSet ColorSet { get; set; }

    public ColorPalette ColorPalette { get; set; }

    public Material TargetMaterial { get; set; }

    public event Action<ColorSet, ColorPalette, Material> OnSelect;

    public void Click()
    {
        OnSelect?.Invoke(ColorSet, ColorPalette, TargetMaterial);
    }
}
