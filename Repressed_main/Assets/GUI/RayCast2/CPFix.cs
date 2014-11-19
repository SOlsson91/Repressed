using UnityEngine;
using System.Collections;

public class CPFix : MonoBehaviour 
{
	private bool m_DoOnce = true;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_DoOnce)
		{
			gameObject.SetActive(false);
			m_DoOnce = false;
		}
	}
}
