﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Boids_Scripts
{
    public class BoidController : MonoBehaviour {

        public GameObject BoidPrefab;
        public int BoidNumber;
        public float MaxSpawnDistance;
        public float BoxBoundries;
        public float MaxSpeed;

        public Transform Target;
        public float TargetRange;
        public Transform Center;

        public float MinMass;
        public float MaxMass;

        private List<BoidBehavior> _boids;

        [Range(0.0f, 1.0f)]
        public float CohesionFactor;

        [Range(0.0f, 1.0f)]
        public float DispersionFactor;

        [Range(0.0f, 1.0f)]
        public float AlignmentFactor;

        [Range(-1.0f, 1.0f)]
        public float TendencyFactor;

        //Deals with spawning and adding boids to a list.
        private void Awake()
        {
            MaxSpeed = (MaxSpeed <= 0) ? 1 : MaxSpeed;
            TargetRange = (TargetRange <= 0) ? 20 : TargetRange;

            _boids = new List<BoidBehavior>();
            var pos = Vector3.zero;
            for (var i = 0; i < BoidNumber; i++)    //Spawn and add to list
            {
                pos.x = Random.Range(-MaxSpawnDistance, MaxSpawnDistance);
                pos.y = Random.Range(-MaxSpawnDistance, MaxSpawnDistance);
                pos.z = Random.Range(-MaxSpawnDistance, MaxSpawnDistance);

                var temp = Instantiate(BoidPrefab, transform.position + pos, new Quaternion()) as GameObject;

                if (temp == null) continue;
                var bb = temp.GetComponent<BoidBehavior>();
                bb.Velocity = bb.transform.position.normalized;
                bb.Mass = Random.Range(MinMass, MaxMass);

                _boids.Add(bb);
            }
        }
    
        //Where Math is applied to the boids
        private void FixedUpdate () {
            foreach(BoidBehavior bb in _boids)
            {
                Vector3 r1 = Cohesion(bb) * CohesionFactor;
                Vector3 r2 = Dispersion(bb);
                Vector3 r3 = Alignment(bb) * AlignmentFactor;
                Vector3 walls = WallBoundries(bb);
                Vector3 tendTowards = TendTowardsPlace(bb) * TendencyFactor;
                bb.Velocity += ((r1 + r2 + r3 + walls + tendTowards) / bb.Mass);
                LimitVelocity(bb);
            }

            Center.position = MarkCenterOfMass();
        }

        //Calculates the CohesionFactor vector for a boid
        private Vector3 Cohesion(BoidBehavior b)
        {
            //Rule 1: center of Mass
            var percCenter = Vector3.zero;
            foreach (BoidBehavior bj in _boids)
            {
                if(bj != b)
                {
                    percCenter += bj.transform.position;
                }
                
            }

            percCenter = percCenter / (_boids.Count - 1); //Divide the Precived Center by how many other boids there are.

            return (percCenter - b.transform.position).normalized;   //Set rule one. MUST BE NORMALIZED
        }

        //Calculates the Dispersion vector of a boid
        private Vector3 Dispersion(BoidBehavior b)
        {
            //Rule 2: distancing each other appart
            Vector3 avoid = Vector3.zero;
            foreach (BoidBehavior bj in _boids)
            {
                if ((bj.transform.position - b.transform.position).magnitude <= 25 * DispersionFactor && bj != b)
                {
                    avoid -= bj.transform.position - b.transform.position;
                }
            }

            return avoid.normalized;
        }

        //Calculates the Alignment vector of a boid
        private Vector3 Alignment(BoidBehavior b)
        {
            //Rule 3: Alignment
            Vector3 percVelocity = Vector3.zero;
            foreach (BoidBehavior bj in _boids)
            {
                if(bj != b)
                    percVelocity += bj.Velocity;
            }

            percVelocity = percVelocity / (_boids.Count - 1);

            Vector3 rule3 = (percVelocity - b.Velocity).normalized;

            return rule3;
        }

        //Sets wall Boundries for the boids
        private Vector3 WallBoundries(BoidBehavior b)
        {
            Vector3 bounds = new Vector3();

            if (b.transform.position.x > BoxBoundries)
                bounds += new Vector3(-10, 0, 0);
            else if (b.transform.position.x < -BoxBoundries)
                bounds += new Vector3(10, 0, 0);

            if (b.transform.position.y > BoxBoundries)
                bounds += new Vector3(0, -10, 0);
            else if (b.transform.position.y < -BoxBoundries)
                bounds += new Vector3(0, 10, 0);

            if (b.transform.position.z > BoxBoundries)
                bounds += new Vector3(0, 0, -10);
            else if (b.transform.position.z < -BoxBoundries)
                bounds += new Vector3(0, 0, 10);

            return bounds;
        }

        //Setting an object to the center of Mass for all the object
        private Vector3 MarkCenterOfMass()
        {
            Vector3 allPositions = Vector3.zero;
            foreach (BoidBehavior bb in _boids)
            {
                allPositions += bb.transform.position;
            }

            Vector3 centerOfMass = allPositions / _boids.Count;
            return centerOfMass;
        }

        //Limiting Velocity
        private void LimitVelocity(BoidBehavior bb)
        {
            if(bb.Velocity.magnitude > MaxSpeed)
            {                   //Normalizing                       times speed you want
                bb.Velocity = (bb.Velocity / bb.Velocity.magnitude) * MaxSpeed;
            }
        } 

        //Function that caculates a tendecy for a boid to move in a certain direction
        private Vector3 TendTowardsPlace(BoidBehavior bb)
        {
            if (TendencyFactor > 0)
            {
                if(GetColor(bb) != Color.cyan)
                    SetColor(Color.cyan, bb);
                return (Target.position - bb.transform.position).normalized;
            }
            

            else if (TendencyFactor < 0 && (Target.position - bb.transform.position).magnitude < TargetRange)
            {
                if (GetColor(bb) != Color.yellow)
                    SetColor(Color.yellow, bb);
                return (Target.position - bb.transform.position).normalized;
            }

            else
            {
                if (GetColor(bb) != Color.black)
                    SetColor(new Color(0, 213, 255, 255), bb);
                return Vector3.zero;
            }
            
        }

        private void SetColor(Color c, BoidBehavior bb)
        {
            bb.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = c;
        }

        private Color GetColor(BoidBehavior bb)
        {
            return bb.transform.GetChild(1).GetComponent<MeshRenderer>().material.color;
        }
    }
}