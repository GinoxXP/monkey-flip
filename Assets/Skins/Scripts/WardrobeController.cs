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

    private List<SkinButton> skinButtons = new();
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
                lastSelectedSkin = skin;
                break;
            }
        }
    }

    public void Close()
    {
        skinController.SetSkin(lastSelectedSkin);
        gameObject.SetActive(false);
    }

    public void SelectSkin()
    {
        UpdateSelectedSkinButton();
        lastSelectedSkin = skinData.skins.Where(x => x.IsSelected).FirstOrDefault();
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
        SetActiveButton(skinButton.Skin);
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
        }
    }

    private void UpdateView()
    {
        ClearSkins();
        FillSkins();
        UpdateSelectedSkinButton();
    }

    private void UpdateSelectedSkinButton()
    {
        foreach (var skinButton in skinButtons)
        {
            skinButton.IsSelected = skinButton.Skin.IsSelected;
            if (skinButton.Skin.IsSelected)
                SetActiveButton(skinButton.Skin);
        }
    }

    [Inject]
    private void Init(SkinController skinController, SkinData skinData)
    {
        this.skinController = skinController;
        this.skinData = skinData;
    }
}
