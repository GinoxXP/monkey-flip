using TMPro;
using UnityEngine;
using Zenject;
using static SkinData;

public class WardrobeView : MonoBehaviour
{
    private DiContainer container;
    private SkinController skinController;
    private SkinData skinData;

    [SerializeField]
    private Transform canvas;
    [Space]
    [SerializeField]
    private TMP_Text skinNameText;
    [SerializeField]
    private Transform palettesParent;
    [SerializeField]
    private Transform skinsParent;
    [SerializeField]
    private GameObject palettePanelPrefab;
    [SerializeField]
    private GameObject buySkinColorViewPrefab;
    [SerializeField]
    private GameObject skinButtonPrefab;

    private void FillPalettes()
    {
        foreach (var palette in skinController.SelectedSkin.ColorPalettes)
        {
            var palettePanel = Instantiate(palettePanelPrefab, palettesParent);
            var paletteComponent = palettePanel.GetComponent<Pallete>();
            paletteComponent.Fill(palette);
            paletteComponent.OnBuyColor += OnColorBought;
        }
    }

    private void FillSkins()
    {
        foreach (var skin in skinData.skins)
        {
            var skinButton = Instantiate(skinButtonPrefab, skinsParent);
            var skinButtonComponent = skinButton.GetComponent<SkinButton>();
            skinButtonComponent.Icon = skin.Icon;
        }
    }

    private void OnColorBought(ColorSet colorSet)
    {
        var buySkinColorView = container.InstantiatePrefab(buySkinColorViewPrefab, canvas);
        var buySkinColorViewComponent = buySkinColorView.GetComponent<BuySkinColorView>();
        buySkinColorViewComponent.ColorSet = colorSet;
    }

    private void Start()
    {
        FillPalettes();
        FillSkins();
    }

    [Inject]
    private void Init(DiContainer container, SkinController skinController, SkinData skinData)
    {
        this.container = container;
        this.skinController = skinController;
        this.skinData = skinData;
    }
}
