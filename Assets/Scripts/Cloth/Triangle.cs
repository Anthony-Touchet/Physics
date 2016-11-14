using UnityEngine;
using System.Collections.Generic;

public class Triangle{

    //formula: (1/2) p |v|^2 Cd a n
    //p and Cd are 1

    Vector3 n;      //Surface Normal
    Vector3 v;      //Average Velocity
    float a;        //Area of triangle

    public float windCoeficent = 1f;

    public Particle p1, p2, p3;     //All the particles.
    public SpringDamper p1P2, p2P3, p3P1;

    public Triangle()
    {

    }

    public Triangle(MonoParticle mp1, MonoParticle mp2, MonoParticle mp3)
    {
        p1 = mp1.particle;
        p2 = mp2.particle;
        p3 = mp3.particle;
    }

    // Update is called once per frame
    public bool CalculateAeroForce (Vector3 vAir) {
        Vector3 vSurface = ((p1.velocity + p2.velocity + p3.velocity) / 3);
        v = vSurface - vAir;
        n = Vector3.Cross((p2.position - p1.position), (p3.position - p1.position)) / 
            Vector3.Cross((p2.position - p1.position), (p3.position - p1.position)).magnitude;
        float ao = (1f / 2f) * Vector3.Cross((p2.position - p1.position), 
            (p3.position - p1.position)).magnitude;
        a = ao * (Vector3.Dot(v, n) / v.magnitude);

        Vector3 Faero = -(1f / 2f) * 1f * Mathf.Pow(v.magnitude, 2) * 1f * a * n;

        p1.AddForce((Faero / 3));
        p2.AddForce((Faero / 3));
        p3.AddForce((Faero / 3));
        return true;
    }
}
