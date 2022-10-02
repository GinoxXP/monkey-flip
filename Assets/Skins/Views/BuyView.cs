using System;
using TMPro;
using UnityEngine;
using Zenject;
using static SkinData;

public class BuyView : MonoBehaviour
{
    private BananaBalanceManager bananaBalanceManager;

    [SerializeField]
    private TMP_Text textComponent;
    [SerializeField]
    private string message;

    public Action OnBought { get; set; }

    public int Cost { get; set; }

    public void Buy()
    {
        if (bananaBalanceManager.Remove(Cost))
        {
            OnBought?.Invoke();
            Close();
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        textComponent.text = string.Format(message, Cost);
    }

    [Inject]
    private void Init(BananaBalanceManager bananaBalanceManager)
    {
        this.bananaBalanceManager = bananaBalanceManager;
    }
}
