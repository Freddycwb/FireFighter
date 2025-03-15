using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleEffect : MonoBehaviour
{
    private int index;

    public void InitPuddle()
    {
        index = PuddleManager.instance.AddPuddle(transform.position, 0.5f);
    }

    public void DeletePuddle()
    {
        PuddleManager.instance.RemovePuddle(index);
    }
}
