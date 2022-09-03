using System;
using UnityEngine;
using UnityEngine.UI;

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

    public Material TargetMaterial { get; set; }

    public event Action Select;

    public void Click()
    {
        TargetMaterial.color = Color;
    }
}
