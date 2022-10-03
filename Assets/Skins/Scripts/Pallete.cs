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

    public event Action<ColorSet, Material> OnSelect;

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

            colorButtonComponent.OnSelect += OnSelected;

            colorButtons.Add((colorButtonComponent, colorSet));
        }
    }

    private void OnSelected(ColorSet colorSet, Material material)
    {
        //DeselectAll();
        OnSelect?.Invoke(colorSet, material);
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
