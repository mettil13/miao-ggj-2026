using UnityEngine;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
    public void ChangeToEnglish() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    public void ChangeToItalian() {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }


}
