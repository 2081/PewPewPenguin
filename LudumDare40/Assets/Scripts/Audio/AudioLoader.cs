using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioLoader : MonoBehaviour {
    
	void Start () {
        if (AudioManager.instance == null )
        {
            SceneManager.LoadSceneAsync("Audio", LoadSceneMode.Additive);
        }
	}
}
