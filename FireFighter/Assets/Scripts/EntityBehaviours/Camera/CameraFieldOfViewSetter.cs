using System.Collections;
using UnityEngine;

public class CameraFieldOfViewSetter : MonoBehaviour
{
    [SerializeField] private Camera cameraToAdjust;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private float waitForSeconds;

    public void SetFieldOfView(float value)
    {
        StartCoroutine(Transition(value));
    }

    IEnumerator Transition(float value)
    {
        while (cameraToAdjust.fieldOfView != value) 
        {
            if (cameraToAdjust.fieldOfView > value)
            {
                cameraToAdjust.fieldOfView -= transitionSpeed;
                if (cameraToAdjust.fieldOfView <= value)
                {
                    cameraToAdjust.fieldOfView = value;
                }
            }
            else if (cameraToAdjust.fieldOfView < value)
            {
                cameraToAdjust.fieldOfView += transitionSpeed;
                if (cameraToAdjust.fieldOfView >= value)
                {
                    cameraToAdjust.fieldOfView = value;
                }
            }
            yield return new WaitForSeconds(waitForSeconds);
        }
    }
}
