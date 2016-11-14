using UnityEngine;
using System.Collections;

public class SpringDamper
{

    public float springConst,       //ks
                 dampingFactor,     //kd
                 restLength;        //l0

    public Particle P1, P2;
    //Hooke's Law F = -kX

    public SpringDamper()
    {

    }

    public SpringDamper(Particle one, Particle two, float sC, float dF, float rL)
    {
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

        float Fs = -springConst * (restLength - dist.magnitude);
        float fd = -dampingFactor * (p11D - p21D);

        Vector3 springDampingForce =  (Fs + fd) * e;

        P1.AddForce(springDampingForce);
        P2.AddForce(-springDampingForce);
    }

    public bool BreakHappens(float breakMultiplyer)
    {
        if ((P2.position - P1.position).magnitude > restLength * breakMultiplyer)
        {
            if (P2.neighbors.Contains(P1))
                P2.neighbors.Remove(P1);
            if (P1.neighbors.Contains(P2))
                P1.neighbors.Remove(P2);
            return true;
        }
            
        return false;
    }
}
