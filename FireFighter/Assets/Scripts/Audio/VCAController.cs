using FMOD.Studio;
using System.Collections;
using UnityEngine;

public class VCAController : MonoBehaviour
{
    private VCA _vca;
    private bool _assignedVCA = false;
    [SerializeField] private string vcaName;

    void Start()
    {
        StartCoroutine(AssignVCA());
    }

    private IEnumerator AssignVCA()
    {
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }
        _vca = FMODUnity.RuntimeManager.GetVCA("vca:/" + vcaName);
        _assignedVCA = true;
    }

    public void SetVolume(InvokeAfterCounter value)
    {
        if (!_assignedVCA) return;
        SetVolume(value.GetCurrentValue());
    }

    public void SetVolume(float value)
    {
        if (!_assignedVCA) return;
        _vca.setVolume(value);
    }
}
