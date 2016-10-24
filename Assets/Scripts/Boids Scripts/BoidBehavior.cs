using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidBehavior : MonoBehaviour {

    Vector3 velocity;
    public float mass;

    Vector3 rule1;
    Vector3 rule2;
    Vector3 rule3;

    [Range(0.0f, 1.0f)]
    public float cohesionExponet;

    [Range(0.0f, 1.0f)]
    public float dispersionExponet;

    [Range(0.0f, 1.0f)]
    public float alignmentExponet;

    List<BoidBehavior> otherBoids;      //Will populate all other boids Besides this one.

    void Start () {
        otherBoids = new List<BoidBehavior>();
	    foreach(BoidBehavior bb in FindObjectsOfType<BoidBehavior>())   //Populating boids
        {
            if(this != bb)  //Add all but the current boid
            {
                otherBoids.Add(bb);
            }
        }

        rule1 = Vector3.zero;
        rule2 = Vector3.zero;
        rule3 = Vector3.zero;
    }
	
    void FixedUpdate()
    {
        //Rule 1: center of mass
        Vector3 percCenter = Vector3.zero;
        foreach(BoidBehavior bb in otherBoids)
        {
            percCenter += bb.transform.position;
        }

        percCenter = percCenter / otherBoids.Count; //Divide the Precived Center by how many other boids there are.

        rule1 = ((percCenter - transform.position) * cohesionExponet).normalized;   //Set rule one. MUST BE NORMALIZED

        //Rule 2: distancing each other appart
        Vector3 avoid = Vector3.zero;
        foreach(BoidBehavior bb in otherBoids)
        {
            if((bb.transform.position - transform.position).magnitude <= 2 * dispersionExponet)
            {
                avoid -= bb.transform.position - transform.position;
            }
        }

        rule2 = avoid.normalized;

        //Rule 3: Alignment

    }

	void LateUpdate () {                    //Were we will sum up all of the vectors.
        velocity += rule1 + rule2 + rule3;

        transform.position += velocity;
        transform.forward = velocity;
	}
}
