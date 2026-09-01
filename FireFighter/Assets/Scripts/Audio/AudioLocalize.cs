using System;
using UnityEngine;

public class AudioLocalize : MonoBehaviour
{
    private FMOD.Studio.EventInstance eventInstance;
    private FMOD.Studio.EVENT_CALLBACK eventStoppedCallback;

    public Action onAudioStart;
    public Action onAudioEnd;

    private bool lastValid = false;

    private void Awake()
    {
        eventStoppedCallback = new FMOD.Studio.EVENT_CALLBACK(OnEventStopped);
    }

    private void Update()
    {
        bool valid = eventInstance.isValid();
        if (!valid && lastValid) onAudioEnd?.Invoke();
        lastValid = valid;
    }

    public void LocalizeAudio(string key)
    {
        StopAudio();

        FMOD.GUID guid = Localization.LocalizeAudio(key);
        if (guid == new FMOD.GUID()) return;

        eventInstance = FMODUnity.RuntimeManager.CreateInstance(guid);
        eventInstance.start();
        eventInstance.release();

        onAudioStart?.Invoke();
    }


    public void LocalizeAudio(TextStruct textStruct)
    {
        LocalizeAudio(textStruct.GetDisplayText());
    }

    public void StopAudio()
    {
        if (!eventInstance.isValid()) return;
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    private FMOD.RESULT OnEventStopped(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        //onAudioEnd?.Invoke();
        return FMOD.RESULT.OK;
    }
}
