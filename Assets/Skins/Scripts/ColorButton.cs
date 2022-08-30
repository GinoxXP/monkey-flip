using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorButton : MonoBehaviour
{
    private Image image;

    public event Action ColorChanged;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Click()
    {

    }
}
