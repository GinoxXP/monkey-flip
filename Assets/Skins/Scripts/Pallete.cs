using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SkinData;

public class Pallete : MonoBehaviour
{
    private List<ColorButton> colorButtons = new List<ColorButton>();

    [SerializeField]
    private TMP_Text palleteTitle;
    [SerializeField]
    private Transform colorsParent;
    [SerializeField]
    private GameObject colorButtonPrefab;

    public void Fill(ColorPalette colorPalettes)
    {
        DestroyColorButtons();

        palleteTitle.text = colorPalettes.Name;

        foreach (var colorSet in colorPalettes.Colors)
        {
            var colorButton = Instantiate(colorButtonPrefab, colorsParent);
            var colorButtonComponent = colorButton.GetComponent<ColorButton>();

            colorButtonComponent.Color = colorSet.Color;
            colorButtonComponent.TargetMaterial = colorPalettes.TargetMaterial;
            colorButtonComponent.Select += UnselectAll;

            colorButtons.Add(colorButtonComponent);
        }
    }

    private void UnselectAll()
    {
        foreach (var colorButton in colorButtons)
        {
            colorButton.IsSelected = false;
        }
    }

    private void DestroyColorButtons()
    {
        for (int i = 0; i < colorButtons.Count; i++)
        {
            colorButtons[i].Select -= UnselectAll;
            Destroy(colorButtons[i].gameObject);
        }

        colorButtons.Clear();
    }
}
