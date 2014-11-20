using UnityEngine;
using System.Collections;

public class OnClickTrigger : ObjectComponent 
{
	public void Trigger()
	{
		if(gameObject.GetComponent<SuperTrigger>())
		{
			SuperTrigger[] triggerArray;
			triggerArray = gameObject.GetComponents<SuperTrigger>();
			foreach(SuperTrigger c in triggerArray)
			{
				if(c.m_OnClick){
					c.ActivateTrigger();
				}
			}
		}
	}

	public override void Interact ()
	{
		Trigger ();
	}
}