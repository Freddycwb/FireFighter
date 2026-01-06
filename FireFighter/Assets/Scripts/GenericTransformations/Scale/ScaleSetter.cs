using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToScale;

    public void SetScale(float value)
    {
        objToScale.transform.localScale = Vector3.one * value;
    }
}
