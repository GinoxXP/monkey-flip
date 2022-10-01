using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinButton : MonoBehaviour
{
    private Image image;

    public Sprite Icon { get; set; }

    public void Click()
    {

    }

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Icon;
    }
}
