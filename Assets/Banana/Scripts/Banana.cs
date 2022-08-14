using UnityEngine;
using Zenject;

public class Banana : MonoBehaviour
{
    private BananaBalanceManager bananaBalanceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
            Destroy(gameObject);
        }
    }

    private void PickUp()
    {
        bananaBalanceManager.Add();
    }

    [Inject]
    private void Init(BananaBalanceManager bananaBalanceManager)
    {
        this.bananaBalanceManager = bananaBalanceManager;
    }
}
