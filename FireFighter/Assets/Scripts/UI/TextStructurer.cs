using TMPro;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class TextStruct : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private string displayText;
    [SerializeField] private bool setOnChangeDisplayText = true;

    [SerializeField] private StringArrayVariable stringArray;

    public void SetTMP()
    {
        if (tmp != null)
        {
            tmp.text = displayText;
        }
    }

    public void LocalizeDisplayText(TextLocalize value)
    {
        value.LocalizeText(displayText);
    }

    private void CheckSetOnChangeDisplayText()
    {
        if (setOnChangeDisplayText)
        {
            SetTMP();
        }
    }

    public void SetDisplayText(string value)
    {
        displayText = value;
        CheckSetOnChangeDisplayText();
    }

    public string GetDisplayText()
    {
        return displayText;
    }

    public void SetDisplayTextByStringArray(int value)
    {
        if (stringArray != null)
        {
            displayText = stringArray.Value[value];
            CheckSetOnChangeDisplayText();
        }
    }

    public void SetDisplayTextByStringArray(IntVariable value)
    {
        SetDisplayTextByStringArray(value.Value);
    }

    public void CleanDisplayText()
    {
        displayText = "";
        CheckSetOnChangeDisplayText();
    }

    public void AddToDisplayText(string value)
    {
        displayText += value;
        CheckSetOnChangeDisplayText();
    }

    public void AddToDisplayText(InvokeAfterCounter value)
    {
        AddToDisplayText(value.GetCurrentValue().ToString());
    }

    public void AddCounterToDisplayText(GameObject value)
    {
        InvokeAfterCounter counter = value.GetComponent<InvokeAfterCounter>();
        if (counter != null)
        {
            AddToDisplayText(counter.GetCurrentValue().ToString());
        }
    }

    public void AddCounterToDisplayText(GameObjectVariable value)
    {
        AddCounterToDisplayText(value.Value);
    }
}
