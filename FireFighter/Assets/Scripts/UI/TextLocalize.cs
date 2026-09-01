using UnityEngine;
using TMPro;

public class TextLocalize : MonoBehaviour
{
    private bool isInUI;

    private TextMeshProUGUI _TMProUIText;
    private TextMeshPro _TMProText;

    [SerializeField] private string _key;
    [SerializeField] private bool richTextTag;
    [SerializeField] private TextGradual textGradual;


    private void Awake()
    {
        if (GetComponent<TextMeshProUGUI>() != null)
        {
            _TMProUIText = GetComponent<TextMeshProUGUI>();
            isInUI = true;
        }
        else
        {
            _TMProText = GetComponent<TextMeshPro>();
        }
        Localization.updateL += OnEnable;
    }
    private void OnEnable()
    {
        if (_key == "")
        {
            return;
        }
        if (isInUI)
        {
            _TMProUIText.text = richTextTag ? "<line-height=100%>" + Localization.LocalizeText(_key) : Localization.LocalizeText(_key);
        }
        else
        {
            _TMProText.text = richTextTag ? "<line-height=100%>" + Localization.LocalizeText(_key) : Localization.LocalizeText(_key);
        }
    }

    public void ChangeText()
    {
        if (_key == "")
        {
            if (isInUI)
            {
                _TMProUIText.text = "";
            }
            else
            {
                _TMProText.text = "";
            }
            return;
        }
        string text = richTextTag ? "<line-height=100%>" + Localization.LocalizeText(_key) : Localization.LocalizeText(_key);
        if (textGradual == null)
        {
            if (isInUI)
            {
                _TMProUIText.text = text;
            }
            else
            {
                _TMProText.text = text;
            }
        }
        else
        {
            textGradual.CallType(Localization.LocalizeText(_key));
        }
    }

    public void LocalizeText(string value)
    {
        if (value == "")
        {
            if (isInUI)
            {
                _TMProUIText.text = "";
            }
            else
            {
                _TMProText.text = "";
            }
            return;
        }
        string text = richTextTag ? "<line-height=100%>" + Localization.LocalizeText(value) : Localization.LocalizeText(value);
        if (textGradual == null)
        {
            if (isInUI)
            {
                _TMProUIText.text = text;
            }
            else
            {
                _TMProText.text = text;
            }
        }
        else
        {
            textGradual.CallType(Localization.LocalizeText(value));
        }
    }
}