using TMPro;
using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class TextStruct : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private string displayText;
    [SerializeField] private bool setOnChangeDisplayText = true;

    [SerializeField] private StringArrayVariable stringArray;

    private TextLocalize textLocalize;
    private bool localizeOnUpdateL;

    private void Awake()
    {
        Localization.updateL += OnEnable;
    }

    private void OnEnable()
    {
        if (localizeOnUpdateL)
        {
            LocalizeDisplayText(textLocalize);
        }
    }

    public void SetTMP()
    {
        if (tmp != null)
        {
            tmp.text = displayText;
        }
    }

    public void LocalizeDisplayText(TextLocalize value)
    {
        textLocalize = value;
        textLocalize.LocalizeText(displayText);
        localizeOnUpdateL = true;
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
        localizeOnUpdateL = false;
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
            localizeOnUpdateL = false;
        }
    }

    public void SetDisplayTextByStringArray(IntVariable value)
    {
        SetDisplayTextByStringArray(value.Value);
    }

    public void CleanDisplayText()
    {
        displayText = "";
        localizeOnUpdateL = false;
        CheckSetOnChangeDisplayText();
    }

    public void AddToDisplayText(string value)
    {
        displayText += value;
        CheckSetOnChangeDisplayText();
        localizeOnUpdateL = false;
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
