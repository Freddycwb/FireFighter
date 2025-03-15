using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionType : MonoBehaviour
{
    [System.Flags]
    public enum Types
    {
        None = 0,
        body = 1,
        melee = 2,
        water = 4,
        meleeFire = 8,
        fire = 16,
        instantDeath = 32,
        puddle = 64,
    }
}
