using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : NumericVariable<float>
{
    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
    }

    public override void Add(float amount)
    {
        Value += amount;
    }

    public override void Add(Variable<float> amount)
    {
        Value += amount.Value;
    }
}
