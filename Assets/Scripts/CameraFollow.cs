using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public List<Transform> agents;
    [Range(0.0f, 1.0f)]public float lookAt;

	// Use this for initialization
	void Start () {
        foreach (Agent a in FindObjectsOfType<Agent>())
        {
            agents.Add(a.transform);
        }
        agents.Add(GameObject.Find("Target").transform);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 lookAtPoint = Vector3.zero;

        foreach(Transform t in agents)
        {
            lookAtPoint += t.position;
        }

        lookAtPoint = lookAtPoint / agents.Count;
        lookAtPoint *= lookAt;
        transform.LookAt(lookAtPoint);
	}
}
