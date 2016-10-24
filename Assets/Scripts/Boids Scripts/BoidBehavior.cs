using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidBehavior : MonoBehaviour {

    [HideInInspector]public Vector3 velocity;

    void LateUpdate() {                    //Were we will sum up all of the vectors.

        transform.position += velocity.normalized;
        transform.forward = velocity.normalized;
    }

    //void CenterOfMass()
    //{
    //    //Rule 1: center of mass
    //    Vector3 percCenter = Vector3.zero;
    //    foreach (BoidBehavior bb in otherBoids)
    //    {
    //        percCenter += bb.transform.position;
    //    }

    //    percCenter = percCenter / otherBoids.Count; //Divide the Precived Center by how many other boids there are.

    //    rule1 = (percCenter - transform.position).normalized * cohesionExponet;   //Set rule one. MUST BE NORMALIZED
    //}

    //void Dispersion()
    //{
    //    //Rule 2: distancing each other appart
    //    Vector3 avoid = Vector3.zero;
    //    foreach (BoidBehavior bb in otherBoids)
    //    {
    //        if ((bb.transform.position - transform.position).magnitude <= 20 * dispersionExponet)
    //        {
    //            avoid -= bb.transform.position - transform.position;
    //        }
    //    }

    //    rule2 = avoid.normalized;
    //}

    //void Alignment()
    //{
    //    //Rule 3: Alignment
    //    Vector3 percVelocity = Vector3.zero;
    //    foreach (BoidBehavior bb in otherBoids)
    //    {
    //        percVelocity += bb.velocity;
    //    }

    //    percVelocity = percVelocity / otherBoids.Count;

    //    rule3 = (percVelocity - velocity).normalized * alignmentExponet;

    //    ClampVector(rule3);
    //}
}
