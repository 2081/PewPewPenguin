using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour {

	void Update () {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = transform.position.z;
        
        transform.rotation = Quaternion.Euler(new Vector3( 0, 0, Vector2.SignedAngle(Vector2.right, cursorPos - transform.position)));
	}
}
