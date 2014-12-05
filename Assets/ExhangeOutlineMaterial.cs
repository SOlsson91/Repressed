using UnityEngine;
using System.Collections;

public class RaycastShooting : MonoBehaviour {
	
	

	RaycastHit hit;
	GameObject outlineChild;

	

	void FixedUpdate ()
	{
		
		
			

			Cast();
		
		
	}
	public int Cast()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Debug.DrawRay(ray.origin,ray.direction * 10, Color.red);
		if(Physics.Raycast(ray, out hit, 10))
		{
			outlineChild = hit.collider.gameObject;
			Debug.Log ("asdf");

		}
		return 0;
	}
	



}