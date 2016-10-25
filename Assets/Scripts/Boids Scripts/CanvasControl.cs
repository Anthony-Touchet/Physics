using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

    public List<BoidController> boidControllers;
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
        tendency.value = 0;
        GameObject.Find("Target").transform.position = Vector3.zero;
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
    }
}
