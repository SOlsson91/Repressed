using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SuperTrigger : ObjectComponent 
{
	#region SuperTriggerVariables
	private int			 m_Counter        = 0;
	#endregion

	#region TriggerTypes
	public bool 	 m_CollaborateSelf;
	public List<int> m_IDsCollaborate;
	public string 	 m_CollaborateInput = "Fire1";

	public bool 	 m_CollaborateGet;

	public bool		 m_Collision;

	public bool      m_OnClick;

	public bool      m_Hover;

	public bool		 m_ZoneTimer;
	public float 	 m_TimerValue;
	#endregion

	#region EditorVariables
	public string			  m_Description	  = "";
	public List<TriggerEvent> m_TriggerEvents = new List<TriggerEvent>();
	#endregion
	
	void Start()
	{
		if (m_CollaborateSelf) {
			gameObject.AddComponent<CollaborateTrigger>();
		}
		if (m_Collision) {
			gameObject.AddComponent<CollisionTrigger>();
		}
		if(m_ZoneTimer){
			gameObject.AddComponent<ZoneTimerTrigger>();
		}
		if(m_OnClick){
			gameObject.AddComponent<OnClickTrigger>();
		}
		if(m_Hover){
			gameObject.AddComponent<HoverTrigger>();
		}
	}

	public void ActivateTrigger()
	{
		m_Counter++;
		List<EventSystem> es = Resources.FindObjectsOfTypeAll<EventSystem>().ToList();
		for(int i = 0; i < m_TriggerEvents.Count();i++)
		{
			for(int j = 0; j < es.Count(); j++)
			{
				if(m_TriggerEvents[i].m_Event == es[j].m_ID)
				{
					if(m_TriggerEvents[i].m_TriggerAt)
					{
						if(m_TriggerEvents[i].m_CounterValue <= m_Counter)
						{
							if(m_TriggerEvents[i].m_TriggerOnce)
							{
								if(!m_TriggerEvents[i].m_HasTriggered){
									es[j].ActivateEvents();
									m_TriggerEvents[i].m_HasTriggered = true;
								}
							}
							else if(m_TriggerEvents[i].m_TriggerTimes)
							{
								if(m_Counter < m_TriggerEvents[i].m_CounterValue + m_TriggerEvents[i].m_TimesToTrigger)
								{
									es[j].ActivateEvents();
								}
							}
							else
							{
								es[j].ActivateEvents();
							}
						}
					}
					else if(m_TriggerEvents[i].m_TriggerAfterEvents)
					{
						if(CheckList(m_TriggerEvents[i].m_EventIDs,es,m_TriggerEvents[i].m_EventsTriggered))
						{
							if(!m_TriggerEvents[i].m_HasTriggered)
							{
								es[j].ActivateEvents();
								m_TriggerEvents[i].m_HasTriggered = true;
							}
						}
					}
					else
					{
						es[j].ActivateEvents();
					}
				}			
			}
		}
	}

	public bool CheckList(List<int> li, List<EventSystem> es, List<bool> lb)
	{
		for(int i = 0; i < li.Count(); i++)
		{
			for(int j = 0; j < es.Count(); j++)
			{
				if(li[i] == es[j].m_ID)
				{
					if(es[j].m_HasBeenActivated)
					{
						lb[i] = true;
					}
				}
			}
		}
		for(int i = 0; i < lb.Count(); i++)
		{
			if(!lb[i]){
				return false;
			}
		}
		return true;
	}
}

[System.Serializable]
public class TriggerEvent
{
	#region EventList
	public int    	  m_Event;
	public int	   	  m_CounterValue;
	public int     	  m_TimesToTrigger;
	public bool    	  m_TriggerOnce			= false;
	public bool    	  m_TriggerAt 			= false;
	public bool    	  m_TriggerTimes 		= false;
	public bool    	  m_FoldoutEvent 		= false;
	public bool    	  m_TriggerAfterEvents 	= false;
	public bool    	  m_HasTriggered    	= false;
	public bool       m_FoldOutAfterEvents  = false;
	public List<bool> m_EventsTriggered 	= new List<bool> ();
	public List<int>  m_EventIDs 			= new List<int> ();
	#endregion
}
