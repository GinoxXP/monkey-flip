using System.Linq;
using UnityEngine;
using Zenject;
using static SkinData;

public class SkinController : MonoBehaviour
{
    private GameObject currentModel;
    private SmoothJump smoothJump;
    private SkinData skinData;
    private Skin currentSkin;

    public Skin SelectedSkin => currentSkin;

    public void SetSkin(Skin newSkin)
    {
        foreach (var skin in skinData.skins)
        {
            if (skin == newSkin)
            {
                currentSkin = skin;

                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(skin.Model, smoothJump.transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
            }
            else
            {
                skin.IsSelected = false;
            }
        }
    }

    public void SetColor(ColorSet newColorSet, ColorPalette colorPalette, Material targetMaterial)
    {
        foreach (var colorSet in colorPalette.ColorSets)
        {
            if (colorSet == newColorSet)
            {
                targetMaterial.color = newColorSet.Color;
                colorSet.IsSelected = true;
            }
            else
            {
                colorSet.IsSelected = false;
            }
        }
    }

    private void Start()
    {
        SetSkin(skinData.skins.First());
    }

    [Inject]
    private void Init(SmoothJump smoothJump, SkinData skinData)
    {
        this.smoothJump = smoothJump;
        this.skinData = skinData;
    }
}
