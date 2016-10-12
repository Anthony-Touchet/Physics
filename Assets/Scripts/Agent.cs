using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
    public int mass;
    public Vector3 velocity;
    // Applying a Force
    // F = mass * velocity
    // velocity = F / mass

    void LateUpdate()
    {
        transform.position += velocity / mass;    //Add velocity to position. Multiply by delta time to make it smooth.
    }
}
