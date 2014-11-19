using UnityEngine;
using System.Collections;

public class HoverTrigger : ObjectComponent 
{
	public void Trigger()
	{
		if(gameObject.GetComponent<SuperTrigger>())
		{
			SuperTrigger[] triggerArray;
			triggerArray = gameObject.GetComponents<SuperTrigger>();
			foreach(SuperTrigger c in triggerArray)
			{
				if(c.m_Hover)
				{
					c.ActivateTrigger();
				}
			}
		}
	}
	
	public void HoverTriggerActivate()
	{
		Trigger();
	}
}