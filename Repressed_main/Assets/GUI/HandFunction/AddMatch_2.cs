using UnityEngine;
using System.Collections;

public class AddMatch : MonoBehaviour 
{
	public GameObject m_LightSource;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			RasmusGameSave.m_MatchCount++;
			//m_LightSource.GetComponent<Match>().m_Count++;
			gameObject.SetActive(false);
		}
	}
}
