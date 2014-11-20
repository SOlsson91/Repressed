using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {

	#region PublicMemeberVariables
	public string m_Input;
	public bool m_OnOff  = false;
	#endregion

	#region PrivateMemeberVariables
	private bool m_CanUse = false;
	private bool m_Toggle = false;
	#endregion

	public bool ToggleLight
	{
		get{return m_Toggle;}
		set{m_Toggle = value;}
	}

	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponentInChildren<Light>().enabled = m_OnOff;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(transform.parent.gameObject.activeInHierarchy != gameObject.GetComponentInChildren<Light>().enabled)
		{
			gameObject.GetComponentInChildren<Light>().enabled = transform.parent.gameObject.activeInHierarchy;
		}
		if(m_CanUse && Input.GetButtonDown(m_Input) && !m_Toggle)
		{
			m_Toggle = true;
			Toggle();
		}
	}

	void Toggle()
	{
		m_OnOff = !m_OnOff;
		gameObject.GetComponentInChildren<Light>().enabled = m_OnOff;
	}

	public void Find()
	{
		if(!m_CanUse)
		{
			m_CanUse = true;
		}
	}
}
