using UnityEngine;
using System.Collections;

public class SeekingBehavior : MonoBehaviour {
    MonoBoid a;
    Vector3 desiredVelocity;
    public Transform target;
    Vector3 steering;
    public float steeringFactor;

    void Awake()
    {
        a = gameObject.GetComponent<MonoBoid>();
    }

    void FixedUpdate () {
        
        desiredVelocity = (target.position - transform.position).normalized;    //Displacement, normilized
        steering = (desiredVelocity - a.velocity).normalized * steeringFactor;  //Steering Velocity, get direction
        a.velocity += steering / a.mass;       //Nudge the direction
        if (a.velocity.magnitude > 5)         //Keep speed to avoid jittering
            a.velocity = a.velocity.normalized;
    }
}
