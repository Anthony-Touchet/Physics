using UnityEngine;

namespace Assets.Scripts.Boids_Scripts
{
    public class BoidBehavior : MonoBehaviour {

        [HideInInspector]public Vector3 Velocity;
    
        public float Mass;

        private void LateUpdate() {                    //Were we will sum up all of the vectors.

            transform.position += Velocity;
            transform.forward = Velocity.normalized;
        }
    }
}
