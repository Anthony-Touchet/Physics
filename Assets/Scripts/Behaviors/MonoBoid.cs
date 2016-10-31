using UnityEngine;
using System.Collections;
using Inferances;

public class MonoBoid : MonoBehaviour{
    public Agent agent;
    public float mass;

    // Use this for initialization
    void Awake () {
        agent = new Agent(mass);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        agent.UpdateVelocity();
        transform.position = agent.position;
    }  
}
