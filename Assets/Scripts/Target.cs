using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject prefab;
    public int agentNumber;
    public float maxDistance;
    public int minMass;
    public int maxMass;

    // Use this for initialization
    void Awake () {
        for (int i = 0; i < agentNumber; i++)
        {
            Vector3 pos = new Vector3();
            pos.x = Random.Range(-maxDistance, maxDistance);
            pos.y = Random.Range(-maxDistance, maxDistance);
            pos.z = Random.Range(-maxDistance, maxDistance);

            GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

            SeekingBehavior sb = temp.GetComponent<SeekingBehavior>();
            sb.target = gameObject.transform;

            Agent ag = temp.GetComponent<Agent>();
            ag.mass = Random.Range(minMass, maxMass);
            ag.velocity = ag.transform.position - transform.position;
        } 
	}
}
