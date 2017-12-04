using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyDeathParticleManager : MonoBehaviour {

    private static EnemyDeathParticleManager instance;


    public static void Explode(Vector3 position)
    {
        if (instance != null) instance.ExplodeInstance(position);
    }

    ParticleSystem[] pss;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        pss = GetComponentsInChildren<ParticleSystem>();
	}
	
    void ExplodeInstance(Vector3 position)
    {
        transform.position = position;
        foreach( var ps in pss ) ps.Play();
    }

}
