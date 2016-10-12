using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public GameObject prefab;
    public int agentNumber;
    public float maxDistance;
    public int minMass;
    public int maxMass;
    [Range(.1f, 1.5f)]public float steeringBehavior;

    // Use this for initialization
    void Awake () {
        steeringBehavior = 1;
        for (int i = 0; i < agentNumber; i++)
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-maxDistance, maxDistance);
            pos.y = Random.Range(-maxDistance, maxDistance);
            pos.z = Random.Range(-maxDistance, maxDistance);

            GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

            SeekingBehavior sb = temp.GetComponent<SeekingBehavior>();
            sb.target = gameObject.transform;
            sb.steeringFactor = steeringBehavior;

            MonoBoid mb = temp.GetComponent<MonoBoid>();
            mb.mass = Random.Range(minMass, maxMass);
            mb.velocity = Vector3.up;
        } 
	}

    void Update()
    {
        foreach (SeekingBehavior sb in FindObjectsOfType<SeekingBehavior>())
        {
            sb.target = gameObject.transform;
            sb.steeringFactor = steeringBehavior;
        }
    }
}
