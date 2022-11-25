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

    public Skin CurrentSkin => currentSkin;

    public void SetSkin(Skin newSkin, bool isPreview = false)
    {
        foreach (var skin in skinData.skins)
        {
            if (skin == newSkin)
            {
                currentSkin = skin;

                if (!isPreview)
                    skin.IsSelected = true;

                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(skin.Model, smoothJump.transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
            }
            else
            {
                if (!isPreview)
                    skin.IsSelected = false;
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
