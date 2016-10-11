using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject prefab;
    public int agentNumber;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < agentNumber; i++)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-10, 10);
            pos.y = Random.Range(-10, 10);
            pos.z = Random.Range(-10, 10);

            GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            Agent a = temp.GetComponent<Agent>();
            a.target = gameObject.transform;
            a.mass = Random.Range(5, 20);
        }
        
	}
}
