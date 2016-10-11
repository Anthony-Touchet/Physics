using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    public int mass;
    public Vector3 force;
    Vector3 velocity;

    // F = mass * velocity
    // velocity = F / mass

    void FixedUpdate () {
        velocity = force / mass;
    }

    void LateUpdate()
    {
        transform.position += velocity * Time.deltaTime;    //Add velocity to position. Multiply by delta time to make it smooth.
    }
}
