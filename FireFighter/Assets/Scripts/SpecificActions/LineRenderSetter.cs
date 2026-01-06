using UnityEngine;

public class LineRenderSetter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private int posIndex = 0;
    public void SetPoint(GameObject value)
    {
        if(lineRenderer.positionCount <= posIndex)
        {
            return;
        }
        lineRenderer.SetPosition(posIndex, value.transform.position);
        posIndex++;
    }
}
