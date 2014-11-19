using UnityEngine;
using System.Collections;

public class AddBattery : MonoBehaviour 
{
	public GameObject m_LightSource;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			m_LightSource.GetComponent<FlashLightChecker>().NewBattery();
			gameObject.SetActive(false);
		}
	}
}
