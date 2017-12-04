using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    public string scene = "";

    public void Unload()
    {
        SceneManager.UnloadSceneAsync(scene);
    }
    public void Load(LoadSceneMode mode)
    {
        SceneManager.LoadScene(scene, mode);
    }
    public void LoadingScreen()
    {
        SceneManager.LoadScene("load", LoadSceneMode.Additive);
    }
    
}
