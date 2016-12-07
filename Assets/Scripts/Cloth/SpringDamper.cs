using UnityEngine;
using System.Collections;

public class SpringDamper
{

    public float SpringConst,       //ks
                 DampingFactor,     //kd
                 RestLength;        //l0

    public Particle P1, P2;
    //Hooke's Law F = -kX

    public SpringDamper()
    {

    }

    public SpringDamper(Particle one, Particle two, float sC, float dF, float rL)
    {
        P1 = one;
        P2 = two;
        SpringConst = sC;
        DampingFactor = dF;
        RestLength = rL;
    }

    public void ComputeForce()
    {
        Vector3 dist = (P2.Position - P1.Position);     //e*

        Vector3 e = dist / dist.magnitude;  //Distance Normalized, e

        float p11D = Vector3.Dot(e, P1.Velocity);   //P1 1D velocity
        float p21D = Vector3.Dot(e, P2.Velocity);   //P2 1D velocity

        float Fs = -SpringConst * (RestLength - dist.magnitude);
        float fd = -DampingFactor * (p11D - p21D);

        Vector3 springDampingForce =  (Fs + fd) * e;

        P1.AddForce(springDampingForce);
        P2.AddForce(-springDampingForce);
    }

    public bool BreakHappens(float breakMultiplyer)
    {
        if (!((P2.Position - P1.Position).magnitude > RestLength*breakMultiplyer)) return false;
        if (P2.Neighbors.Contains(P1))
            P2.Neighbors.Remove(P1);
        if (P1.Neighbors.Contains(P2))
            P1.Neighbors.Remove(P2);
        return true;
    }
}
