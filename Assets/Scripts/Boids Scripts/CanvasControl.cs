using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

    public List<BoidController> boidControllers;
    public GameObject predator;
    public Slider cohesion;
    public Slider dispersion;
    public Slider alignment;
    public Slider tendency;

    // Use this for initialization
    void Start () {
        foreach(BoidController bc in boidControllers)
        {
            bc.cohesion = cohesion.value;
            bc.dispersion = dispersion.value;
            bc.alignment = alignment.value;
            bc.tendency = 0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Presets();
        
        foreach (BoidController bc in boidControllers)
        {
            bc.cohesion = cohesion.value;
            bc.dispersion = dispersion.value;
            bc.alignment = alignment.value;
            if (bc.name == "GroupA")
                bc.tendency = tendency.value;
        }        
    }

    public void SetTendencyToZero()
    {
        GameObject tar = GameObject.Find("Target");
        tendency.value = 0;
        tar.transform.position = Vector3.zero;
        tar.GetComponent<Wanderer>().position = Vector3.zero;
    }

    public void Presets()
    {
        //Preset 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cohesion.value = 1f;
            dispersion.value = 0.3f;
            alignment.value = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cohesion.value = 0.5f;
            dispersion.value = 0.5f;
            alignment.value = 1f;

        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cohesion.value = 0.25f;
            dispersion.value = 0.75f;
            alignment.value = 0.5f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cohesion.value = 0.55f;
            dispersion.value = 0.1f;
            alignment.value = 1f;
        }
    }

    public void PredatorButton()
    {
        predator.GetComponent<TargetControl>().enabled = !predator.GetComponent<TargetControl>().enabled;
        predator.GetComponent<Wanderer>().enabled = !predator.GetComponent<Wanderer>().enabled;
    }
}
