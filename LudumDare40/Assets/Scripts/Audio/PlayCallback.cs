using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCallback : MonoBehaviour {

	public void Play(string key)
    {
        AudioManager.instance.Play(key);
    }
}
