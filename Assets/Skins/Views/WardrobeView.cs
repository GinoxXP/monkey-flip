using System.Collections.Generic;
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

    private List<SkinButton> skinButtons = new();
    private List<Palette> colorPalettes = new();

    private void FillPalettes()
    {
        foreach (var palette in skinController.SelectedSkin.ColorPalettes)
        {
            var palettePanel = Instantiate(palettePanelPrefab, palettesParent);
            var paletteComponent = palettePanel.GetComponent<Palette>();
            paletteComponent.Fill(palette);
            paletteComponent.OnSelect += OnColorSetButtonClicked;

            colorPalettes.Add(paletteComponent);
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

            skinButtons.Add(skinButtonComponent);
        }
    }

    private void OnColorBought(ColorSet colorSet)
    {
        var buyView = container.InstantiatePrefab(buyViewPrefab, canvas);
        var buyViewComponent = buyView.GetComponent<BuyView>();
        buyViewComponent.OnBought += () =>
        {
            colorSet.IsBought = true;
            UpdateColorPalettes();
        };
    }

    private void OnSkinBought(Skin skin)
    {
        var buyView = container.InstantiatePrefab(buyViewPrefab, canvas);
        var buyViewComponent = buyView.GetComponent<BuyView>();
        buyViewComponent.OnBought += () =>
        {
            skin.IsBought = true;
            UpdateSkinButtons();
        };
    }

    private void OnColorSetButtonClicked(ColorSet colorSet, ColorPalette colorPalette, Material targetMaterial)
    {
        if (colorSet.IsBought)
        {
            skinController.SetColor(colorSet, colorPalette, targetMaterial);
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
            UpdateSkinButtons();
        }
        else
        {
            OnSkinBought(skin);
        }
    }

    private void UpdateColorPalettes()
    {
        foreach (var palette in colorPalettes)
        {
            palette.UpdateColorButtons();
        }
    }

    private void UpdateSkinButtons()
    {
        foreach (var skinButton in skinButtons)
        {
            skinButton.IsLocked = !skinButton.Skin.IsBought;
            skinButton.IsSelected = skinButton.Skin.IsSelected;
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
