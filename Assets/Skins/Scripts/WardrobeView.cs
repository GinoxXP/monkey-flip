using System.Collections.Generic;
using System.Linq;
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
    private Transform skinsParent;
    [SerializeField]
    private GameObject buyViewPrefab;
    [SerializeField]
    private GameObject skinButtonPrefab;

    private List<SkinButton> skinButtons = new();

    private void FillSkins()
    {
        skinButtons.Clear();

        foreach (var skin in skinData.skins)
        {
            var skinButton = Instantiate(skinButtonPrefab, skinsParent);
            var skinButtonComponent = skinButton.GetComponent<SkinButton>();
            skinButtonComponent.Skin = skin;
            //skinButtonComponent.OnSelect += OnSkinButtonClicked;

            skinButtons.Add(skinButtonComponent);
        }

        UpdateSkinButtons();
    }

    private void ClearSkins()
    {
        while (skinButtons.Count > 0)
        {
            var skinButton = skinButtons.First();
            //skinButton.OnSelect -= OnSkinButtonClicked;

            skinButtons.Remove(skinButton);
            Destroy(skinButton.gameObject);
        }

        skinButtons.Clear();
    }

    //private void OnSkinBought(Skin skin)
    //{
    //    var buyView = container.InstantiatePrefab(buyViewPrefab, canvas);
    //    var buyViewComponent = buyView.GetComponent<BuyView>();
    //    buyViewComponent.Cost = skin.Cost;
    //    buyViewComponent.OnBought += () =>
    //    {
    //        skin.IsBought = true;
    //        UpdateView();
    //    };
    //}

    //private void OnSkinButtonClicked(Skin skin)
    //{
    //    if (skin.IsBought)
    //    {
    //        skinController.SetSkin(skin);
    //        UpdateView();        
    //    }
    //    else
    //    {
    //        OnSkinBought(skin);
    //    }
    //}

    private void UpdateSkinButtons()
    {
        foreach (var skinButton in skinButtons)
        {
            skinButton.IsLocked = !skinButton.Skin.IsBought;
            skinButton.IsSelected = skinButton.Skin.IsSelected;
        }
    }

    private void UpdateView()
    {
        ClearSkins();
        FillSkins();
    }

    private void Start()
    {
        UpdateView();
    }

    [Inject]
    private void Init(DiContainer container, SkinController skinController, SkinData skinData)
    {
        this.container = container;
        this.skinController = skinController;
        this.skinData = skinData;
    }
}
