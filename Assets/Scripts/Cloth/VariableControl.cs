using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VariableControl : MonoBehaviour {

    List<MonoParticle> particleList;
    List<SpringDamper> springDampenerList;
    List<Triangle> triangleList;
    public GameObject particlePrefab;

    public int width;
    public int height;
    public float padding;

    public float gravity;
    [Range(0f, 100f)]
    public float springConst;
    [Range(0f, 10f)]
    public float dampingFactor;
    [Range(0f, 25f)]public float restLength;

    [Range(0.01f, 3f)]
    public float windStrength;

    public bool wind;
    int windRowCount;

    public float breakFactor;

    // Use this for initialization
    void Awake() {
        particleList = new List<MonoParticle>();    //Create New list
        springDampenerList = new List<SpringDamper>();
        triangleList = new List<Triangle>();

        SpawnParticles(width, height);          //Spawn in the particles
        SetSpringDampers();
        SetTriangles();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        List<SpringDamper> temp = new List<SpringDamper>();
        
        foreach(SpringDamper sd in springDampenerList)  //temp list for spring dampers
        {
            temp.Add(sd);
        }

	   foreach (MonoParticle mp in particleList)    //Apply Gravity
        {
            mp.particle.force = Vector3.zero;
            mp.particle.force = (gravity * Vector3.down) * mp.particle.mass;
        }

        foreach(SpringDamper sd in temp)  //Calculate force of the springs
        {
                     
            sd.dampingFactor = dampingFactor;
            sd.springConst = springConst;
            sd.restLength = restLength;
            sd.ComputeForce();

            if (sd.BreakHappens(breakFactor))
            {
                springDampenerList.Remove(sd);
            }
        }

        if (wind)
        {
            foreach (Triangle t in triangleList)     //Calculate triangle forces
            {
                 t.CalculateAeroFoce(Vector3.forward * windStrength);               
            }
        }
        

        foreach (MonoParticle mp in particleList)       //Move Particles
        {
            if (mp.anchor == false)
                mp.transform.position = mp.particle.UpdateParticle();

            else           //Is Anchor
                mp.particle.position = mp.transform.position;
        }
    }

    void SpawnParticles(int width, int height)
    {
        float yPosition = 0f;
        float xPosition = 0f;
        int count = 0;

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++, count++)
            {
                GameObject temp = Instantiate(particlePrefab, new Vector3(xPosition, yPosition, 0), new Quaternion()) as GameObject;
                temp.GetComponent<MonoParticle>().particle = new Particle(new Vector3(xPosition, yPosition, 0), new Vector3(0,0,0), 1);
                particleList.Add(temp.GetComponent<MonoParticle>());
                temp.name = "Point " + (count).ToString();
                xPosition += 1f + padding;
            }
            xPosition = 0f;
            yPosition += 1f + padding;
        }

        particleList[particleList.Count - 1].anchor = true;
        particleList[particleList.Count - width].anchor = true;

        particleList[0].anchor = true;
        particleList[width - 1].anchor = true;
    }

    void SetSpringDampers()
    {
        foreach (MonoParticle p in particleList)
        {
            int index = FindIndex(particleList, p);
            p.particle.neighbors = new List<Particle>();

            //if (index != particleList.Count - 1)
            //{
            //    p.neighbors.Add(particleList[index + 1]);
            //    SpringDamper sd = new SpringDamper(p.particle, particleList[index + 1].particle, springConst, dampingFactor, restLength);
            //    springDampenerList.Add(sd);
            //}

            //Find and set neighbors
            if ((index + 1) % width > index % width)                                                                            //immediate right
            {
                p.particle.neighbors.Add(particleList[index + 1].particle);
                SpringDamper sd = new SpringDamper(p.particle, particleList[index + 1].particle, springConst, dampingFactor, restLength);
                springDampenerList.Add(sd);
            }

            if (index + width < particleList.Count)                                                                            //immediate up
            {
                p.particle.neighbors.Add(particleList[index + width].particle);
                SpringDamper sd = new SpringDamper(p.particle, particleList[index + width].particle, springConst, dampingFactor, restLength);
                springDampenerList.Add(sd);
            }

            if (index + width - 1 < particleList.Count && index - 1 >= 0 && (index - 1) % width < index % width)                //Top left
            {
                p.particle.neighbors.Add(particleList[index + width - 1].particle);
                SpringDamper sd = new SpringDamper(p.particle, particleList[index + width - 1].particle, springConst, dampingFactor, restLength);
                springDampenerList.Add(sd);
            }


            if (index + width + 1 < particleList.Count && (index + 1) % width > index % width)                                 //Top right
            {
                p.particle.neighbors.Add(particleList[index + width + 1].particle);
                SpringDamper sd = new SpringDamper(p.particle, particleList[index + width + 1].particle, springConst, dampingFactor, restLength);
                springDampenerList.Add(sd);
            }
        }
    }

    void SetTriangles()
    {
        //First pass
        foreach (MonoParticle mp in particleList)
        {
            int index = FindIndex(particleList, mp);

            Triangle t;
            if (index % width != width - 1 && index + width < particleList.Count)
            {
                t = new Triangle(particleList[index], particleList[index + 1], particleList[index + width]);
                triangleList.Add(t);
            }
        }

        //Second Pass
        foreach (MonoParticle mp in particleList)
        {
            int index = FindIndex(particleList, mp);

            Triangle t;
            if (index >= width && index + 1 < particleList.Count && index % width != width - 1)
            {
                t = new Triangle(particleList[index], particleList[index + 1], particleList[index - width + 1]);
                triangleList.Add(t);
            }
        }

    }

    public int FindIndex(List<MonoParticle> list, MonoParticle item)
    {
        int index = 0;

        //Get index
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == item)
            {
                index = i;
                break;
            }

        }

        return index;
    }

    public int FindIndex(List<Triangle> list, Triangle item)
    {
        int index = 0;

        //Get index
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == item)
            {
                index = i;
                break;
            }

        }

        return index;
    }
}
