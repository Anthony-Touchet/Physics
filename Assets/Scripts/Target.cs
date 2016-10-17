using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject prefab;
    public int agentNumber;
    public float maxDistance;
    public int minMass;
    public int maxMass;
    [Range(.1f, 1.5f)]public float steeringBehavior;
    public float radius;

    // Use this for initialization
    void Start () {
        steeringBehavior = 1;
        for (int i = 0; i < agentNumber; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-maxDistance, maxDistance);
            pos.y = Random.Range(-maxDistance, maxDistance);
            pos.z = Random.Range(-maxDistance, maxDistance);

            GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

            SeekandArrive sa = temp.GetComponent<SeekandArrive>();
            sa.target = gameObject.transform;
            sa.steeringFactor = steeringBehavior;
            sa.radius = radius;

            MonoBoid mb = temp.GetComponent<MonoBoid>();
            mb.mass = Random.Range(minMass, maxMass);
            mb.agent.velocity = Vector3.up;
        } 
	}

    void Update()
    {
        foreach (SeekandArrive sb in FindObjectsOfType<SeekandArrive>())
        {
            sb.target = gameObject.transform;
            sb.steeringFactor = steeringBehavior;
            sb.radius = radius;
        }
    }
}
