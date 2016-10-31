using UnityEngine;
using System.Collections;

public class SeekandArrive : MonoBehaviour {
    MonoBoid mB;
    Vector3 desiredVelocity;
    public Transform target;
    Vector3 steering;
    public float steeringFactor;
    public float radius;

    void Start()
    {
        mB = gameObject.GetComponent<MonoBoid>();
    }

    void FixedUpdate()
    {
        float pushBackForceFactor = (target.position - transform.position).magnitude / radius;
        Vector3 pushBackForce = (target.position - transform.position).normalized * pushBackForceFactor;

        mB.agent.velocity = pushBackForce / mB.agent.mass;
        
        if (mB.agent.velocity.magnitude > 3)         //Keep speed to avoid jittering
            mB.agent.velocity = mB.agent.velocity.normalized;
    }
}
