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
}
