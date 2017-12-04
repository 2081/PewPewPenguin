using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        if (PlayerController.instance != null)
        {
            Vector3 playerPos = PlayerController.instance.transform.position;
            playerPos.z = transform.position.z;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, playerPos - transform.position)));
        }
    }
}
