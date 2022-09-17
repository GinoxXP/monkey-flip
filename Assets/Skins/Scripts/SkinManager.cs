using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public bool ItemIsBought(string key)
        => PlayerPrefs.GetInt(key, 0) == 1;
}
