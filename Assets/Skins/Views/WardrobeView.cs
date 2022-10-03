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
    private GameObject buyViewPrefab;
    [SerializeField]
    private GameObject skinButtonPrefab;

    private void FillPalettes()
    {
        foreach (var palette in skinController.SelectedSkin.ColorPalettes)
        {
            var palettePanel = Instantiate(palettePanelPrefab, palettesParent);
            var paletteComponent = palettePanel.GetComponent<Pallete>();
            paletteComponent.Fill(palette);
            paletteComponent.OnSelect += OnColorSetButtonClicked;
        }
    }

    private void FillSkins()
    {
        foreach (var skin in skinData.skins)
        {
            var skinButton = Instantiate(skinButtonPrefab, skinsParent);
            var skinButtonComponent = skinButton.GetComponent<SkinButton>();
            skinButtonComponent.Skin = skin;
            skinButtonComponent.OnSelect += OnSkinButtonClicked;
        }
    }

    private void OnColorBought(ColorSet colorSet)
    {
        var buyView = container.InstantiatePrefab(buyViewPrefab, canvas);
        var buyViewComponent = buyView.GetComponent<BuyView>();
        buyViewComponent.OnBought += () => colorSet.IsBought = true;
    }

    private void OnSkinBought(Skin skin)
    {
        var buyView = container.InstantiatePrefab(buyViewPrefab, canvas);
        var buyViewComponent = buyView.GetComponent<BuyView>();
        buyViewComponent.OnBought += () => skin.IsBought = true;
    }

    private void OnColorSetButtonClicked(ColorSet colorSet, Material targetMaterial)
    {
        if (colorSet.IsBought)
        {
            skinController.SetColor(colorSet, targetMaterial);
        }
        else
        {
            OnColorBought(colorSet);
        }
    }

    private void OnSkinButtonClicked(Skin skin)
    {
        if (skin.IsBought)
        {
            skinController.SetSkin(skin);
        }
        else
        {
            OnSkinBought(skin);
        }
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
