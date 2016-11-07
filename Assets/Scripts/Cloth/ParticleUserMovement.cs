using UnityEngine;
using System.Collections;

public class ParticleUserMovement : MonoBehaviour {

    GameObject current = null;

	// Update is called once per frame
	void Update () {
        

        if (Input.GetMouseButtonDown(0))
        {
            if(current != null)
            {
                current.GetComponent<MonoParticle>().anchor = false;
            }

            if(ShootRay() != null)
            {
                Debug.Log(ShootRay().name);
            }
        }
	}

    void FixedUpdate()
    {
        if(current != null)
            current.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, current.transform.position.z);
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
