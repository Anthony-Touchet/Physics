using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Boids_Scripts
{
    public class CanvasControl : MonoBehaviour {

        public List<BoidController> BoidControllers;
        public GameObject Predator;
        public Slider Cohesion;
        public Slider Dispersion;
        public Slider Alignment;
        public Slider Tendency;

        // Use this for initialization
        private void Start () {
            foreach(BoidController bc in BoidControllers)
            {
                bc.CohesionFactor = Cohesion.value;
                bc.DispersionFactor = Dispersion.value;
                bc.AlignmentFactor = Alignment.value;
                bc.TendencyFactor = 0f;
            }
        }
	
        // Update is called once per frame
        private void Update () {
            Presets();
        
            foreach (BoidController bc in BoidControllers)
            {
                bc.CohesionFactor = Cohesion.value;
                bc.DispersionFactor = Dispersion.value;
                bc.AlignmentFactor = Alignment.value;
                if (bc.name == "GroupA")
                    bc.TendencyFactor = Tendency.value;
            }        
        }

        public void SetTendencyToZero()
        {
            var tar = GameObject.Find("Target");
            Tendency.value = 0;
            tar.transform.position = Vector3.zero;
            tar.GetComponent<Wanderer>().Position = Vector3.zero;
        }

        public void Presets()
        {
            //Preset 1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Cohesion.value = 1f;
                Dispersion.value = 0.3f;
                Alignment.value = 0f;
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Cohesion.value = 0.5f;
                Dispersion.value = 0.5f;
                Alignment.value = 1f;

            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Cohesion.value = 0.25f;
                Dispersion.value = 0.75f;
                Alignment.value = 0.5f;
            }

            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Cohesion.value = 0.55f;
                Dispersion.value = 0.1f;
                Alignment.value = 1f;
            }
        }

        public void PredatorButton()
        {
            Predator.GetComponent<TargetControl>().enabled = !Predator.GetComponent<TargetControl>().enabled;
            Predator.GetComponent<Wanderer>().enabled = !Predator.GetComponent<Wanderer>().enabled;
        }
    }
}
