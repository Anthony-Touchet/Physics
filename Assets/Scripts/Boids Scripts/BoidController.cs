using UnityEngine;
using System.Collections.Generic;

public class BoidController : MonoBehaviour {

    public GameObject boidPrefab;
    public int boidNumber;
    public float maxSpawnDistance;
    public float boxBoundries;
    public float maxSpeed;

    public Transform target;
    public float targetRange;
    public Transform center;

    public float minMass;
    public float maxMass;

    private List<BoidBehavior> boids;

    [Range(0.0f, 1.0f)]
    public float cohesion;

    [Range(0.0f, 1.0f)]
    public float dispersion;

    [Range(0.0f, 1.0f)]
    public float alignment;

    [Range(-1.0f, 1.0f)]
    public float tendency;

    //Deals with spawning and adding boids to a list.
    private void Awake()
    {
        maxSpeed = (maxSpeed <= 0) ? 1 : maxSpeed;
        targetRange = (targetRange <= 0) ? 20 : targetRange;

        Color col = new Color();
        col.r = Random.Range(0, 226);
        col.g = Random.Range(0, 226);
        col.b = Random.Range(0, 226);
        col.a = 255;

        boids = new List<BoidBehavior>();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < boidNumber; i++)    //Spawn and add to list
        {
            pos.x = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.y = Random.Range(-maxSpawnDistance, maxSpawnDistance);
            pos.z = Random.Range(-maxSpawnDistance, maxSpawnDistance);

            GameObject temp = Instantiate(boidPrefab, transform.position + pos, new Quaternion()) as GameObject;

            BoidBehavior bb = temp.GetComponent<BoidBehavior>();
            bb.velocity = bb.transform.position.normalized;
            bb.mass = Random.Range(minMass, maxMass);

            bb.transform.parent = transform;

            boids.Add(bb);
        }
    }
    
    //Where Math is applied to the boids
    private void FixedUpdate () {
	    foreach(BoidBehavior bb in boids)
        {
            Vector3 r1 = Cohesion(bb) * cohesion;
            Vector3 r2 = Dispersion(bb) * dispersion;
            Vector3 r3 = Alignment(bb) * alignment;
            Vector3 walls = WallBoundries(bb);
            Vector3 tendTowards = TendTowardsPlace(bb) * tendency;
            bb.velocity += ((r1 + r2 + r3 + walls + tendTowards) / bb.mass);
            LimitVelocity(bb);
        }

        center.position = MarkCenterOfMass();
	}

    //Calculates the cohesion vector for a boid
    private Vector3 Cohesion(BoidBehavior b)
    {
        //Rule 1: center of mass
        Vector3 percCenter = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if(bj != b)
            {
                percCenter += bj.transform.position;
            }
                
        }

        percCenter = percCenter / (boids.Count - 1); //Divide the Precived Center by how many other boids there are.

        return (percCenter - b.transform.position).normalized;   //Set rule one. MUST BE NORMALIZED
    }

    //Calculates the Dispersion vector of a boid
    private Vector3 Dispersion(BoidBehavior b)
    {
        //Rule 2: distancing each other appart
        Vector3 avoid = Vector3.zero;
        foreach (BoidBehavior bj in boids)
        {
            if ((bj.transform.position - b.transform.position).magnitude <= 20 && bj != b)
            {
                avoid -= bj.transform.position - b.transform.position;
            }
        }

        return avoid.normalized;
    }

    //Calculates the Alignment vector of a boid
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

        Vector3 rule3 = (percVelocity - b.velocity).normalized;

        //ClampVector(rule3);

        return rule3;
    }

    //Sets wall boundries for the boids
    private Vector3 WallBoundries(BoidBehavior b)
    {
        Vector3 bounds = new Vector3();

        if (b.transform.position.x > boxBoundries)
            bounds += new Vector3(-10, 0, 0);
        else if (b.transform.position.x < -boxBoundries)
            bounds += new Vector3(10, 0, 0);

        if (b.transform.position.y > boxBoundries)
            bounds += new Vector3(0, -10, 0);
        else if (b.transform.position.y < -boxBoundries)
            bounds += new Vector3(0, 10, 0);

        if (b.transform.position.z > boxBoundries)
            bounds += new Vector3(0, 0, -10);
        else if (b.transform.position.z < -boxBoundries)
            bounds += new Vector3(0, 0, 10);

        return bounds;
    }

    //Clamp the vector between 0 and 1
    public void ClampVector(Vector3 vec)
    {
        vec.x = Mathf.Clamp(vec.x, 0, 1);
        vec.y = Mathf.Clamp(vec.y, 0, 1);
        vec.z = Mathf.Clamp(vec.z, 0, 1);
    }

    //Setting an object to the center of mass for all the object
    private Vector3 MarkCenterOfMass()
    {
        Vector3 centerOfMass = Vector3.zero;
        Vector3 allPositions = Vector3.zero;
        foreach (BoidBehavior bb in boids)
        {
            allPositions += bb.transform.position;
        }

        centerOfMass = allPositions / boids.Count;
        return centerOfMass;
    }

    //Limiting Velocity
    private void LimitVelocity(BoidBehavior bb)
    {
        if(bb.velocity.magnitude > maxSpeed)
        {                   //Normalizing                       times speed you want
            bb.velocity = (bb.velocity / bb.velocity.magnitude) * maxSpeed;
        }
    } 

    //Function that caculates a tendecy for a boid to move in a certain direction
    private Vector3 TendTowardsPlace(BoidBehavior bb)
    {
        if (tendency > 0)
        {
            if(GetColor(bb) != Color.white)
                SetColor(Color.white, bb);
            return (target.position - bb.transform.position).normalized;
        }
            

        else if (tendency < 0 && (target.position - bb.transform.position).magnitude < targetRange)
        {
            if (GetColor(bb) != Color.yellow)
                SetColor(Color.yellow, bb);
            return (target.position - bb.transform.position).normalized;
        }

        else
        {
            if (GetColor(bb) != Color.black)
                SetColor(Color.black, bb);
            return Vector3.zero;
        }
            
    }

    private void SetColor(Color c, BoidBehavior bb)
    {
        bb.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = c;
    }

    private Color GetColor(BoidBehavior bb)
    {
        return bb.transform.GetChild(1).GetComponent<MeshRenderer>().material.color;
    }
}