using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle{

    public Vector3 velocity;
    public Vector3 force;
    public float mass;
    public Vector3 position;
    public Vector3 acceleration;

    public List<Particle> neighbors;

    public Particle()
    {

    }

    public Particle(Vector3 pos, Vector3 velo, float m)
    {
        position = Vector3.zero;
        force = Vector3.zero;
        velocity = Vector3.zero;
        position = pos;
        velocity = velo;
        mass = (m <= 0) ? 1 : m;
    }

    public Vector3 UpdateParticle()
    {
        acceleration = (1f / mass) * force;
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        return position;
    }

    public void AddForce(Vector3 f)
    {
        force += f;
    }
}
