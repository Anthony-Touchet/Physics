using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle{

    public Vector3 velocity;
    public Vector3 steering;
    public float mass;
    public Vector3 position;

    public Particle()
    {

    }

    public Particle(Vector3 pos, Vector3 velo, float m)
    {
        position = Vector3.zero;
        steering = Vector3.zero;
        velocity = Vector3.zero;
        position = pos;
        velocity = velo;
        mass = (m == 0) ? 1 : m;
    }

    public Vector3 UpdateParticle(float grav)
    {
        if (velocity.magnitude > 5)
            velocity = velocity.normalized * 5;
        steering += new Vector3(0, -1 * grav, 0); //applying gravity
        velocity += steering / mass;
        position += velocity;
        return position;
    }
}
