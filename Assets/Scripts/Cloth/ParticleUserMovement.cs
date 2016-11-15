using UnityEngine;
using System.Collections;

public class ParticleUserMovement : MonoBehaviour {

    GameObject current = null;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(ShootRay() != null && ShootRay().GetComponent<MonoParticle>() != null)
            {
                current = ShootRay();
                current.GetComponent<MonoParticle>().anchor = (current.GetComponent<MonoParticle>().anchor == true) ? false : true;
            }
            else
            {
                current = null;
            }
        }
	}

    public void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1) && ShootRay().GetComponent<MonoParticle>() != null)
        {
            current = ShootRay();            
        }

        if (Input.GetMouseButton(1) && current != null)
        {
            current.GetComponent<MonoParticle>().particle.force = Vector3.zero;
            current.GetComponent<MonoParticle>().particle.velocity = Vector3.zero;

            Vector3 mouse = Input.mousePosition;
            mouse.z = 35f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouse);
            worldPos.z = current.transform.position.z;

            current.GetComponent<MonoParticle>().particle.position = worldPos;
            current.transform.position = worldPos;
        }

    }

    public GameObject ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //Ray
        RaycastHit hit = new RaycastHit();                              //Raycast hit that stores the Info of what it hit
        Physics.Raycast(ray.origin, ray.direction, out hit);            //Actual Casting of the ray

        if(hit.transform != null)
            return hit.transform.gameObject;    //Return the GameObject
        return null;
    }
}
