using TMPro;
using UnityEngine;

public class TextStruct : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private string displayText;
    [SerializeField] private bool setOnChangeDisplayText = true;

    public void SetTMP()
    {
        tmp.text = displayText;
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
