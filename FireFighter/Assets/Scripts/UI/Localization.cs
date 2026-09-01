using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Localization : MonoBehaviour
{
    private static Dictionary<string, Dictionary<string, string>> _textDics;
    private static Dictionary<string, Dictionary<string, FMOD.GUID>> _audioDics;
    public static string _currentLanguage = "_pt-br";

    private static Dictionary<string, Dictionary<string, string>> _symbolDics;
    public static string _currentInput = "_playstation";

    private static bool _hasLoadedText;
    private static bool _hasLoadedAudio;
    public static Action updateL;

    private static void LoadText()
    {
        _textDics = LoadFromAsset(Resources.Load<TextAsset>("Localization"));
        _symbolDics = LoadFromAsset(Resources.Load<TextAsset>("Symbols"));

        _hasLoadedText = true;
    }

    private static void LoadAudio()
    {
        if (!FMODUnity.RuntimeManager.HaveAllBanksLoaded) return;

        Dictionary<string, Dictionary<string, string>> audioPaths = LoadFromAsset(Resources.Load<TextAsset>("Audio"));
        _audioDics = audioPaths.ToDictionary(
            outer => outer.Key,
            outer => outer.Value.ToDictionary(
                inner => inner.Key,
                inner =>
                {
                    string eventPath = inner.Value;
                    FMOD.Studio.EventDescription eventDescription;
                    FMOD.GUID guid = new FMOD.GUID();
                    if (FMODUnity.RuntimeManager.StudioSystem.getEvent(eventPath, out eventDescription) == FMOD.RESULT.OK)
                    {
                        eventDescription.getID(out guid);
                    }
                    return guid;
                }
            )
        );

        _hasLoadedAudio = true;
    }

    private static Dictionary<string, Dictionary<string, string>> LoadFromAsset(TextAsset asset)
    {
        string text = asset.text;

        Dictionary<string, Dictionary<string, string>> _dics = new Dictionary<string, Dictionary<string, string>>();
        string[] lines = text.Split('\n');
        string[] header = lines[0].Split('\t');

        // 2 because line 1 is empty
        for (int i = 2; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split('\t');
            string key = cells[0].Trim();
            for (int j = 1; j < cells.Length; j++)
            {
                string category = header[j].Trim();
                if (!_dics.ContainsKey(category))
                {
                    _dics.Add(category, new Dictionary<string, string>());
                }
                string value = cells[j].Trim();
                _dics[category].Add(key, value);
            }
        }

        return _dics;
    }

    public void ChangeLanguage(string language)
    {
        _currentLanguage = language;
        UpdateLanguage();
    }

    public static string LocalizeText(string key)
    {
        if (!_hasLoadedText)
        {
            LoadText();
        }
        if (!_textDics[_currentLanguage].ContainsKey(key))
        {
            Debug.LogWarning(string.Format("Localization.Localize There's no value for key: {0} in textDics", key));
            return string.Format("*_{0}_*", key);
        }
        
        string text = _textDics[_currentLanguage][key];
        for (int escapeStart = text.IndexOf('['); escapeStart != -1; escapeStart = text.IndexOf('[')) {
            int escapeEnd = text.IndexOf(']');

            if (escapeEnd == -1) break;

            int symbolLen = escapeEnd - (escapeStart + 1);
            string symbol = "";

            if (symbolLen > 0)
            {
                string symbolKey = text.Substring(escapeStart + 1, symbolLen);

                if (!_symbolDics[_currentInput].ContainsKey(symbolKey))
                {
                    Debug.LogWarning(string.Format("Localization.Localize There's no value for key: {0} in symbolDics", symbolKey));
                    symbol = string.Format("*_{0}_*", symbolKey);
                }
                else
                {

                    symbol = _symbolDics[_currentInput][symbolKey];
                }
            }

            text = text.Replace(text.Substring(escapeStart, escapeEnd - escapeStart + 1), symbol); 
        }

        return text;
    }

    public static FMOD.GUID LocalizeAudio(string key)
    {
        if (!_hasLoadedAudio)
        {
            LoadAudio();
        }
        if (!_audioDics[_currentLanguage].ContainsKey(key))
        {
            return new FMOD.GUID();
        }

        return _audioDics[_currentLanguage][key];
    }

    public static void UpdateLanguage()
    {
        updateL?.Invoke();
    }
}
