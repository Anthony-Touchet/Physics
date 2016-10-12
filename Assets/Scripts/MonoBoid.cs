using UnityEngine;
using System.Collections;
using Inferances;

public class MonoBoid : MonoBehaviour, IBoid{
    private Agent a;
    public float mass;

    float IBoid.mass
    {
        get
        {
            return a.mass;
        }

        set
        {
            a.mass = value;
        }
    }

    public Vector3 velocity
    {
        get
        {
            return a.velocity;
        }

        set
        {
            a.velocity = value;
        }
    }

    public Vector3 position
    {
        get
        {
            return a.position;
        }

        set
        {
            a.position = value;
        }
    }

    // Use this for initialization
    void Start () {
        a = new Agent(mass);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        a.UpdateVelocity();
    }  
}
