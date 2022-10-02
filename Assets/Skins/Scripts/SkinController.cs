using System.Linq;
using UnityEngine;
using Zenject;
using static SkinData;

public class SkinController : MonoBehaviour
{
    private GameObject currentModel;
    private SmoothJump smoothJump;
    private SkinData skinData;
    private Skin selectedSkin;

    public Skin SelectedSkin => selectedSkin;

    public void SetSkin(Skin newSkin)
    {
        foreach (var skin in skinData.skins)
        {
            if (skin == newSkin)
            {
                selectedSkin = skin;

                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(skin.Model, smoothJump.transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
            }
        }
    }

    public void SetColor(ColorSet colorSet, Material targetMaterial)
    {
        targetMaterial.color = colorSet.Color;
        colorSet.IsSelected = true;
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
