using System;
using UnityEngine;
using UnityEngine.UI;
using static SkinData;

public class SkinButton : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject selectIcon;
    [SerializeField]
    private GameObject lockIcon;

    public Skin Skin { get; set; }

    public bool IsSelected { get => selectIcon.activeSelf; set => selectIcon.SetActive(value); }

    public bool IsLocked { get => lockIcon.activeSelf; set => lockIcon.SetActive(value); } 

    public Action<Skin> OnSelect;

    public void Click()
    {
        OnSelect?.Invoke(Skin);
    }

    private void Start()
    {
        image.sprite = Skin.Icon;
    }
}
