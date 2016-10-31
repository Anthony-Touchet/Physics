using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoParticle : MonoBehaviour {

    public Particle particle;
    public bool anchor;
    public List<MonoParticle> neighbors;	

	// Update is called once per frame

    void LateUpdate()
    {
        if (anchor == false)
            transform.position = particle.position;

        foreach (MonoParticle mp in neighbors)
        {
            Debug.DrawLine(transform.position, mp.transform.position, Color.red);
        }
    }
}
