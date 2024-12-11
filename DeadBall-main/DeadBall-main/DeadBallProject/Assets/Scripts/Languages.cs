using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Languages : MonoBehaviour
{
    public enum Language
    {
        English,
        Polish,
        Spanish
    }

    [System.Serializable]
    public class LanguageData
    {
        public Language language;
        public Sprite languageIcon;
        public List<TextMeshProUGUI> textsToTranslate;
        public List<string> translations;
    }

    public List<LanguageData> languages;
    public Image languageIcon;
    private int currentLanguageIndex;

    void Start()
    {
        // Ustaw domy�lny j�zyk na angielski
        currentLanguageIndex = 0;
        UpdateLanguage();
    }

    public void SwitchLanguage()
    {
        // Zmie� indeks j�zyka na kolejny
        currentLanguageIndex = (currentLanguageIndex + 1) % languages.Count;
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        LanguageData selectedLanguage = languages[currentLanguageIndex];

        // Zmie� ikon� j�zyka
        if (languageIcon != null)
        {
            languageIcon.sprite = selectedLanguage.languageIcon;
        }

        // Zaktualizuj wszystkie teksty
        for (int i = 0; i < selectedLanguage.textsToTranslate.Count; i++)
        {
            if (i < selectedLanguage.translations.Count)
            {
                selectedLanguage.textsToTranslate[i].text = selectedLanguage.translations[i];
            }
        }
    }
}
