using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidBehavior : MonoBehaviour {

    [HideInInspector]public Vector3 velocity;
    
    public float mass;

    void LateUpdate() {                    //Were we will sum up all of the vectors.

        transform.position += velocity;
        transform.forward = velocity.normalized;
    }
}
