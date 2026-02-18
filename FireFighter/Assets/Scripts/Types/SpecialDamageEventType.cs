using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialDamageEventType : MonoBehaviour
{
    [System.Flags]
    public enum Types
    {
        None = 0,
        noKnockback = 1,
        gainHealStock = 2,
        restoreWater = 4
    }
}
