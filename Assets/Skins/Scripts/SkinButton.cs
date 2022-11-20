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

    private void Start()
    {
        image.sprite = Skin.Icon;
    }
}
