using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuList : UIMenu
{
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();

    protected override void SelectFirstButton()
    {
        SetCurrentButton(buttons[0]);
    }

    public override void UnselectButtons()
    {
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<Animator>().GetComponent<InvokeAfterSwitch>().SetValueFalse();
        }
    }

    public override void BlockButtons(bool value)
    {
        foreach (var button in buttons)
        {
            button.GetComponent<InvokeAfterSwitch>().SetValue(!value);
        }
    }

    public void SelectNextButton()
    {
        int nextButton = buttons.IndexOf(currentButton) + 1;
        int index = nextButton >= buttons.Count ? 0 : nextButton;
        SetCurrentButton(buttons[index]);
    }

    public void SelectPreviousButton()
    {
        int nextButton = buttons.IndexOf(currentButton) - 1;
        int index = nextButton < 0 ? buttons.Count - 1 : nextButton;
        SetCurrentButton(buttons[index]);
    }
}
