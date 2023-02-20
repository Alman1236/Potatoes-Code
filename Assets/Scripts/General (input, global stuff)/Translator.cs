using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumsAndStructs;

public abstract class Translator : MonoBehaviour
{
    protected Dictionary<TextMeshProUGUI, textsNames> textsInTheScene = new Dictionary<TextMeshProUGUI, textsNames>();

    virtual protected void Awake()
    {
        OnChangingLanguage();
    }

    public void OnChangingLanguage()
    {
        foreach(var item in textsInTheScene)
        {
            item.Key.text = GameTexts.GetTranslatedText(item.Value);   
        }
    }
}
