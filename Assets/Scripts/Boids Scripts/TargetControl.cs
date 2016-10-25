using UnityEngine;
using System.Collections;

public class TargetControl : MonoBehaviour {

    public float range;

	// Update is called once per frame
	void Update () {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && transform.position.z < range)
            movement += Vector3.forward;

        if (Input.GetKey(KeyCode.S) && transform.position.z > -range)
            movement += Vector3.back;

        if (Input.GetKey(KeyCode.A) && transform.position.x > -range)
            movement += Vector3.left;

        if (Input.GetKey(KeyCode.D) && transform.position.x < range)
            movement += Vector3.right;

        if (Input.GetKey(KeyCode.PageUp) && transform.position.y < range)
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.PageDown) && transform.position.y > -range)
            movement += Vector3.down;


        transform.position += movement;
    }
}
