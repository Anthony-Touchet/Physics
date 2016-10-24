using UnityEngine;
using System.Collections;

public class SeekingBehavior : MonoBehaviour {
    MonoBoid mB;
    Vector3 desiredVelocity;
    public Transform target;
    Vector3 steering;
    public float steeringFactor;

    void Start()
    {
        mB = gameObject.GetComponent<MonoBoid>();
    }

    void FixedUpdate () {
        
        desiredVelocity = (target.position - transform.position).normalized;    //Displacement, normilized
        steering = (desiredVelocity - mB.agent.velocity).normalized * .1f * steeringFactor;  //Steering Velocity, get direction
        mB.agent.velocity += steering / mB.agent.mass;       //Nudge the direction
        if (mB.agent.velocity.magnitude > 3)         //Keep speed to avoid jittering
            mB.agent.velocity = mB.agent.velocity.normalized;
    }
}
