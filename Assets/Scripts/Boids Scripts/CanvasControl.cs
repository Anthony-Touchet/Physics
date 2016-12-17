// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CanvasControl.cs" company="Touchet Corp">
//   COPYRIGHT BY ANTHONY TOUCHET
// </copyright>
// <summary>
//   Defines the BoidBehavior type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts.Boids_Scripts
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CanvasControl : MonoBehaviour
    {
        public List<BoidController> BoidControllers;
        public GameObject Predator;
        public Slider Cohesion;
        public Slider Dispersion;
        public Slider Alignment;
        public Slider Tendency;

        public void SetTendencyToZero()
        {
            var tar = GameObject.Find("Target");
            Tendency.value = 0;
            tar.transform.position = Vector3.zero;
            tar.GetComponent<Wanderer>().Position = Vector3.zero;
        }

        public void Presets()
        {
            // Preset 1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Cohesion.value = 1f;
                Dispersion.value = 0f;
                Alignment.value = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Cohesion.value = 0f;
                Dispersion.value = 1f;
                Alignment.value = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Cohesion.value = 1f;
                Dispersion.value = 1f;
                Alignment.value = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Cohesion.value = 1f;
                Dispersion.value = 0.2f;
                Alignment.value = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Cohesion.value = 1f;
                Dispersion.value = 0.5f;
                Alignment.value = 0f;
            }
        }

        public void PredatorButton()
        {
            Predator.GetComponent<TargetControl>().enabled = !Predator.GetComponent<TargetControl>().enabled;
            Predator.GetComponent<Wanderer>().enabled = !Predator.GetComponent<Wanderer>().enabled;
        }

        // Use this for initialization
        // ReSharper disable once UnusedMember.Local
        private void Start()
        {
            foreach (BoidController bc in BoidControllers)
            {
                bc.CohesionFactor = Cohesion.value;
                bc.DispersionFactor = Dispersion.value;
                bc.AlignmentFactor = Alignment.value;
                bc.TendencyFactor = 0f;
            }
        }

        // Update is called once per frame
        // ReSharper disable once UnusedMember.Local
        private void Update()
        {
            Presets();
        
            foreach (BoidController bc in BoidControllers)
            {
                bc.CohesionFactor = Cohesion.value;
                bc.DispersionFactor = Dispersion.value;
                bc.AlignmentFactor = Alignment.value;
                if (bc.name == "GroupA")
                {
                    bc.TendencyFactor = Tendency.value;
                }
            }        
        }
    }
}
