using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SkinData;

public class Pallete : MonoBehaviour
{
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

            colorButtonComponent.Select += OnColorButtonClicked;

            colorButtons.Add((colorButtonComponent, colorSet));
        }
    }

    private void OnColorButtonClicked(ColorSet colorSet, Material targetMaterial)
    {
        if (colorSet.IsBought)
        {
            SetColor(colorSet, targetMaterial);
            UpdateColorButtons();
        }
        else
        {
            OnBuyColor?.Invoke(colorSet);
        }
    }

    private void SetColor(ColorSet colorSet, Material targetMaterial)
    {
        DeselectAll();
        targetMaterial.color = colorSet.Color;
        colorSet.IsSelected = true;
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
            colorButton.Select -= OnColorButtonClicked;
            Destroy(colorButton.gameObject);
        }

        colorButtons.Clear();
    }

    private void Start()
    {
        UpdateColorButtons();
    }
}
