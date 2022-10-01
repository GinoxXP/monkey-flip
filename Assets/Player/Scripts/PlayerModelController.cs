using UnityEngine;
using Zenject;
using static SkinData;

public class PlayerModelController : MonoBehaviour
{
    private GameObject currentModel;
    private SmoothJump smoothJump;
    private SkinData skinData;

    public bool SetMonkey(string tag)
    {
        foreach (var skin in skinData.skins)
        {
            if (skin.Name == tag)
            {
                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(skin.Model, transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
                return true;
            }
        }

        return false;
    }

    private void Start()
    {
        SetMonkey(CHIMPANZE_NAME);
    }

    [Inject]
    private void Init(SmoothJump smoothJump, SkinData skinData)
    {
        this.smoothJump = smoothJump;
        this.skinData = skinData;
    }
}
