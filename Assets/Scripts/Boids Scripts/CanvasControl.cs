using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

    public BoidController boidController;
    public Slider cohesion;
    public Slider dispersion;
    public Slider alignment;

    // Use this for initialization
    void Start () {
        cohesion.value = boidController.cohesion;
        dispersion.value = boidController.dispersion;
        alignment.value = boidController.alignment;
    }
	
	// Update is called once per frame
	void Update () {
        boidController.cohesion = cohesion.value;
        boidController.dispersion = dispersion.value;
        boidController.alignment = alignment.value;
    }
}
