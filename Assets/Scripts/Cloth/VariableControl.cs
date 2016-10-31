using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VariableControl : MonoBehaviour {

    Vector3 windDirection;
    public float windStrength;

    List<MonoParticle> particleList;
    [SerializeField]
    List<SpringDamper> springDampenerList;
    public GameObject particlePrefab;

    public int width;
    public int height;
    public float padding;

    public float gravity;
    public float springConst, dampingFactor, restLength;

    // Use this for initialization
    void Awake() {
        particleList = new List<MonoParticle>();    //Create New list
        springDampenerList = new List<SpringDamper>();

        SpawnParticles(width, height);          //Spawn in the particles
        SetSpringDampers();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	   

        foreach(SpringDamper sd in springDampenerList)
        {
            sd.dampingFactor = dampingFactor;
            sd.springConst = springConst;
            sd.restLength = restLength;
            sd.ComputeForce();
        }

        foreach (MonoParticle mp in particleList)
        {
            mp.particle.UpdateParticle(gravity);
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
                temp.GetComponent<MonoParticle>().particle = new Particle(new Vector3(xPosition, yPosition, 0), new Vector3(0,0,0), 10);
                particleList.Add(temp.GetComponent<MonoParticle>());
                temp.name = "Point " + (count).ToString();
                xPosition += 1f + padding;
            }
            xPosition = 0f;
            yPosition += 1f + padding;
        }

        particleList[particleList.Count - 1].anchor = true;
        particleList[particleList.Count - width].anchor = true;
    }

    void SetSpringDampers()
    {
        foreach (MonoParticle p in particleList)
        {
            int index = 0;
            p.neighbors = new List<MonoParticle>();

            //Get P's index
            for (int i = 0; i < particleList.Count; i++)
            {
                if (particleList[i] == p)
                {
                    index = i;
                    break;
                }

            }

            if(index != particleList.Count - 1)
            {
                p.neighbors.Add(particleList[index + 1]);
                SpringDamper sd = new SpringDamper(p.particle, particleList[index + 1].particle, springConst, dampingFactor, restLength);
                springDampenerList.Add(sd);
            }           

            ////Find and set neighbors
            //if ((index + 1) % width > index % width)                                                                            //immediate right
            //{
            //    p.neighbors.Add(particleList[index + 1]);
            //    SpringDamper sd = new SpringDamper(p.particle, particleList[index + 1].particle, springConst, dampingFactor, restLength);
            //    springDampenerList.Add(sd);
            //}               

            //if (index + height < particleList.Count)                                                                            //immediate up
            //{
            //    p.neighbors.Add(particleList[index + height]);
            //    SpringDamper sd = new SpringDamper(p.particle, particleList[index + height].particle, springConst, dampingFactor, restLength);
            //    springDampenerList.Add(sd);
            //}

            //if (index + height - 1 < particleList.Count && index - 1 >= 0 && (index - 1) % width < index % width)                //Top left
            //{
            //    p.neighbors.Add(particleList[index + height - 1]);
            //    SpringDamper sd = new SpringDamper(p.particle, particleList[index + height - 1].particle, springConst, dampingFactor, restLength);
            //    springDampenerList.Add(sd);
            //}


            //if (index + height + 1 < particleList.Count && (index + 1) % width > index % width)                                 //Top right
            //{
            //    p.neighbors.Add(particleList[index + height + 1]);
            //    SpringDamper sd = new SpringDamper(p.particle, particleList[index + height + 1].particle, springConst, dampingFactor, restLength);
            //    springDampenerList.Add(sd);
            //}
        }
    }
}
