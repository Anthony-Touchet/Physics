using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle{

    public Vector3 Velocity;
    public Vector3 Force;
    public float Mass;
    public Vector3 Position;
    public Vector3 Acceleration;

    public List<Particle> Neighbors;

    public Particle()
    {

    }

    public Particle(Vector3 pos, Vector3 velo, float m)
    {
        Position = Vector3.zero;
        Force = Vector3.zero;
        Velocity = Vector3.zero;
        Position = pos;
        Velocity = velo;
        Mass = (m <= 0) ? 1 : m;
    }

    public Vector3 UpdateParticle()
    {
        Acceleration = (1f / Mass) * Force;
        Velocity += Acceleration * Time.deltaTime;
        Position += Velocity * Time.deltaTime;
        return Position;
    }

    public void AddForce(Vector3 f)
    {
        Force += f;
    }
}
