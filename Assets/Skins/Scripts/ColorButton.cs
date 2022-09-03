using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public Color Color { get => image.color; set => image.color = value; }

    public void Click()
    {

    }
}
