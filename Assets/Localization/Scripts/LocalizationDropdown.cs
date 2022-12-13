using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

public class LocalizationDropdown : MonoBehaviour
{
    private const string LOCALIZATION_KEY = "SELECTED_LOCALIZATION";

    private PauseController pauseController;

    [SerializeField]
    private TMP_Dropdown dropdown;

    public void OnDropdownValueChanged(int index)
    {
        var selectedOption = dropdown.options[index];
        PlayerPrefs.SetInt(LOCALIZATION_KEY, index);
        SetLocale(selectedOption.text);
    }

    private void OnPauseChanged(bool isPause)
        => gameObject.SetActive(isPause);


    private void SetLocale(string localeKey)
        => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Where(x => x.Formatter.ToString() == localeKey).FirstOrDefault();

    private void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(LOCALIZATION_KEY, 0);
    }

    [Inject]
    private void Init(PauseController pauseController)
    {
        this.pauseController = pauseController;
        pauseController.PauseChanged += OnPauseChanged;
    }
}
