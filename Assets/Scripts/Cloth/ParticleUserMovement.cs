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

    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && current != null)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = 35f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouse);
            worldPos.z = current.transform.position.z;
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
