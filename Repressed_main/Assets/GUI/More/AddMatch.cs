using UnityEngine;
using System.Collections;

public class AddMatch_2 : MonoBehaviour 
{
	public GameObject m_LightSource;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			m_LightSource.GetComponent<Match>().m_Count++;
			gameObject.SetActive(false);
		}
	}
}
