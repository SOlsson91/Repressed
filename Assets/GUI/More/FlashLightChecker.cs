using UnityEngine;
using System.Collections;

public class FlashLightChecker : MonoBehaviour 
{
	public GameObject m_Flashlight;
	public int m_FirstLimit  = 10;
	public int m_SecondLimit = 20;
	public int m_ThirdLimit  = 30;
	public int m_FourthLimit = 40;

	private float m_Timer;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Flashlight.activeInHierarchy == true)
		{
			m_Timer += Time.deltaTime;
			if(m_Timer < m_FirstLimit)
			{
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 10;
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 0;
			}
			else if(m_Timer < m_SecondLimit)
			{
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 20;
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 10;
			}
			else if(m_Timer < m_ThirdLimit)
			{
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 30;
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 10;
			}
			else if(m_Timer < m_FourthLimit)
			{
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 50;
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 20;
			}
			else
			{
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 80;
				m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 50;
			}
		}
		//Debug.Log (m_Timer);
	}

	public void NewBattery()
	{
		m_Timer = 0;
	}
}
