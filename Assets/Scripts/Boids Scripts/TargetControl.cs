using UnityEngine;

namespace Assets.Scripts.Boids_Scripts
{
    public class TargetControl : MonoBehaviour {

        public float Range;

        // Update is called once per frame
        private void Update () {
            var movement = Vector3.zero;

            if (Input.GetKey(KeyCode.W) && transform.position.z < Range)
                movement += Vector3.forward;

            if (Input.GetKey(KeyCode.S) && transform.position.z > -Range)
                movement += Vector3.back;

            if (Input.GetKey(KeyCode.A) && transform.position.x > -Range)
                movement += Vector3.left;

            if (Input.GetKey(KeyCode.D) && transform.position.x < Range)
                movement += Vector3.right;

            if (Input.GetKey(KeyCode.PageUp) && transform.position.y < Range)
                movement += Vector3.up;

            if (Input.GetKey(KeyCode.PageDown) && transform.position.y > -Range)
                movement += Vector3.down;


            transform.position += movement;
        }
    }
}
