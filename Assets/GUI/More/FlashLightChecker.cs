using UnityEngine;
using System.Collections;

public class FlashLightChecker : MonoBehaviour 
{
	public GameObject m_Flashlight;
	public Light m_Light;
	public int m_FirstLimit  = 10;
	public int m_SecondLimit = 20;
	public int m_ThirdLimit  = 30;
	public int m_FourthLimit = 40;
	private bool m_Flicker = true;
	
	private float m_Timer;
	private float m_OriginalSpotAngle;
	
	// Use this for initialization
	void Start () 
	{
		m_OriginalSpotAngle = m_Light.spotAngle;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Flashlight != null)
		{
			if(m_Flashlight.activeInHierarchy == true)
			{
				m_Timer += Time.deltaTime;
				if(m_Flicker)
				{
					if(m_Timer < m_FirstLimit)
					{
						m_Flashlight.GetComponentInChildren<FlickeringLight>().m_LampOn = false;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 10;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 0;
					}
					else if(m_Timer < m_SecondLimit)
					{
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 20;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 10;
					}
					else if(m_Timer < m_ThirdLimit)
					{
						m_Flashlight.GetComponentInChildren<FlickeringLight>().m_LampOn = true;
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 10;
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 0;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 30;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 10;
					}
					else if(m_Timer < m_FourthLimit)
					{
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 30;
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 10;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 50;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 20;
					}
					else
					{
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 50;
						m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 20;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit1 = 80;
						//m_Flashlight.GetComponentInChildren<FlickeringLight>().Limit2 = 50;
					}
					if(m_Timer < 40)
					{
						m_Light.spotAngle = m_OriginalSpotAngle * ((100 - (m_Timer*2))/100);
					}
					else
					{
						m_Light.spotAngle = m_OriginalSpotAngle * 0.2f;
					}
				}
				//else
				//{
				//	m_Flashlight.GetComponentInChildren<FlickeringLight>().m_LampOn = false;
				//	if(m_Timer < 40)
				//	{
				//		m_Light.spotAngle = m_OriginalSpotAngle * ((100 - (m_Timer*2))/100);
				//	}
				//	else
				//	{
				//		m_Light.spotAngle = m_OriginalSpotAngle * 0.2f;
				//	}
				//}
			}
		}
		//Debug.Log (m_Timer);
	}
	
	public void NewBattery()
	{
		m_Timer = 0;
	}
}
