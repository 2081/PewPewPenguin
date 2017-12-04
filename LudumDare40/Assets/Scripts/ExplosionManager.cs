using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionManager : MonoBehaviour {

    private static ExplosionManager instance;


    public static void Explode(Vector3 position)
    {
        if (instance != null) instance.ExplodeInstance(position);
    }

    ParticleSystem ps;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
    void ExplodeInstance(Vector3 position)
    {
        transform.position = position;
        ps.Play();
    }

}
