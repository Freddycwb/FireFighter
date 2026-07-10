using System.Collections;
using TMPro;
using UnityEngine;

public class FPSShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        StartCoroutine("SetFPS");
    }

    private IEnumerator SetFPS()
    {
        while (true)
        {
            text.text = "FPS: " + ((int)(1 / Time.unscaledDeltaTime)).ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
