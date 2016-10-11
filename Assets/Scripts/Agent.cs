using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    public int mass;
    public Vector3 force;
    Vector3 velocity;
    // Applying a Force
    // F = mass * velocity
    // velocity = F / mass

    Vector3 desiredVelocity;
    public Transform target;
    Vector3 steering;

    void FixedUpdate () {
        
        desiredVelocity = (target.position - transform.position).normalized;    //Displacement, normilized
        steering = (desiredVelocity - velocity).normalized / mass;  //Steering Velocity, get direction, bassed on mass.
        velocity += steering;       //Nudge the direction
        if (velocity.magnitude > 5)         //Keep speed to avoid jittering
            velocity = velocity.normalized;
    }

    void LateUpdate()
    {
        transform.position += velocity;    //Add velocity to position. Multiply by delta time to make it smooth.
    }
}
