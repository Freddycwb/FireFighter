using UnityEngine;

public class LineRenderSetter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private int posIndex = 0;
    public void SetPoint(GameObject value)
    {
        lineRenderer.SetPosition(posIndex, value.transform.position);
        posIndex++;
    }
}
