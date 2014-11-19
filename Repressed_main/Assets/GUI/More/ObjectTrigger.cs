using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SuperTrigger))]
[RequireComponent(typeof(Id))]
[RequireComponent(typeof(Locked))]
public class ObjectTrigger : MonoBehaviour 
{
	#region PublicMemberVariables
	public GameObject[] m_Objects;
	#endregion
	
	#region PrivateMemberVariables
	private SuperTrigger m_SuperTrigger;
	private Locked m_Locked;
	#endregion

	// Use this for initialization
	void Start () 
	{
		m_SuperTrigger = GetComponent<SuperTrigger> ();
		m_Locked = GetComponent<Locked> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckActive ();
		if(m_Locked.GetLocked())
		{
			ResetObjects();
			m_Locked.UnLock();
		}
	}

	private void CheckActive()
	{
		bool TempBool = false;
		for(int i = 0; i < m_Objects.Length; i++)
		{
			if(m_Objects[i].activeInHierarchy)
			{
				TempBool = true;
			}
		}
		if(TempBool)
		{
			CheckRightOrder();
		}
	}

	private void CheckRightOrder()
	{
		bool TempRight = false;
		int actives    = 0;
		if(m_Objects[0].activeInHierarchy)
		{
			actives++;
		}
		for(int i = 1; i < m_Objects.Length; i++)
		{
			if(m_Objects[i].activeInHierarchy)
			{
				actives++;
			}

			if(m_Objects[i].activeInHierarchy && !m_Objects[i-1].activeInHierarchy)
			{
				ResetObjects();
				TempRight = false;
				i = m_Objects.Length;
			}
			else
			{
				TempRight = true;
			}
		}
		if(TempRight && actives == m_Objects.Length)
		{
			m_SuperTrigger.ActivateTrigger();
		}
	}

	private void ResetObjects()
	{
		for(int i = 0; i < m_Objects.Length; i++)
		{
			m_Objects[i].SetActive(false);
		}
	}
	//private void 
}
