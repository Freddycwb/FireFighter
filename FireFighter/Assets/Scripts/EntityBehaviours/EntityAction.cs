using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EntityAction : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInputAction _input;

    public enum InteractionType
    {
        down,
        hold,
        up
    }
    [SerializeField] protected InteractionType interaction;

    [SerializeField] private bool canControl = true;
    private bool _doAction;

    public Action onActionTrigger;
    public Action onCanControl;
    public Action onCantControl;

    public void SetCanControl(bool value)
    {
        if (value && !canControl)
        {
            if (onCanControl != null)
            {
                onCanControl.Invoke();
            }
        }
        else if (!value && canControl)
        {
            if (onCantControl != null)
            {
                onCantControl.Invoke();
            }
        }
        canControl = value;
    }

    public void DoAction()
    {
        if (canControl)
        {
            _doAction = true;
        }
    }

    private void Start()
    {
        if (input == null && GetComponent<IInputAction>() != null)
        {
            _input = GetComponent<IInputAction>();
        }
        else if (input != null && input.GetComponent<IInputAction>() != null)
        {
            _input = input.GetComponent<IInputAction>();
        }
    }

    public virtual void Update()
    {
        if (GetButton())
        {
            if (onActionTrigger != null)
            {
                onActionTrigger.Invoke();
            }
        }
    }

    protected bool GetButton()
    {
        if (_doAction)
        {
            _doAction = false;
            return true;
        }
        if (_input == null || !canControl)
        {
            return false;
        }
        switch (interaction)
        {
            case InteractionType.down:
                return _input.buttonDown;
            case InteractionType.hold:
                return _input.buttonHold;
            case InteractionType.up:
                return _input.buttonUp;
            default:
                return false;
        }
    }

    protected bool GetButtonDown()
    {
        if (_input == null || !canControl)
        {
            return false;
        }
        return _input.buttonDown;
    }

    protected bool GetButtonHold()
    {
        if (_input == null || !canControl)
        {
            return false;
        }
        return _input.buttonHold;
    }

    protected bool GetButtonUp()
    {
        if (_input == null || !canControl)
        {
            return false;
        }
        return _input.buttonUp;
    }
}
