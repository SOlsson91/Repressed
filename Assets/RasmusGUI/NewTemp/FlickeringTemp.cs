using UnityEngine;
using System.Collections;

public class FlickeringTemp : MonoBehaviour 
{
	public FlashLightTemp m_Flashlight;
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			m_Flashlight.StartFlickering();
			gameObject.SetActive(false);
		}
	}
}
