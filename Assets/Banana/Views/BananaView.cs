using UnityEngine;
using TMPro;
using Zenject;

public class BananaView : MonoBehaviour
{
    private BananaBalance bananaBalanceManager;

    [SerializeField]
    private TMP_Text counter;

    private void Start()
    {
        bananaBalanceManager.BalanceChanged += SetCounter;

        SetCounter();
    }

    private void SetCounter()
    {
        var bananas = bananaBalanceManager.Balance;

        var bananasText = string.Empty;

        if (bananas < 1000)
            bananasText = bananas.ToString();
        else if (bananas >= 1000 && bananas < 1000000)
            bananasText = $"{bananas / 1000}K";
        else if (bananas >= 1000000)
            bananasText = $"{bananas / 1000000}M";

        counter.text = bananasText;
    }

    [Inject]
    private void Init(BananaBalance bananaBalanceManager)
    {
        this.bananaBalanceManager = bananaBalanceManager;
    }
}
