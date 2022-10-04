using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SkinData;

public class Palette : MonoBehaviour
{
    private List<(ColorButton, ColorSet)> colorButtons = new ();

    [SerializeField]
    private TMP_Text palleteTitle;
    [SerializeField]
    private Transform colorsParent;
    [SerializeField]
    private GameObject colorButtonPrefab;

    public event Action<ColorSet, ColorPalette, Material> OnSelect;

    public void Fill(ColorPalette colorPalette)
    {
        DestroyColorButtons();

        palleteTitle.text = colorPalette.Name;

        foreach (var colorSet in colorPalette.ColorSets)
        {
            var colorButton = Instantiate(colorButtonPrefab, colorsParent);
            var colorButtonComponent = colorButton.GetComponent<ColorButton>();

            colorButtonComponent.Color = colorSet.Color;
            colorButtonComponent.ColorSet = colorSet;
            colorButtonComponent.ColorPalette = colorPalette;
            colorButtonComponent.TargetMaterial = colorPalette.TargetMaterial;

            colorButtonComponent.OnSelect += OnSelected;

            colorButtons.Add((colorButtonComponent, colorSet));
        }
    }

    public void UpdateColorButtons()
    {
        foreach (var (colorButton, colorSet) in colorButtons)
        {
            colorButton.IsLocked = !colorSet.IsBought;
            colorButton.IsSelected = colorSet.IsSelected;
        }
    }

    private void OnSelected(ColorSet colorSet, ColorPalette colorPalette, Material material)
    {
        OnSelect?.Invoke(colorSet, colorPalette, material);
        UpdateColorButtons();
    }

    private void DestroyColorButtons()
    {
        for (int i = 0; i < colorButtons.Count; i++)
        {
            var (colorButton, colorSet) = colorButtons[i];
            colorButton.OnSelect -= OnSelect;
            Destroy(colorButton.gameObject);
        }

        colorButtons.Clear();
    }

    private void Start()
    {
        UpdateColorButtons();
    }
}
