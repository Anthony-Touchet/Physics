using UnityEngine;
using System.Collections;
using Inferances;
using System;

public class Agent : IBoid{
    private float m_mass;
    private Vector3 m_velocity;
    private Vector3 m_position;

    public Agent(float m)
    {
        velocity = Vector3.zero;
        position = Vector3.zero;
        mass = (mass == 0) ? 1 : mass;
    }

    public float mass
    {
        get
        {
            return m_mass;
        }

        set
        {
            m_mass = value;
        }
    }

    public Vector3 position
    {
        get
        {
            return m_position;
        }

        set
        {
            m_position = value;
        }
    }

    public Vector3 velocity
    {
        get
        {
            return m_velocity;
        }

        set
        {
            m_velocity = value;
        }
    }

    public void UpdateVelocity()
    {
        position += velocity;    //Add Velocity to Position. Multiply by delta time to make it smooth.
    }
}
