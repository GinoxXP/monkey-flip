using TMPro;
using UnityEngine;
using Zenject;

public class WardrobeView : MonoBehaviour
{
    private DiContainer container;

    [SerializeField]
    private Transform canvas;
    [Space]
    [SerializeField]
    private SkinData skinData;
    [SerializeField]
    private TMP_Text skinNameText;
    [SerializeField]
    private Transform palettesParent;
    [SerializeField]
    private GameObject palettePanelPrefab;
    [SerializeField]
    private GameObject buySkinColorViewPrefab;

    private void SelectSkin(int index)
    {
        var skin = skinData.skins[index];
        skinData.skins.Add(new SkinData.Skin());
        skinNameText.text = skin.Name;

        foreach (var palette in skin.ColorPalettes)
        {
            var palettePanel = Instantiate(palettePanelPrefab, palettesParent);
            var paletteComponent = palettePanel.GetComponent<Pallete>();
            paletteComponent.Fill(palette);
            paletteComponent.OnBuyColor += OnColorBought;
        }
    }

    private void OnColorBought(SkinData.ColorSet colorSet)
    {
        var buySkinColorView = container.InstantiatePrefab(buySkinColorViewPrefab, canvas);
        var buySkinColorViewComponent = buySkinColorView.GetComponent<BuySkinColorView>();
        buySkinColorViewComponent.ColorSet = colorSet;
    }

    private void Start()
    {
        SelectSkin(0);
    }

    [Inject]
    private void Init(DiContainer container)
    {
        this.container = container;
    }
}
