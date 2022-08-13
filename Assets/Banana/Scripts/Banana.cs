using UnityEngine;
using Zenject;

public class Banana : MonoBehaviour
{
    public const string BANANA_COUNTER_KEY = "BananaCounter";

    private BananaView bananaView;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        int bananas = PlayerPrefs.GetInt(BANANA_COUNTER_KEY, 0);
        bananas++;
        PlayerPrefs.SetInt(BANANA_COUNTER_KEY, bananas);

        bananaView.BananaCounterChanged?.Invoke();
    }

    [Inject]
    private void Init(BananaView bananaView)
    {
        this.bananaView = bananaView;
    }
}
