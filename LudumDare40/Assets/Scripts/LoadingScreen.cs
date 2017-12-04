using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    private static LinkedList<LoadingScreen> instances = new LinkedList<LoadingScreen>();

    public static void Close()
    {
        foreach(var instance in instances)
        {
            instance.GetComponent<Animator>().SetTrigger("Close");
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }


    private void OnEnable()
    {
        instances.AddLast(this);
    }

    private void OnDisable()
    {
        instances.Remove(this);
    }
}
