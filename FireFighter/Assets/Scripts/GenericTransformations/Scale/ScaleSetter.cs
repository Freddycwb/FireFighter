using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToScale;

    public void SetScale(float value)
    {
        objToScale.transform.localScale = Vector3.one * value;
    }

    public void SetXScale(float value)
    {
        objToScale.transform.localScale = new Vector3(value, objToScale.transform.localScale.y, objToScale.transform.localScale.z);
    }

    public void SetYScale(float value)
    {
        objToScale.transform.localScale = new Vector3(objToScale.transform.localScale.x, value, objToScale.transform.localScale.z);
    }

    public void SetZScale(float value)
    {
        objToScale.transform.localScale = new Vector3(objToScale.transform.localScale.x, objToScale.transform.localScale.y, value);
    }
}
