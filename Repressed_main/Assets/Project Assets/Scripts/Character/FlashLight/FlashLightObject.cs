using UnityEngine;
using System.Collections;

public class FlashLightObject : ObjectComponent 
{
	public override void Interact()
	{
		GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLight>().Find();
		gameObject.SetActive (false);
		//Camera.main.GetComponent<Raycasting>().Release();
	}
}
