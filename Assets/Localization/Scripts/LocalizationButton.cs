using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationButton : MonoBehaviour
{
    private const string LOCALIZATION_KEY = "SELECTED_LOCALIZATION";

    [SerializeField]
    private string localizationCode;
    private void SetLocale(string localeKey)
    => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.Formatter.ToString() == localeKey).FirstOrDefault();

    public void Click()
    {
        PlayerPrefs.SetString(LOCALIZATION_KEY, localizationCode);
        SetLocale(localizationCode);
    }
}
