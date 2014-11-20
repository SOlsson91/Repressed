using UnityEngine;
using System.Collections;

public class ZoneTimerTrigger : MonoBehaviour {

	#region TimerVariables
	public  float m_Timer 		= 0;
	public  float m_TimerValue;
	private GameObject player;
	private bool m_intersecting = false;
	private bool  m_IsActive 	= false;
	#endregion

	void Start()
	{
		SuperTrigger[] triggerArray;
		triggerArray = gameObject.GetComponents<SuperTrigger>();
		foreach(SuperTrigger c in triggerArray)
		{
			if(c.m_ZoneTimer){
				m_TimerValue = c.m_TimerValue;
			}
		}
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(m_IsActive)
			m_Timer -= Time.deltaTime;	

		if((m_Timer <= 0 ) && m_IsActive)
		{
			if(gameObject.GetComponent<SuperTrigger>())
			{
				SuperTrigger[] triggerArray;
				triggerArray = gameObject.GetComponents<SuperTrigger>();
				foreach(SuperTrigger c in triggerArray)
				{
					if(c.m_ZoneTimer){
						c.ActivateTrigger();
						m_Timer = m_TimerValue;
					}
				}
			}
		}

		if (gameObject.collider.bounds.Intersects (player.collider.bounds)) 
		{

			if(!m_IsActive && !m_intersecting)
			{
				m_Timer = m_TimerValue;
				m_IsActive = true;
			}	
			m_intersecting = true;

		}
		else
		{
			if(m_IsActive)
			{
				m_IsActive = false;
			}
			m_intersecting = false;
		}
	}
}
