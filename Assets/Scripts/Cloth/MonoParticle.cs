using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoParticle : MonoBehaviour {

    public Particle particle;
    public bool anchor;

    void LateUpdate()
    {
        foreach (Particle mp in particle.neighbors)
        {
            Debug.DrawLine(transform.position, mp.position, Color.red);
        }
    }
}
