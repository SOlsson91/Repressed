using UnityEngine;
using System.Collections;

public class RedlLightFinish : MonoBehaviour 
{
	public GameObject[] m_Lights;

	private bool m_Started = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GetComponent<Locked>().GetLocked() == true)
		{
			if(m_Started == false)
			{
				m_Started = true;
				for(int i = 0; i < m_Lights.Length; i++)
				{
					m_Lights[i].GetComponent<RedLight>().SetNewStartPosition();
				}
			}
			for(int i = 0; i < m_Lights.Length; i++)
			{
				m_Lights[i].GetComponent<RedLight>().m_Active = true;
			}
		}
		else
		{
			for(int i = 0; i < m_Lights.Length; i++)
			{
				m_Lights[i].GetComponent<Light>().color = Color.white;
				m_Lights[i].GetComponent<RedLight>().m_Active = false;
			}
		}
	}
}
