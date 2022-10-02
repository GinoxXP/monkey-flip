using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using static SkinData;

public class Pallete : MonoBehaviour
{
    private SkinController skinController;
    private List<(ColorButton, ColorSet)> colorButtons = new ();

    [SerializeField]
    private TMP_Text palleteTitle;
    [SerializeField]
    private Transform colorsParent;
    [SerializeField]
    private GameObject colorButtonPrefab;

    public event Action<ColorSet> OnBuyColor;

    public void Fill(ColorPalette colorPalettes)
    {
        DestroyColorButtons();

        palleteTitle.text = colorPalettes.Name;

        foreach (var colorSet in colorPalettes.ColorSets)
        {
            var colorButton = Instantiate(colorButtonPrefab, colorsParent);
            var colorButtonComponent = colorButton.GetComponent<ColorButton>();

            colorButtonComponent.Color = colorSet.Color;
            colorButtonComponent.ColorSet = colorSet;
            colorButtonComponent.TargetMaterial = colorPalettes.TargetMaterial;

            colorButtonComponent.OnSelect += OnColorButtonClicked;

            colorButtons.Add((colorButtonComponent, colorSet));
        }
    }

    private void OnColorButtonClicked(ColorSet colorSet, Material targetMaterial)
    {
        if (colorSet.IsBought)
        {
            skinController.SetColor(colorSet, targetMaterial);
            DeselectAll();
            UpdateColorButtons();
        }
        else
        {
            OnBuyColor?.Invoke(colorSet);
        }
    }

    private void DeselectAll()
    {
        foreach (var (colorButton, colorSet) in colorButtons)
            colorSet.IsSelected = false;
    }

    private void UpdateColorButtons()
    {
        foreach (var (colorButton, colorSet) in colorButtons)
        {
            colorButton.IsLocked = !colorSet.IsBought;
            colorButton.IsSelected = colorSet.IsSelected;
        }
    }

    private void DestroyColorButtons()
    {
        for (int i = 0; i < colorButtons.Count; i++)
        {
            var (colorButton, colorSet) = colorButtons[i];
            colorButton.OnSelect -= OnColorButtonClicked;
            Destroy(colorButton.gameObject);
        }

        colorButtons.Clear();
    }

    private void Start()
    {
        UpdateColorButtons();
    }

    [Inject]
    private void Init(SkinController skinController)
    {
        this.skinController = skinController;
    }
}
