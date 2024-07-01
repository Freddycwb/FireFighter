using UnityEngine;

public interface IInputAction
{
    bool buttonDown { get; }
    bool buttonHold { get; }
    bool buttonUp { get; }
}
