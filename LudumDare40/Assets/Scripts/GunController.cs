using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public delegate void OnGunShot(float angle, float recoil);


    public float firerate = 2f;
    public float recoil = 1f;

    public Transform bulletPrefab;

    public OnGunShot onGunShot;

	// Use this for initialization
	void Start () {
        StartCoroutine(ShootRoutine());
	}

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if(Input.GetAxis("Fire1") > 0 && PlayerController.instance != null && !PlayerController.instance.dead )
            {
                Shoot();
                yield return new WaitForSeconds(1 / firerate);
            } else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void Shoot()
    {
        AudioManager.instance.Play("Pew");

        Transform tr = Instantiate(bulletPrefab);
        tr.position = transform.position;

        float angle = transform.rotation.eulerAngles.z;

        tr.GetComponent<BulletController>().initialAngle = angle;

        if( onGunShot != null )
            onGunShot.Invoke(angle * Mathf.Deg2Rad, recoil);
    }
}
