using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance { private set; get; }

    private bool _muteSounds = false;
    public bool muteSounds {
        get
        {
            if (!Preferences.loaded) Preferences.Load();
            return Preferences.muteSounds;
        }
        set
        {
            Preferences.muteSounds = _muteSounds = value;
            Preferences.Save();
            foreach(var apc in dictionary)
            {
                if( apc.Value.category == AudioPlaybackConfig.SoundCategory.Sound)
                    apc.Value.Mute(value);
            }
        }
    }
    private bool _muteMusic = false;
    public bool muteMusic
    {
        get
        {
            if (!Preferences.loaded) Preferences.Load();
            return Preferences.muteMusic;
        }
        set
        {
            Preferences.muteMusic = _muteMusic = value;
            Preferences.Save();
            foreach (var apc in dictionary)
            {
                if (apc.Value.category == AudioPlaybackConfig.SoundCategory.Music)
                    apc.Value.Mute(value);
            }
        }
    }

    private Dictionary<string, AudioPlaybackConfig> dictionary;

	// Use this for initialization
	void Start () {
        if( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            dictionary = new Dictionary<string, AudioPlaybackConfig>();
            foreach (var apc in GetComponentsInChildren<AudioPlaybackConfig>())
            {
                dictionary.Add(apc.name.ToLower(), apc);
            }
            
            muteMusic = muteMusic;
            muteSounds = muteSounds;
        } else
        {
            Destroy(gameObject);
        }
	}
	
    public void Play(string key)
    {
        AudioPlaybackConfig apc;
        if(dictionary.TryGetValue(key.ToLower(), out apc))
        {
            apc.Play();
        } else
        {
            Debug.LogError("Sound not found: " + key);
        }
    }

    public void Mute(string key, bool mute = true)
    {
        AudioPlaybackConfig apc;
        if (dictionary.TryGetValue(key.ToLower(), out apc))
        {
            apc.Mute(mute);
        }
    }



    public void WaitAndPlaySound(string sound, float seconds)
    {
        StartCoroutine(WaitAndPlaySoundRoutine(sound, seconds));
    }
    private IEnumerator WaitAndPlaySoundRoutine(string sound, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Play(sound);
    }
}
