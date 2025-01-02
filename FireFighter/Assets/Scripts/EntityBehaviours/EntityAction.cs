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
    [SerializeField] private bool useUnscaledTime;

    private bool _wasHolding;

    private bool _doDownAction;
    private bool _doHoldAction;
    private bool _doUpAction;

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

    public void DoDownAction()
    {
        if (canControl)
        {
            _doDownAction = true;
        }
    }

    public void DoHoldAction()
    {
        if (canControl)
        {
            _doHoldAction = true;
        }
    }

    public void DoUpAction()
    {
        if (canControl)
        {
            _doUpAction = true;
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
        if (_input == null || !canControl || (TimeManager.GetIsPaused() && !useUnscaledTime))
        {
            return false;
        }
        if (TimeManager.GetJustUnpaused() && !useUnscaledTime)
        {
            if ((!_wasHolding && _input.buttonHold && (interaction == InteractionType.down || interaction == InteractionType.hold)))
            {
                return true;
            }
            if ((_wasHolding && !_input.buttonHold && interaction == InteractionType.up))
            {
                return true;
            }
        }
        _wasHolding = _input.buttonHold;
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
        if (_doDownAction)
        {
            _doDownAction = false;
            return true;
        }
        if ((_input == null && !_doDownAction) || !canControl || (TimeManager.GetIsPaused() && !useUnscaledTime))
        {
            return false;
        }
        return _input.buttonDown;
    }

    protected bool GetButtonHold()
    {
        if (_doHoldAction)
        {
            _doHoldAction = false;
            return true;
        }
        if ((_input == null && !_doHoldAction) || !canControl || (TimeManager.GetIsPaused() && !useUnscaledTime))
        {
            return false;
        }
        return _input.buttonHold;
    }

    protected bool GetButtonUp()
    {
        if (_doUpAction)
        {
            _doUpAction = false;
            return true;
        }
        if ((_input == null && !_doUpAction) || !canControl || (TimeManager.GetIsPaused() && !useUnscaledTime))
        {
            return false;
        }
        return _input.buttonUp;
    }
}
