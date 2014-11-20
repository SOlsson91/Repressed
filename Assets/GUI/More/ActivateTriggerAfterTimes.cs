using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SuperTrigger))]
public class ActivateTriggerAfterTimes : MonoBehaviour 
{
	public int m_TimesToActivate;

	private int m_TimesCalled = 0;
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			m_TimesCalled++;
			if(m_TimesCalled >= m_TimesToActivate)
			{
				ActivateSuperTrigger();
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void ActivateSuperTrigger()
	{
		GetComponent<SuperTrigger>().ActivateTrigger();
	}
}
