using UnityEngine;
using System.Collections;

public class Wanderer : MonoBehaviour {

    public float boundries;
    public float maxSpeed;
    public float findingRange;
    public float mass;

    Vector3 desiredVelocity;
    Vector3 steering;
    bool seeking;
    Vector3 target;

    Vector3 velocity;
    
    [HideInInspector]public Vector3 position;
	// Use this for initialization
	void Awake () {
        position = transform.position;
        target = GetTarget();
        seeking = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (seeking == true)
        {
            desiredVelocity = (target - position).normalized;
            steering = (desiredVelocity - velocity).normalized;
            velocity += steering / mass;
            
            //Limit Velocity
            if (velocity.magnitude > maxSpeed)
                velocity = velocity.normalized * maxSpeed;

            seeking = IsClose();

            position += velocity;
            transform.position = position;
        }

        else
        {
            target = GetTarget();
            seeking = true;
        }
    }

    private Vector3 GetTarget()
    {
        if(Random.Range(0, 5) == 3)
        {
            return FindObjectOfType<BoidController>().transform.position;
        }

        Vector3 t;
        t.x = Random.Range(-boundries, boundries);
        t.y = Random.Range(-boundries, boundries);
        t.z = Random.Range(-boundries, boundries);
        return t;
    }

    private bool IsClose()
    {
        if ((target - position).magnitude <= findingRange)
            return false;

        else
            return true;
    }
}
