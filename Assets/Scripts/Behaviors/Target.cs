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
    public Vector3 pos;

    // Use this for initialization
    void Awake () {
        for (int i = 0; i < agentNumber; i++)
        {
            pos.x = Random.Range(-maxDistance, maxDistance);
            pos.y = Random.Range(-maxDistance, maxDistance);
            pos.z = Random.Range(-maxDistance, maxDistance);

            GameObject temp = Instantiate(prefab, pos, new Quaternion()) as GameObject;

            if (temp.GetComponent<SeekingBehavior>() != null)
            {
                SeekingBehavior sa = temp.GetComponent<SeekingBehavior>();
                sa.target = gameObject.transform;
                sa.steeringFactor = steeringBehavior;
            }

            if (temp.GetComponent<SeekandArrive>() != null)
            {
                SeekandArrive sa = temp.GetComponent<SeekandArrive>();
                sa.target = gameObject.transform;
                sa.steeringFactor = steeringBehavior;
                sa.radius = radius;
            }

            MonoBoid mb = temp.GetComponent<MonoBoid>();
            mb.agent.position = pos;
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

        foreach (SeekingBehavior sb in FindObjectsOfType<SeekingBehavior>())
        {
            sb.target = gameObject.transform;
            sb.steeringFactor = steeringBehavior;
        }
    }
}
