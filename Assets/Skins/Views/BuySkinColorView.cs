using TMPro;
using UnityEngine;
using Zenject;
using static SkinData;

public class BuySkinColorView : MonoBehaviour
{
    private BananaBalanceManager bananaBalanceManager;

    [SerializeField]
    private TMP_Text textComponent;
    [SerializeField]
    private string message;

    public ColorSet ColorSet { get; set; }

    public void Buy()
    {
        if (bananaBalanceManager.Remove(ColorSet.Cost))
        {
            ColorSet.IsBought = true;
            Close();
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        textComponent.text = string.Format(message, ColorSet.Cost);
    }

    [Inject]
    private void Init(BananaBalanceManager bananaBalanceManager)
    {
        this.bananaBalanceManager = bananaBalanceManager;
    }
}
