using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    [SerializeField]
    private GameObject languageBox;

    public void Click()
    {
        languageBox.SetActive(!languageBox.activeSelf);
    }
}
