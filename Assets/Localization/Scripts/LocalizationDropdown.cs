using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;

    public void OnDropdownValueChanged(int index)
    {
        var selectedOption = dropdown.options[index];
        SetLocale(selectedOption.text);
    }

    private void SetLocale(string localeKey)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.Formatter.ToString() == localeKey).FirstOrDefault();
    }
}
