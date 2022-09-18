using System;
using UnityEngine;

public class BananaBalanceManager : MonoBehaviour
{
    public const string BANANA_COUNTER_KEY = "BananaCounter";

    public event Action BalanceChanged;

    public int Balance { get => PlayerPrefs.GetInt(BANANA_COUNTER_KEY, 0); }

    public void Add(int value = 1)
    {
        var balance = Balance + value;
        PlayerPrefs.SetInt(BANANA_COUNTER_KEY, balance);

        BalanceChanged?.Invoke();
    }

    public bool CanRemove(int value)
       => Balance >= value;

    public bool Remove(int value)
    {
        if (!CanRemove(value))
            return false;

        var balance = Balance - value;
        PlayerPrefs.SetInt(BANANA_COUNTER_KEY, balance);

        BalanceChanged?.Invoke();

        return true;
    }
}
