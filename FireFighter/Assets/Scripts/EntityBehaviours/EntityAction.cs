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
    [SerializeField] private float bufferTime;
    private float _currentBufferTime;
    private bool _bufferCanControl;

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
            if (interaction == InteractionType.down)
            {
                if (onActionTrigger != null)
                {
                    onActionTrigger.Invoke();
                }
            }
        }
    }

    public void DoHoldAction()
    {
        if (canControl)
        {
            _doHoldAction = true;
            if (interaction == InteractionType.hold)
            {
                if (onActionTrigger != null)
                {
                    onActionTrigger.Invoke();
                }
            }
        }
    }

    public void DoUpAction()
    {
        if (canControl)
        {
            _doUpAction = true;
            if (interaction == InteractionType.up)
            {
                if (onActionTrigger != null)
                {
                    onActionTrigger.Invoke();
                }
            }
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
        _bufferCanControl = canControl;
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
        BufferTime();
    }

    protected bool GetButton()
    {
        if (_input == null || (TimeManager.GetIsPaused() && !useUnscaledTime))
        {
            return false;
        }
        if (TimeManager.GetJustUnpaused() && !useUnscaledTime)
        {
            bool r = false;
            if ((!_wasHolding && _input.buttonHold && (interaction == InteractionType.down || interaction == InteractionType.hold)))
            {
                r = true;
            }
            if ((_wasHolding && !_input.buttonHold && interaction == InteractionType.up))
            {
                r = true;
            }
            if (!canControl && r)
            {
                Buffer(r);
                return false;
            }
        }
        _wasHolding = _input.buttonHold;

        switch (interaction)
        {
            case InteractionType.down:
                return Buffer(_input.buttonDown);
            case InteractionType.hold:
                return Buffer(_input.buttonHold);
            case InteractionType.up:
                return Buffer(_input.buttonUp);
            default: 
                return false;
        }
    }

    private bool Buffer(bool value)
    {
        if (value && canControl)
        {
            return true;
        }
        else if (value && !canControl)
        {
            _currentBufferTime = bufferTime;
            return false;
        }
        else
        {
            return false;
        }
    }

    private void BufferTime()
    {
        if (_currentBufferTime > 0)
        {
            _currentBufferTime -= Time.deltaTime;
            if (_currentBufferTime <= 0)
            {
                _currentBufferTime = 0;
            }
        }
        if ((!_bufferCanControl && canControl) && _currentBufferTime > 0)
        {
            if (onActionTrigger != null)
            {
                onActionTrigger.Invoke();
            }
            switch (interaction)
            {
                case InteractionType.down:
                    _doDownAction = true;
                    break;
                case InteractionType.hold:
                    _doHoldAction = true;
                    break;
                case InteractionType.up:
                    _doUpAction = true;
                    break;
            }
            _currentBufferTime = 0;
        }
        _bufferCanControl = canControl;
    }

    public void CancelBuffer()
    {
        _currentBufferTime = 0;
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

    private void OnDisable()
    {
        _currentBufferTime = 0;
    }
}
