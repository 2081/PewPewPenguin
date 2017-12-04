using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlaybackConfig : MonoBehaviour {

    public enum Order
    {
        Randomized,
        Sequencial,
        Fixed,
        Simultaneous
    }

    public enum SoundCategory
    {
        Sound,
        Music
    }

    public SoundCategory category;
    [Range(0f, 2f)]
    public float globalPitchFactor = 1f;
    [Range(0f, 2f)]
    public float globalVolumeFactor = 1f;

    public int loopLength = 1;
    public Order order;
    public float sequenceResetDelay = -1f;
    public float playAgainDelay = 0.1f;
    public bool playOnAwake = false;

    private AudioSource[] sources;
    private int currentIndex = -1;
    private float lastPlayed = 0f;
    private int seqTimesPlayer = 0;
    //private float[] originalPitch;

    public void Awake()
    {
        lastPlayed = -playAgainDelay * 2;
        sources = GetComponents<AudioSource>();
        // originalPitch = new float[sources.Length];
        for (var i = 0; i < sources.Length; ++i)
        {
            //originalPitch[i] = sources[i].pitch;
            sources[i].pitch = sources[i].pitch * globalPitchFactor;
            sources[i].volume = sources[i].volume * globalVolumeFactor;
        }

        if( playOnAwake)
        {
            Play();
        }
    }

    public AudioSource GetAudioSource()
    {
        if( sources.Length > 0)
        {
            switch (order)
            {
                case Order.Randomized:
                    currentIndex = Random.Range(0, sources.Length);
                    break;
                case Order.Sequencial:
                    currentIndex = (currentIndex + 1);
                    // Debug.Log(currentIndex + " " + Time.time +" " + lastPlayed + " " + sequenceResetDelay);
                    currentIndex = sequenceResetDelay <= 0 || (Time.time - lastPlayed >= sequenceResetDelay) ? 0 : Mathf.Min(currentIndex, sources.Length -1);
                    break;
                case Order.Fixed:
                    currentIndex = 0;
                    break;
            }
            return sources[currentIndex];
        }
        return null;
    }

    public void Play()
    {
        if( Time.time - lastPlayed >= playAgainDelay)
        {
            if ( loopLength > 0)
            {
                if( order == Order.Simultaneous)
                {
                    foreach(var source in sources)
                    {
                        source.PlayOneShot(source.clip);
                    }
                } else
                {
                    AudioSource source = GetAudioSource();
                    source.PlayOneShot(source.clip);
                }

            } else if( loopLength < 0)
            {
                GetAudioSource().Play();
            }
            lastPlayed = Time.time;

        }
    }

    public void Mute(bool mute = true)
    {
        foreach (var s in sources) s.mute = mute;
    }
}
