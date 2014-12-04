using UnityEngine;
using System.Collections;

public class Match : MonoBehaviour 
{
	public float m_TimeMatch;
	public int   m_Count = 3;

	private bool  m_Lit = false;

	// Use this for initialization
	void Start () 
	{
		m_Count = RasmusGameSave.m_MatchCount;
	}

	public bool Lit
	{ 
		set{m_Lit = value;}
		get{return m_Lit;}
	}
	public int GetCount()
	{ 
		m_Count = RasmusGameSave.m_MatchCount;
		return 	m_Count;
	}
	// Update is called once per frame
	void Update () 
	{
		if(m_Lit)
		{
			if(!GetComponent<Animation>().isPlaying)
			{
				GetComponent<Animation>()["MatchAnimation"].time = 0;
				GetComponent<Animation>().Play();
			}
			if(GetComponent<Animation>()["MatchAnimation"].time > GetComponent<Animation>()["MatchAnimation"].length)
			{
				GetComponent<Animation>().Stop();
				GetComponent<Animation>()["MatchAnimation"].time = 0;
				m_Lit = false;
				MatchFinished();
			}
			//m_Timer += Time.deltaTime;
			//if(m_Timer > m_TimeMatch)
			//{
			//	m_Lit = false;
			//	Debug.Log("Tändstickan har brunnit ut");
			//	MatchFinished();
			//}
		}
	}

	private void MatchFinished()
	{
		transform.parent.transform.parent.transform.parent.GetComponent<HandScript> ().PutArmDown ();
		m_Lit = false;
		//m_Count--;
		RasmusGameSave.m_MatchCount--;
		//m_Timer = 0;
	}

	public void EndMatch()
	{
		Debug.Log("Tändstickan avslutas");
		GetComponent<Animation>().Stop();
		m_Lit = false;
		RasmusGameSave.m_MatchCount--;
		//m_Count--;
		//m_Timer = 0;
	}
}
