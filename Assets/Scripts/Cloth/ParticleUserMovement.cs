using UnityEngine;
using System.Collections;

public class ParticleUserMovement : MonoBehaviour {
    private GameObject _current = null;

	// Update is called once per frame
	private void Update ()
	{
	    if (!Input.GetMouseButtonDown(0)) return;
	    if(ShootRay() != null && ShootRay().GetComponent<MonoParticle>() != null)
	    {
	        _current = ShootRay();
	        _current.GetComponent<MonoParticle>().Anchor = (_current.GetComponent<MonoParticle>().Anchor != true);
	    }
	    else
	    {
	        _current = null;
	    }
	}

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1) && ShootRay().GetComponent<MonoParticle>() != null)
        {
            _current = ShootRay();            
        }

        if (!Input.GetMouseButton(1) || _current == null) return;
        _current.GetComponent<MonoParticle>().Particle.Force = Vector3.zero;
        _current.GetComponent<MonoParticle>().Particle.Velocity = Vector3.zero;

        Vector3 mouse = Input.mousePosition;
        mouse.z = 35f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouse);
        worldPos.z = _current.transform.position.z;

        _current.GetComponent<MonoParticle>().Particle.Position = worldPos;
        _current.transform.position = worldPos;
    }

    public GameObject ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //Ray
        RaycastHit hit;                                                 //Raycast hit that stores the Info of what it hit
        Physics.Raycast(ray.origin, ray.direction, out hit);            //Actual Casting of the ray

        return hit.transform != null ? hit.transform.gameObject : null;
    }
}
