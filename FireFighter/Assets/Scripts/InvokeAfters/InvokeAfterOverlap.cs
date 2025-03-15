using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InvokeAfterOverlap : InvokeAfter
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private QueryTriggerInteraction triggerInteraction;
    [SerializeField] private List<string> tags = new List<string>();
    [SerializeField] private bool ignoreTags;

    public Action<GameObject> onContact;

    public void Check()
    {
        Collider[] collision = Physics.OverlapSphere(transform.position, radius, layer, triggerInteraction);

        if (collision.Length <= 0)
        {
            CallSubAction();
            return;
        }

        foreach (Collider collider in collision)
        {
            bool acceptTags = (tags.Contains(collider.tag) && !ignoreTags) || (!tags.Contains(collider.tag) && ignoreTags);
            if (tags.Count == 0 || acceptTags)
            {
                onContact?.Invoke(collider.gameObject);
                CallAction();
            }
        }
    }
}
