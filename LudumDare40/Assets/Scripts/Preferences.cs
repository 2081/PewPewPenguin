using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour {

    
    private static string filename = "CFactory?tag=pref";

    public static bool muteSounds = false;
    private static string muteSoundsKey = "muteSounds";

    public static bool muteMusic = false;
    private static string muteMusicKey = "muteAudio";

    public static bool loaded = false;

    public static void Load()
    {
        /*string tmp;
        
        tmp = filename + "_" + muteSoundsKey;
        if (ES2.Exists(tmp))
        {
            muteSounds = ES2.Load<bool>(tmp);
        }

        tmp = filename + "_" + muteMusicKey;
        if (ES2.Exists(tmp))
        {
            muteMusic = ES2.Load<bool>(tmp);
        }

        loaded = true;*/
    }

    public static void Save()
    {
        /*ES2.Save<bool>(muteSounds, filename + "_" + muteSoundsKey);
        ES2.Save<bool>(muteMusic, filename + "_" + muteMusicKey);*/
    }
}
