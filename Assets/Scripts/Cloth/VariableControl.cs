using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VariableControl : MonoBehaviour {

    public GameObject Cam;
    private List<MonoParticle> _particleList;
    private List<SpringDamper> _springDamperList;
    private List<Triangle> _triangleList;

    public GameObject ParticlePrefab;
    public GameObject SpringPrefab;
    public Material LineMaterial;

    private List<GameObject> _springdrawers;

    public int Width;
    public int Height;
    public float Padding;

    public float Mass;
    public float Gravity;
    [Range(0f, 100f)]
    public float SpringConst;
    [Range(0f, 10f)]
    public float DampingFactor;
    [Range(0f, 25f)]public float RestLength;

    [Range(0f, 10f)]
    public float WindStrength;

    public bool Wind;

    [Range(0.01f, 10f)]
    public float BreakFactor;

    // Use this for initialization
    private void Awake() {
        _particleList = new List<MonoParticle>();    //Create New list
        _springDamperList = new List<SpringDamper>();
        _triangleList = new List<Triangle>();
        _springdrawers = new List<GameObject>();

        SpawnParticles(Width, Height);          //Spawn in the particles
        SetSpringDampers();
        SetTriangles();

        Vector3 total = Vector3.zero;
        foreach (MonoParticle mp in _particleList)
        {
            total += mp.Particle.Position;
        }
        total = total / _particleList.Count;
        total.z = -35f;
        Cam.transform.position = total;
    }
	
    // Update is called once per frame
    private void FixedUpdate () {
        List<SpringDamper> tempSdList = new List<SpringDamper>();
        List<Triangle> tempTriList = new List<Triangle>();

        foreach (SpringDamper sd in _springDamperList)  //temp list for spring dampers
        {
            tempSdList.Add(sd);
        }

        foreach (Triangle t in _triangleList)  //temp list for Triangles
        {
            tempTriList.Add(t);
        }

        foreach (MonoParticle mp in _particleList)    //Apply Gravity
        {
            mp.Particle.Force = Vector3.zero;
            mp.Particle.Force = (Gravity * Vector3.down) * mp.Particle.Mass;
        }

        foreach(SpringDamper sd in tempSdList)  //Calculate force of the springs
        {                   
            sd.DampingFactor = DampingFactor;
            sd.SpringConst = SpringConst;
            sd.RestLength = RestLength;
            sd.ComputeForce();

            if (!sd.BreakHappens(BreakFactor) && (sd.P1 != null && sd.P2 != null)) continue;
            Destroy(_springdrawers[_springDamperList.IndexOf(sd)]);            
            _springdrawers.Remove(_springdrawers[_springDamperList.IndexOf(sd)]);            
            _springDamperList.Remove(sd);
        }

        if (Wind)
        {
            foreach (Triangle t in tempTriList)     //Calculate triangle forces
            {         
            
                if (!_springDamperList.Contains(t.P1P2) || !_springDamperList.Contains(t.P2P3)
                    || !_springDamperList.Contains(t.P3P1))
                {
                    _triangleList.Remove(t);
                }
                else
                    t.CalculateAeroForce(Vector3.forward * WindStrength);
            }                                
        }      

        foreach (MonoParticle mp in _particleList)       //Move Particles
        {
            if (Camera.main.WorldToScreenPoint(mp.Particle.Position).y <= 10f)  //Floor
            {
                if (mp.Particle.Force.y < 0f)
                    mp.Particle.Force.y = 0;
                mp.Particle.Velocity = -mp.Particle.Velocity * .65f;
            }

            if (Camera.main.WorldToScreenPoint(mp.Particle.Position).y > Screen.height - 10f)  //Ceiling
            {
                if (mp.Particle.Force.y > 0f)
                    mp.Particle.Force.y = 0;
                mp.Particle.Velocity = -mp.Particle.Velocity * .65f;
            }

            if (Camera.main.WorldToScreenPoint(mp.Particle.Position).x < 10f)    //Left Wall
            {
                if (mp.Particle.Force.x < 0f)
                    mp.Particle.Force.x = 0;
                mp.Particle.Velocity = -mp.Particle.Velocity;
            }

            if (Camera.main.WorldToScreenPoint(mp.Particle.Position).x > Screen.width - 10f)    //Left Wall
            {
                if (mp.Particle.Force.x > 0f)
                    mp.Particle.Force.x = 0;
                mp.Particle.Velocity = -mp.Particle.Velocity * .65f;
            }

            if (mp.Anchor == false)
                mp.transform.position = mp.Particle.UpdateParticle();

            else           //Is Anchor
                mp.Particle.Position = mp.transform.position;
        }

    }

    private void LateUpdate()
    {
        List<GameObject> tempSdList = new List<GameObject>();
        foreach (GameObject sd in _springdrawers)  //temp list for spring dampers
        {
            tempSdList.Add(sd);
        }

        for (int i = 0; i < tempSdList.Count; i++)
        {
            if(tempSdList[i] != null)
            {
                LineRenderer lr = tempSdList[i].GetComponent<LineRenderer>();
                lr.SetPosition(0, _springDamperList[i].P1.Position);
                lr.SetPosition(1, _springDamperList[i].P2.Position);

                float tension = (_springDamperList[i].P2.Position - _springDamperList[i].P1.Position).magnitude;
                Color colorFraction = Color.red / 3.25f;

                lr.material.color = colorFraction * tension /*+ Color.white / tension*/;
                lr.SetColors(lr.material.color, lr.material.color);
            }          
        }
    }

    private void SpawnParticles(int width, int height)
    {
        float yPosition = 0f;
        float xPosition = 0f;
        int count = 0;

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++, count++)
            {
                GameObject temp = Instantiate(ParticlePrefab, new Vector3(xPosition, yPosition, 0), new Quaternion()) as GameObject;
                if (temp != null)
                {
                    temp.GetComponent<MonoParticle>().Particle = new Particle(new Vector3(xPosition, yPosition, 0), new Vector3(0,0,0), Mass);
                    _particleList.Add(temp.GetComponent<MonoParticle>());
                    temp.name = "Point " + (count).ToString();
                }
                xPosition += 1f + Padding;
            }
            xPosition = 0f;
            yPosition += 1f + Padding;
        }

        _particleList[_particleList.Count - 1].Anchor = true;
        _particleList[_particleList.Count - width].Anchor = true;

        _particleList[0].Anchor = true;
        _particleList[width - 1].Anchor = true;
    }

    private void SetSpringDampers()
    {
        foreach (MonoParticle p in _particleList)
        {
            int index = FindIndex(_particleList, p);
            p.Particle.Neighbors = new List<Particle>();

            //Find and set neighbors
            if ((index + 1) % Width > index % Width)                                                                            //immediate right
            {
                p.Particle.Neighbors.Add(_particleList[index + 1].Particle);
                SpringDamper sd = new SpringDamper(p.Particle, _particleList[index + 1].Particle, SpringConst, DampingFactor, RestLength);
                _springdrawers.Add(CreateSpringDrawer(sd));
                _springDamperList.Add(sd);

            }

            if (index + Width < _particleList.Count)                                                                            //immediate up
            {
                p.Particle.Neighbors.Add(_particleList[index + Width].Particle);
                SpringDamper sd = new SpringDamper(p.Particle, _particleList[index + Width].Particle, SpringConst, DampingFactor, RestLength);
                _springdrawers.Add(CreateSpringDrawer(sd));
                _springDamperList.Add(sd);
            }

            if (index + Width - 1 < _particleList.Count && index - 1 >= 0 && (index - 1) % Width < index % Width)                //Top left
            {
                p.Particle.Neighbors.Add(_particleList[index + Width - 1].Particle);
                SpringDamper sd = new SpringDamper(p.Particle, _particleList[index + Width - 1].Particle, SpringConst, DampingFactor, RestLength);
                _springdrawers.Add(CreateSpringDrawer(sd));
                _springDamperList.Add(sd);
            }


            if (index + Width + 1 < _particleList.Count && (index + 1) % Width > index % Width)                                 //Top right
            {
                p.Particle.Neighbors.Add(_particleList[index + Width + 1].Particle);
                SpringDamper sd = new SpringDamper(p.Particle, _particleList[index + Width + 1].Particle, SpringConst, DampingFactor, RestLength);
                _springdrawers.Add(CreateSpringDrawer(sd));
                _springDamperList.Add(sd);
            }
        }
    }

    void SetTriangles()
    {
        //First pass
        foreach (MonoParticle mp in _particleList)
        {
            int index = FindIndex(_particleList, mp);

            Triangle t;
            if (index % Width != Width - 1 && index + Width < _particleList.Count)
            {
                t = new Triangle(_particleList[index], _particleList[index + 1], _particleList[index + Width]);
                foreach (SpringDamper sd in _springDamperList)
                {
                    if ((sd.P1 == t.P1 && sd.P2 == t.P2) || (sd.P1 == t.P2 && sd.P2 == t.P1))
                        t.P1P2 = sd;

                    else if ((sd.P1 == t.P2 && sd.P2 == t.P3) || (sd.P1 == t.P3 && sd.P2 == t.P2))
                        t.P2P3 = sd;

                    else if ((sd.P1 == t.P3 && sd.P2 == t.P1) || (sd.P1 == t.P1 && sd.P2 == t.P3))
                        t.P3P1 = sd;
                }
                _triangleList.Add(t);
            }
        }

        //Second Pass
        foreach (MonoParticle mp in _particleList)
        {
            int index = FindIndex(_particleList, mp);

            Triangle t;
            if (index >= Width && index + 1 < _particleList.Count && index % Width != Width - 1)
            {
                t = new Triangle(_particleList[index], _particleList[index + 1], _particleList[index - Width + 1]);
                _triangleList.Add(t);
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
        var index = 0;

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

    private GameObject CreateSpringDrawer(SpringDamper sd)
    {
        var go = Instantiate(SpringPrefab, (sd.P1.Position + sd.P2.Position) / 2f, new Quaternion()) as GameObject;
        if (go == null) return null;
        var lr = go.GetComponent<LineRenderer>();
        lr.materials[0] = LineMaterial;
        lr.SetWidth(.1f, .1f);
        return go;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}