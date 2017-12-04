using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour {

    [HideInInspector]
    public float initialAngle;

    public float speed = 1f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * new Vector3(Mathf.Cos(initialAngle * Mathf.Deg2Rad), Mathf.Sin(initialAngle * Mathf.Deg2Rad), 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExplosionManager.Explode(transform.position);
        Destroy(gameObject);
    }
}
