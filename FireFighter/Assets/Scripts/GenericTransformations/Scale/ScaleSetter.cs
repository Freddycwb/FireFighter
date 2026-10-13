using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    [SerializeField] private GameObject objToScale;

    public void SetObjToScale(GameObject value)
    {
        objToScale = value;
    }

    public void SetScale(float value)
    {
        objToScale.transform.localScale = Vector3.one * value;
    }

    public void SetScale(GameObject value)
    {
        SetScale((value.transform.localScale.x + value.transform.localScale.y + value.transform.localScale.z) / 3);
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
