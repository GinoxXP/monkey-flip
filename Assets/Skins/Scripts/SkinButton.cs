using System;
using UnityEngine;
using UnityEngine.UI;
using static SkinData;

public class SkinButton : MonoBehaviour
{
    private bool isSelected;

    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject selectedPanel;
    [SerializeField]
    private GameObject skinSelectedIcon;

    public Skin Skin { get; set; }

    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            selectedPanel.SetActive(isSelected);
        }
    }

    public Action<SkinButton> OnSelect;

    public void Click()
    {
        OnSelect?.Invoke(this);
    }

    private void UpdateSkinSelectedIcon()
    {
        skinSelectedIcon.SetActive(Skin.IsSelected);
    }

    private void OnSkinPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateSkinSelectedIcon();
    }

    private void Start()
    {
        image.sprite = Skin.Icon;
        UpdateSkinSelectedIcon();
        Skin.PropertyChanged += OnSkinPropertyChanged;
    }
}
