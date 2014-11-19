using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour 
{
	public GameObject m_LeftArm;
	public GameObject m_Match;
	public GameObject m_Flashlight;

	public bool m_Active;
	public bool m_MatchSelected = true;
	// Use this for initialization
	void Start () 
	{
		m_Active = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(Input.GetKeyDown("t"))
		//{
		//	m_Match.GetComponent<Match>().Count += 1;
		//}

		if(Input.GetKeyDown("f"))
		{
			if(m_MatchSelected == false)
			{
				if(!m_Active)
				{
					m_Flashlight.SetActive(true);
					m_Match.SetActive(false);
					PutArmUp();
				}
				else
				{
					PutArmDown();
				}
			}
			else
			{
				if(!m_Active && m_Match.GetComponent<Match>().Count > 0)
				{
					m_Flashlight.SetActive(false);
					m_Match.SetActive(true);
					PutArmUp();
					m_Match.GetComponent<Match>().Lit = true;
				}
				else if(m_Match.GetComponent<Match>().Count > 0)
				{
					PutArmDown();
					m_Match.GetComponent<Match>().EndMatch();
				}
			}
		}
		if(!m_Active)
		{
			if(Input.GetKeyDown("1"))
			{
				m_MatchSelected = true;
			}
			if(Input.GetKeyDown("2"))
			{
				m_MatchSelected = false;
			}
		}
		if(m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].time < 0)
		{
			m_LeftArm.GetComponent<Animation>().Stop();
			m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].time = 0;
			m_Flashlight.SetActive(false);
			m_Match.SetActive(false);
		}
	}

	public void PutArmUp()
	{
		m_Active = true;
		m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].speed = 1;
		m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].time = 0;
		m_LeftArm.GetComponent<Animation>().Play();
	}

	public void PutArmDown()
	{
		m_Active = false;
		m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].speed = -1;
		m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].time = m_LeftArm.GetComponent<Animation>().animation["ArmAnimation"].length;
		m_LeftArm.GetComponent<Animation>().Play();
	}

	public int GetMatchesCount()
	{
		return m_Match.GetComponent<Match> ().Count;
	}
}
