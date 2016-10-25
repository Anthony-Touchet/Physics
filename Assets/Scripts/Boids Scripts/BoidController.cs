using UnityEngine;
using System.Collections.Generic;

public class BoidController : MonoBehaviour {

    public GameObject boidPrefab;
    public int boidNumber;
    public float maxSpawnDistance;
    public float boxBoundries;
    public Transform target;

    public float minMass;
    public float maxMass;

    private List<BoidBehavior> boids;

    [Range(0.0f, 1.0f)]
    public float cohesion;

    [Range(0.0f, 1.0f)]
    public float dispersion;

    [Range(0.0f, 1.0f)]
    public float alignment;

    private void Awake()
    {
        boids = new List<BoidBehavior>();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < boidNumber; i++)    //Spawn and add to list
        {
            pos.x = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.y = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.z = Random.Range(-maxSpawnDistance, maxSpawnDistance);

            GameObject temp = Instantiate(boidPrefab, pos, new Quaternion()) as GameObject;

            BoidBehavior bb = temp.GetComponent<BoidBehavior>();
            bb.velocity = bb.transform.position.normalized;
            bb.mass = Random.Range(minMass, maxMass);

            bb.transform.parent = transform;

            boids.Add(bb);
        }

        foreach(BoidBehavior bb in FindObjectsOfType<BoidBehavior>())
        {
            if (boids.Contains(bb) == false)
                boids.Add(bb);
        }
    }

    private void FixedUpdate () {
	    foreach(BoidBehavior bb in boids)
        {
            Vector3 r1 = CenterOfMass(bb) * cohesion;
            Vector3 r2 = Dispersion(bb) * dispersion;
            Vector3 r3 = Alignment(bb) * alignment;
            Vector3 walls = WallBoundries(bb);
            bb.velocity += (r1 + r2 + r3 + walls) / bb.mass;
        }
	}

    private Vector3 CenterOfMass(BoidBehavior b)
    {
        //Rule 1: center of mass
        Vector3 percCenter = Vector3.zero;
        float totalMass = 0;
        foreach (BoidBehavior bj in boids)
        {
            if(bj != b)
            {
                percCenter += bj.transform.position * bj.mass;
                totalMass += bj.mass;
            }
                
        }

        percCenter = percCenter / totalMass /*/ (boids.Count - 1)*/; //Divide the Precived Center by how many other boids there are.

        return (percCenter - b.transform.position).normalized ;   //Set rule one. MUST BE NORMALIZED
    }

    private Vector3 Dispersion(BoidBehavior b)
    {
        //Rule 2: distancing each other appart
        Vector3 avoid = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if ((bj.transform.position - b.transform.position).magnitude <= 50  && bj != b)
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

        Vector3 rule3 = (percVelocity - b.velocity).normalized ;

        ClampVector(rule3);

        return rule3;
    }

    private Vector3 WallBoundries(BoidBehavior b)
    {
        Vector3 bounds = new Vector3();

        if (b.transform.position.x > boxBoundries)
            bounds += new Vector3(-5, 0, 0);
        else if (b.transform.position.x < -boxBoundries)
            bounds += new Vector3(5, 0, 0);

        if (b.transform.position.y > boxBoundries)
            bounds += new Vector3(0, -5, 0);
        else if (b.transform.position.y < -boxBoundries)
            bounds += new Vector3(0, 5, 0);

        if (b.transform.position.z > boxBoundries)
            bounds += new Vector3(0, 0, -5);
        else if (b.transform.position.z < -boxBoundries)
            bounds += new Vector3(0, 0, 5);

        return bounds;
    }

    public void ClampVector(Vector3 vec)
    {
        vec.x = Mathf.Clamp(vec.x, 0, 1);
        vec.y = Mathf.Clamp(vec.y, 0, 1);
        vec.z = Mathf.Clamp(vec.z, 0, 1);
    }
}