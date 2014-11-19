using UnityEngine;
using System.Collections;

public class handleGodRay : MonoBehaviour 
{



 
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name == "GodRayTrigger")
		{
			if (Camera.main.GetComponent<GodRays> ().enabled) 
			{
				Camera.main.GetComponent<GodRays> ().enabled = false;
					
			} 
			else 
			{
				Camera.main.GetComponent<GodRays> ().enabled = true;
			}
		}

	}
	

}
