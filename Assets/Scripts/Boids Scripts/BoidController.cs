using UnityEngine;
using System.Collections.Generic;

public class BoidController : MonoBehaviour {

    public GameObject boidPrefab;
    public int boidNumber;
    public float maxSpawnDistance;
    //public float boxBoundries;
    public Transform target;

    private List<BoidBehavior> boids;

    [Range(0.1f, 1.0f)]
    public float cohesion;

    [Range(0.1f, 1.0f)]
    public float dispersion;

    [Range(0.0f, 0.35f)]
    public float alignment;

    private void Awake()
    {
        boids = new List<BoidBehavior>();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < boidNumber; i++)
        {
            pos.x = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.y = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.z = Random.Range(-maxSpawnDistance, maxSpawnDistance);

            GameObject temp = Instantiate(boidPrefab, pos, new Quaternion()) as GameObject;

            BoidBehavior bb = temp.GetComponent<BoidBehavior>();
            bb.velocity = bb.transform.position.normalized;

            bb.transform.parent = transform;

            boids.Add(bb);
        }
    }

    private void FixedUpdate () {
	    foreach(BoidBehavior bb in boids)
        {
            Vector3 r1 = CenterOfMass(bb);
            Vector3 r2 = Dispersion(bb);
            Vector3 r3 = Alignment(bb);
            bb.velocity += r1 + r2 + r3;
        }
	}

    private Vector3 CenterOfMass(BoidBehavior b)
    {
        //Rule 1: center of mass
        Vector3 percCenter = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if(bj != b)
                percCenter += bj.transform.position;
        }

        percCenter = percCenter / (boids.Count - 1); //Divide the Precived Center by how many other boids there are.

        return (percCenter - b.transform.position).normalized * cohesion;   //Set rule one. MUST BE NORMALIZED
    }

    private Vector3 Dispersion(BoidBehavior b)
    {
        //Rule 2: distancing each other appart
        Vector3 avoid = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if ((bj.transform.position - b.transform.position).magnitude <= 100 * dispersion && bj != b)
            {
                avoid -= bj.transform.position - b.transform.position;
            }
        }

        return avoid.normalized;
    }

    private Vector3 Alignment(BoidBehavior b)
    {
        //Rule 3: Alignment
        Vector3 percVelocity = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if(bj != b)
                percVelocity += bj.velocity;
        }

        percVelocity = percVelocity / (boids.Count - 1);

        Vector3 rule3 = (percVelocity - b.velocity).normalized * alignment;

        ClampVector(rule3);

        return rule3;
    }

    public void ClampVector(Vector3 vec)
    {
        vec.x = Mathf.Clamp(vec.x, 0, 1);
        vec.y = Mathf.Clamp(vec.y, 0, 1);
        vec.z = Mathf.Clamp(vec.z, 0, 1);
    }
}