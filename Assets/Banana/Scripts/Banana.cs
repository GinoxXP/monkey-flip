using UnityEngine;
using Zenject;

public class Banana : MonoBehaviour
{
    private BananaBalance bananaBalanceManager;
    private Monkey monkey;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Monkey>(out _))
        {
            PickUp();
            Destroy(gameObject);
        }
    }

    private void PickUp()
    {
        monkey.Yey();
        bananaBalanceManager.Add();
    }

    [Inject]
    private void Init(BananaBalance bananaBalanceManager, Monkey monkey)
    {
        this.bananaBalanceManager = bananaBalanceManager;
        this.monkey = monkey;
    }
}
