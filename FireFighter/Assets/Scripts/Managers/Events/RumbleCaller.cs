using UnityEngine;

public class RumbleCaller : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float lowFrequency = 0.25f;
    [SerializeField][Range(0, 1)] private float highFrequency = 1f;
    [SerializeField] private float duration = 0.25f;

    [SerializeField] private GameEventVector3 RumbleRequest;

    public void CallRumble()
    {
        RumbleRequest.value = new Vector3 (lowFrequency, highFrequency, duration);
        RumbleRequest.Raise();
    }

    public void ZeroRumble()
    {
        RumbleRequest.value = new Vector3(0, 0, 0);
        RumbleRequest.Raise();
    }
}
