using UnityEngine;

namespace Assets.Scripts.Boids_Scripts
{
    public class Wanderer : MonoBehaviour {

        public float Boundries;
        public float MaxSpeed;
        public float FindingRange;
        public float Mass;

        private Vector3 _desiredVelocity;
        private Vector3 _steering;
        private bool _seeking;
        private Vector3 _target;

        private Vector3 _velocity;
    
        [HideInInspector]public Vector3 Position;
        // Use this for initialization
        private void Awake () {
            Position = transform.position;
            _target = GetTarget();
            _seeking = true;
        }
	
        // Update is called once per frame
        private void Update () {
            if (_seeking)
            {
                _desiredVelocity = (_target - Position).normalized;
                _steering = (_desiredVelocity - _velocity).normalized;
                _velocity += _steering / Mass;
            
                //Limit Velocity
                if (_velocity.magnitude > MaxSpeed)
                    _velocity = _velocity.normalized * MaxSpeed;

                _seeking = IsClose();

                Position += _velocity;
                transform.position = Position;
            }

            else
            {
                _target = GetTarget();
                _seeking = true;
            }
        }

        private Vector3 GetTarget()
        {
            if(Random.Range(0, 5) == 3)
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
            if ((_target - Position).magnitude <= FindingRange)
                return false;
            return true;
        }
    }
}
