using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

public class LocalizationDropdown : MonoBehaviour
{
    private PauseController pauseController;

    [SerializeField]
    private TMP_Dropdown dropdown;

    public void OnDropdownValueChanged(int index)
    {
        var selectedOption = dropdown.options[index];
        SetLocale(selectedOption.text);
    }

    private void OnPauseChanged(bool isPause)
        => gameObject.SetActive(isPause);


    private void SetLocale(string localeKey)
        => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.Formatter.ToString() == localeKey).FirstOrDefault();

    [Inject]
    private void Init(PauseController pauseController)
    {
        this.pauseController = pauseController;
        pauseController.PauseChanged += OnPauseChanged;
    }
}
