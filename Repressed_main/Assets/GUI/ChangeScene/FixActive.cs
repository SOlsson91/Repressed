using UnityEngine;
using System.Collections;

public class FixActive : MonoBehaviour 
{
	public GameObject m_Child;

	private bool m_Start = true;

	// Update is called once per frame
	void Update () 
	{
		if(m_Start)
		{
			m_Child.SetActive(true);
			m_Start = false;
		}
	}
}
