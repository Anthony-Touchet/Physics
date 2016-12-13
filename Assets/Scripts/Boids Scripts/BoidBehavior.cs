// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoidBehavior.cs" company="Touchet Corp">
//   COPYRIGHT BY ANTHONY TOUCHET
// </copyright>
// <summary>
//   Defines the BoidBehavior type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Boids_Scripts
{
    using UnityEngine;

    public class BoidBehavior : MonoBehaviour
    {
        [HideInInspector]public Vector3 Velocity;
    
        public float Mass;

        private void LateUpdate()
        {
            // Were we will sum up all of the vectors.
            transform.position += Velocity;
            transform.forward = Velocity.normalized;
        }
    }
}
