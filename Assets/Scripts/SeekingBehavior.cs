using UnityEngine;
using System.Collections;

public class SeekingBehavior : MonoBehaviour {
    Agent a;
    Vector3 desiredVelocity;
    public Transform target;
    Vector3 steering;
    public float steeringFactor;

    void Awake()
    {
        a = gameObject.GetComponent<Agent>();
    }

    void FixedUpdate () {
        
        desiredVelocity = (target.position - transform.position).normalized;    //Displacement, normilized
        steering = (desiredVelocity - a.velocity).normalized * steeringFactor;  //Steering Velocity, get direction
        a.velocity += steering;       //Nudge the direction
        if (a.velocity.magnitude > 5)         //Keep speed to avoid jittering
            a.velocity = a.velocity.normalized;
    }
}
