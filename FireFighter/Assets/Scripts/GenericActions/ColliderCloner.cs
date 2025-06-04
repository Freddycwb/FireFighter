using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCloner : MonoBehaviour
{
    [SerializeField] private Collider reference;
    [SerializeField] private bool isTrigger = true;
    [SerializeField] private bool createColliderOnEnable = true;
    [SerializeField] private bool setTransformToReference = true;

    private void OnEnable()
    {
        if (!createColliderOnEnable)
        {
            return;
        }
        if (reference.GetType() == typeof(BoxCollider))
        {
            AddBoxCollider();
        }
        else if (reference.GetType() == typeof(CapsuleCollider))
        {
            AddCapsuleCollider();
        }
        if (setTransformToReference)
        {
            transform.position = reference.transform.position;
            transform.rotation = reference.transform.rotation;
            transform.localScale = reference.transform.localScale;
        }
    }

    private void AddBoxCollider()
    {
        BoxCollider referenceBox = (BoxCollider)reference;
        BoxCollider newBox = gameObject.AddComponent<BoxCollider>();
        newBox.isTrigger = isTrigger;
        newBox.material = referenceBox.material;
        newBox.center = referenceBox.center;
        newBox.size = referenceBox.size;
    }

    private void AddCapsuleCollider()
    {
        CapsuleCollider referenceCapsule = (CapsuleCollider)reference;
        CapsuleCollider newCapsule = gameObject.AddComponent<CapsuleCollider>();
        newCapsule.isTrigger = isTrigger;
        newCapsule.material = referenceCapsule.material;
        newCapsule.center = referenceCapsule.center;
        newCapsule.radius = referenceCapsule.radius;
        newCapsule.height = referenceCapsule.height;
        newCapsule.direction = referenceCapsule.direction;
    }
}
