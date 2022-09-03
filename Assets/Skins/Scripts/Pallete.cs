using TMPro;
using UnityEngine;
using static SkinData;

public class Pallete : MonoBehaviour
{
    [SerializeField]
    private TMP_Text palleteTitle;
    [SerializeField]
    private Transform colorsParent;
    [SerializeField]
    private GameObject colorButtonPrefab;

    public void Fill(ColorPalette colorPalettes)
    {
        palleteTitle.text = colorPalettes.Name;

        foreach (var colorSet in colorPalettes.Colors)
        {
            var colorButton = Instantiate(colorButtonPrefab, colorsParent);
            var colorButtonComponent = colorButton.GetComponent<ColorButton>();

            colorButtonComponent.Color = colorSet.Color;
            colorButtonComponent.TargetMaterial = colorPalettes.TargetMaterial;
        }
    }
}
