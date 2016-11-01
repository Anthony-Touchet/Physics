using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoParticle : MonoBehaviour {

    public Particle particle;
    public bool anchor;
    public List<MonoParticle> neighbors;	

    void LateUpdate()
    {
        foreach (MonoParticle mp in neighbors)
        {
            Debug.DrawLine(transform.position, mp.transform.position, Color.red);
        }
    }
}
