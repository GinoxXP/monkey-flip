using System.ComponentModel;
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

    public bool SetSkin(string tag)
    {
        foreach (var skin in skinData.skins)
        {
            if (skin.Name == tag)
            {
                selectedSkin = skin;

                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(skin.Model, smoothJump.transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
                return true;
            }
        }

        return false;
    }

    public void SetColor(ColorSet colorSet, Material targetMaterial)
    {
        targetMaterial.color = colorSet.Color;
        colorSet.IsSelected = true;
    }

    private void Start()
    {
        SetSkin(CHIMPANZE_NAME);
    }

    [Inject]
    private void Init(SmoothJump smoothJump, SkinData skinData)
    {
        this.smoothJump = smoothJump;
        this.skinData = skinData;
    }
}
