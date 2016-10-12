using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject prefab;
    public int agentNumber;
    public int maxDistance;

    // Use this for initialization
    void Awake () {
        for (int i = 0; i < agentNumber; i++)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-maxDistance, maxDistance);
            pos.y = Random.Range(-maxDistance, maxDistance);
            pos.z = Random.Range(-maxDistance, maxDistance);

            GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            Agent a = temp.GetComponent<Agent>();
            a.target = gameObject.transform;
            a.mass = Random.Range(5, 20);
        }
        
	}
}
