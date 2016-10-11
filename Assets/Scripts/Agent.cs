using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    public int mass;
    public Vector3 force;

    // F = mass * velocity
    // velocity = F / mass

    Vector3 velocity;

    void FixedUpdate () {
        velocity = force / mass;
    }

    void LateUpdate()
    {
        transform.position += velocity * Time.deltaTime;    //Add velocity to position. Multiply by delta time to make it smooth.
    }
}
