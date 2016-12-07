using UnityEngine;
using System.Collections.Generic;

public class Triangle{

    //formula: (1/2) p |v|^2 Cd a n
    //p and Cd are 1

    private Vector3 _n;      //Surface Normal
    private Vector3 _v;      //Average Velocity
    private float _a;        //Area of triangle

    public float WindCoeficent = 1f;

    public Particle P1, P2, P3;     //All the particles.
    public SpringDamper P1P2, P2P3, P3P1;

    public Triangle()
    {

    }

    public Triangle(MonoParticle mp1, MonoParticle mp2, MonoParticle mp3)
    {
        P1 = mp1.Particle;
        P2 = mp2.Particle;
        P3 = mp3.Particle;
    }

    // Update is called once per frame
    public bool CalculateAeroForce (Vector3 vAir) {
        var vSurface = ((P1.Velocity + P2.Velocity + P3.Velocity) / 3);
        _v = vSurface - vAir;
        _n = Vector3.Cross((P2.Position - P1.Position), (P3.Position - P1.Position)) / 
            Vector3.Cross((P2.Position - P1.Position), (P3.Position - P1.Position)).magnitude;
        float ao = (1f / 2f) * Vector3.Cross((P2.Position - P1.Position), 
            (P3.Position - P1.Position)).magnitude;
        _a = ao * (Vector3.Dot(_v, _n) / _v.magnitude);

        var faero = -(1f / 2f) * 1f * Mathf.Pow(_v.magnitude, 2) * 1f * _a * _n;

        P1.AddForce((faero / 3));
        P2.AddForce((faero / 3));
        P3.AddForce((faero / 3));
        return true;
    }
}
