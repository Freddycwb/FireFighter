using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] protected GameObject currentButton;

    private void OnEnable()
    {
        BlockButtons(false);
        SelectFirstButton();
    }

    protected virtual void SelectFirstButton()
    {

    }

    public void SetCurrentButton(GameObject value)
    {
        if (!enabled)
        {
            return;
        }
        UnselectButtons();
        currentButton = value;
        currentButton.GetComponentInChildren<Animator>().GetComponent<InvokeAfterSwitch>().SetValueTrue();
    }

    public virtual void UnselectButtons()
    {

    }

    public void ClickButton()
    {
        if (!enabled)
        {
            return;
        }
        currentButton.GetComponent<InvokeAfterBoolComparison>().Compare(currentButton.GetComponent<InvokeAfterSwitch>().GetValue());
    }

    public virtual void BlockButtons(bool value)
    {

    }

    private void OnDisable()
    {
        UnselectButtons();
        BlockButtons(true);
    }
}
