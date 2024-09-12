using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageType : MonoBehaviour
{
    [System.Flags]
    public enum Types
    {
        None = 0,
        melee = 1,
        water = 2,
        fire = 4,
    }
}
