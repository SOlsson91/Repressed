using UnityEngine;
using System.Collections;

public class FlashLightTemp : MonoBehaviour 
{
	public GameObject m_Light;
	public GameObject m_Arm;

	//public GUITexture m_FlashlightTexture;
	public GUITexture m_BatteryGUI;
	public Texture m_BatteryLevel1;
	public Texture m_BatteryLevel2;
	public Texture m_BatteryLevel3;
	public Texture m_BatteryLevel4;
	public Texture m_BatteryLevel5;

	public int m_Level1;
	public int m_Level2;
	public int m_Level3;
	public int m_Level4;
	public int m_level5;

	public float m_FlickeringLimit = 1f;

	private float m_Timer  = 0;
	private bool  m_Active = false;
	private float m_FlickeringTimer = 0;
	private float m_Intensity;

	// Use this for initialization
	void Start () 
	{
		m_Intensity = m_Light.GetComponent<Light>().intensity;
	}
	
	// Update is called once per frame
	void Update () 
	{


		if(Input.GetKeyDown("f"))
		{
			m_Active = !m_Active;
			ToggleActive();
		}

		if(m_Active)
		{
			m_FlickeringTimer += Time.deltaTime;
			if(m_FlickeringTimer > m_FlickeringLimit)
			{
				m_Light.GetComponent<FlickeringLight>().enabled = false;
				m_Light.GetComponent<Light>().intensity = m_Intensity;
			}

			m_Timer += Time.deltaTime;

			//Debug.Log(m_Timer);

			if(m_Timer < m_Level1)
			{
				//Mer kod för beteende av ljuskällan
				m_BatteryGUI.texture = m_BatteryLevel1;
			}
			else if(m_Timer < m_Level2)
			{
				m_BatteryGUI.texture = m_BatteryLevel2;
			}
			else if(m_Timer < m_Level3)
			{
				m_BatteryGUI.texture = m_BatteryLevel3;
			}
			else if(m_Timer < m_Level4)
			{
				m_BatteryGUI.texture = m_BatteryLevel4;
			}
			else
			{
				m_BatteryGUI.texture = m_BatteryLevel5;
				m_Active = false;
				ToggleActive();
			}
		}
	}

	public void ResetBattery()
	{
		m_Timer = 0;
	}

	public void StartFlickering()
	{
		m_FlickeringTimer = 0;
		if(m_Active)
			m_Light.GetComponent<FlickeringLight>().enabled = true;
	}

	private void ToggleActive()
	{
		m_Light.SetActive(m_Active);
		m_BatteryGUI.enabled = true;
		//if(m_Active)
		//{
		//	//Kod för aktivering av ficklampan
		//}
		//else
		//{
		//	//Kod för avaktivering
		//}
	}
}
