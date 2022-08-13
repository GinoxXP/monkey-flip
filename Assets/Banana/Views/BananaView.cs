using UnityEngine;
using TMPro;
using System;

public class BananaView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text counter;

    public Action BananaCounterChanged;

    private void Start()
    {
        BananaCounterChanged += SetCounter;

        SetCounter();
    }

    private void SetCounter()
    {
        int bananas = PlayerPrefs.GetInt(Banana.BANANA_COUNTER_KEY, 0);

        var bananasText = string.Empty;

        if (bananas < 1000)
            bananasText = bananas.ToString();
        else if (bananas >= 1000 && bananas < 1000000)
            bananasText = $"{bananas / 1000}K";
        else if (bananas >= 1000000)
            bananasText = $"{bananas / 1000000}M";

        counter.text = bananasText;
    }
}
