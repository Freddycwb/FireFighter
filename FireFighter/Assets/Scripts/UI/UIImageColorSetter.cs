using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIImageColorSetter : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetColor(string value)
    {
        var r = value.Substring(0, 2);
        var g = value.Substring(2, 2);
        var b = value.Substring(4, 2);
        string alpha;
        if (value.Length >= 8)
            alpha = value.Substring(6, 2);
        else
            alpha = "FF";

        image.color = new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                        (int.Parse(g, NumberStyles.HexNumber) / 255f),
                        (int.Parse(b, NumberStyles.HexNumber) / 255f),
                        (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
    }

    public void SetOpacityByCounterGameObject(GameObjectVariable value)
    {
        InvokeAfterCounter counter = value.Value.GetComponent<InvokeAfterCounter>();

        if (counter != null)
        {
            SetOpacity(counter);
        }
    }

    public void SetOpacityByCounterGameObject(GameObject value)
    {
        InvokeAfterCounter counter = value.GetComponent<InvokeAfterCounter>();

        if (counter != null) {
            SetOpacity(counter);
        }
    }

    public void SetOpacity(InvokeAfterCounter value)
    {
        float min = value.GetMinValue();
        float current = value.GetCurrentValue();
        float max = value.GetMaxValue();

        SetOpacity(current / (max - min));
    }

    public void SetOpacity(float value)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
    }
}
