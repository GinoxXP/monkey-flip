using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
using static SkinData;

public class WardrobeController : MonoBehaviour
{
    private SkinController skinController;
    private SkinData skinData;
    private BananaBalanceManager balanceManager;

    [SerializeField]
    private GameObject selectButton;
    [SerializeField]
    private GameObject selectedButtonPlaceholder;
    [SerializeField]
    private GameObject buyButton;
    [Space]
    [SerializeField]
    private TMP_Text skinNameText;
    [SerializeField]
    private Transform skinsParent;
    [SerializeField]
    private GameObject skinButtonPrefab;
    [SerializeField]
    private TMP_Text priceText;

    private List<SkinButton> skinButtons = new();
    private Skin lastSetedSkin;
    private Skin lastSelectedSkin;

    private enum ButtonType
    {
        Select,
        Selected,
        Buy,
    }

    public void Open()
    {
        UpdateView();

        foreach (var skin in skinData.skins)
        {
            if (skin.IsSelected)
            {
                lastSetedSkin = skin;
                break;
            }
        }
    }

    public void Close()
    {
        skinController.SetSkin(lastSetedSkin);
        gameObject.SetActive(false);
    }

    public void BuySkin()
    {
        if (balanceManager.CanRemove(lastSelectedSkin.Cost))
        {
            balanceManager.Remove(lastSelectedSkin.Cost);
            lastSelectedSkin.IsBought = true;
            UpdateSelectedSkinButton();
        }
    }

    private void FillSkins()
    {
        skinButtons.Clear();

        foreach (var skin in skinData.skins)
        {
            var skinButton = Instantiate(skinButtonPrefab, skinsParent);
            var skinButtonComponent = skinButton.GetComponent<SkinButton>();
            skinButtonComponent.Skin = skin;
            skinButtonComponent.OnSelect += OnSkinButtonClicked;

            if (skin.IsSelected)
            {
                skinNameText.text = skin.Name;
                skinButtonComponent.IsSelected = true;
            }

            skinButtons.Add(skinButtonComponent);
        }
    }

    private void ClearSkins()
    {
        while (skinButtons.Count > 0)
        {
            var skinButton = skinButtons.First();
            skinButton.OnSelect -= OnSkinButtonClicked;

            skinButtons.Remove(skinButton);
            Destroy(skinButton.gameObject);
        }

        skinButtons.Clear();
    }

    private void OnSkinButtonClicked(SkinButton skinButton)
    {
        foreach (var button in skinButtons)
            button.IsSelected = false;

        skinButton.IsSelected = true;

        skinController.SetSkin(skinButton.Skin, true);
        lastSelectedSkin = skinButton.Skin;
        SetActiveButton(skinButton.Skin);
        skinNameText.text = skinButton.Skin.Name;
    }

    private void SetActiveButton(Skin skin)
    {
        if (skin.IsSelected)
        {
            selectButton.SetActive(false);
            selectedButtonPlaceholder.SetActive(true);
            buyButton.SetActive(false);
        }
        else if (skin.IsBought)
        {
            selectButton.SetActive(true);
            selectedButtonPlaceholder.SetActive(false);
            buyButton.SetActive(false);
        }
        else
        {
            selectButton.SetActive(false);
            selectedButtonPlaceholder.SetActive(false);
            buyButton.SetActive(true);
            priceText.text = skin.Cost.ToString();
        }
    }

    private void UpdateView()
    {
        ClearSkins();
        FillSkins();

        foreach (var skinButton in skinButtons)
        {
            if (skinButton.Skin.IsSelected)
                SetActiveButton(skinButton.Skin);
        }
    }

    private void UpdateSelectedSkinButton()
    {
        SetActiveButton(lastSelectedSkin);
    }

    [Inject]
    private void Init(SkinController skinController, SkinData skinData, BananaBalanceManager balanceManager)
    {
        this.skinController = skinController;
        this.skinData = skinData;
        this.balanceManager = balanceManager;
    }
}
