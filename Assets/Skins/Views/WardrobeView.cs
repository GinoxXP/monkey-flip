using TMPro;
using UnityEngine;

public class WardrobeView : MonoBehaviour
{
    [SerializeField]
    private SkinData skinData;
    [SerializeField]
    private TMP_Text skinNameText;
    [SerializeField]
    private Transform palettesParent;
    [SerializeField]
    private GameObject palettePanelPrefab;

    private void SelectSkin(int index)
    {
        var skin = skinData.skins[index];

        skinNameText.text = skin.Name;

        foreach (var palette in skin.ColorPalettes)
        {
            var palettePanel = Instantiate(palettePanelPrefab, palettesParent);
            var paletteComponent = palettePanel.GetComponent<Pallete>();
            paletteComponent.Fill(palette);
        }
    }

    private void Start()
    {
        SelectSkin(0);
    }
}
