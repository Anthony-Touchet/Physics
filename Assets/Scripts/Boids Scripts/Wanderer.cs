// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Wanderer.cs" company="Touchet Corp">
//   COPYRIGHT BY ANTHONY TOUCHET
// </copyright>
// <summary>
//   Defines the Wanderer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Boids_Scripts
{
    using UnityEngine;

    public class Wanderer : MonoBehaviour
    {
        public float Boundries;
        public float MaxSpeed;
        public float FindingRange;
        public float Mass;
        [HideInInspector]public Vector3 Position;

        private Vector3 desiredVelocity;
        private Vector3 steering;
        private bool seeking;
        private Vector3 target;

        private Vector3 velocity;

        // Use this for initialization
        // ReSharper disable once UnusedMember.Local
        private void Awake()
        {
            Position = transform.position;
            target = GetTarget();
            seeking = true;
        }

        // Update is called once per frame
        // ReSharper disable once UnusedMember.Local
        private void Update()
        {
            if (seeking)
            {
                desiredVelocity = (target - Position).normalized;
                steering = (desiredVelocity - velocity).normalized;
                velocity += steering / Mass;
            
                // Limit Velocity
                if (velocity.magnitude > MaxSpeed)
                {
                    velocity = velocity.normalized * MaxSpeed;
                }

                seeking = IsClose();

                Position += velocity;
                transform.position = Position;
            }
            else
            {
                target = GetTarget();
                seeking = true;
            }
        }

        private Vector3 GetTarget()
        {
            if (Random.Range(0, 5) == 3)
            {
                return FindObjectOfType<BoidController>().transform.position;
            }

            Vector3 t;
            t.x = Random.Range(-Boundries, Boundries);
            t.y = Random.Range(-Boundries, Boundries);
            t.z = Random.Range(-Boundries, Boundries);
            return t;
        }

        private bool IsClose()
        {
            return !((target - Position).magnitude <= FindingRange);
        }
    }
}
