using UnityEngine;
using System.Collections;

public class SpringDamper
{

    public float springConst,       //ks
                 dampingFactor,     //kd
                 restLength;        //l0

    public Particle P1, P2;
    //Hooke's Law F = -kX

    public Vector3 P1force;
    public Vector3 P2force;
    public SpringDamper()
    {

    }
    public SpringDamper(Particle one, Particle two, float sC, float dF, float rL)
    {
        if (one == null)
            Debug.Break();
        P1 = one;
        P2 = two;
        springConst = sC;
        dampingFactor = dF;
        restLength = rL;
    }

    public void ComputeForce()
    {
        Vector3 dist = (P2.position - P1.position);     //e*

        Vector3 e = dist / dist.magnitude;  //Distance Normalized, e

        float p11D = Vector3.Dot(e, P1.velocity);   //P1 1D velocity
        float p21D = Vector3.Dot(e, P2.velocity);   //P2 1D velocity

        float springDampingForce = -(springConst) * (restLength - dist.magnitude) - dampingFactor * (p11D - p21D);

        P1force = springDampingForce * e;
        P2force = -(P1force);

        P1.steering += P1force;
        P2.steering += P2force;
    }
}
