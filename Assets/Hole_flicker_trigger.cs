using UnityEngine;
using System.Collections;

public class Hole_flicker_trigger : MonoBehaviour {


	bool m_closeToDanger;

	 void OnTriggerEnter(Collider other)
		{

		if (other.gameObject.tag == "DangerZone") 
		{
		
			m_closeToDanger = true;


		}



	}
	void OnTriggerExit(Collider other) 
	{

		if (other.tag == "DangerZone") 
		{
		

			m_closeToDanger = false;
		}
	}

	void Update()
	{
		if (GameObject.FindGameObjectWithTag ("MainLight") != null) {
						if (m_closeToDanger) {
								GameObject.FindGameObjectWithTag ("MainLight").GetComponent<FlickeringLight> ().enabled = true;
						} else {
								GameObject.FindGameObjectWithTag ("MainLight").GetComponent<FlickeringLight> ().enabled = false;		
						}
				}
	}

}
