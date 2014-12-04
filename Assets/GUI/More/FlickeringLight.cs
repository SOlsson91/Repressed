using UnityEngine;
using System.Collections;

/*Class for flickering light, the variables can be accessed from editor or during runtime
 * 
Created by: Rasmus
 */

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour 
{
	#region PublicMemberVariables
	public bool  m_LampOn = false;
	public float m_Delay;
	public int   m_Limit1 = 70;
	public int   m_Limit2 = 20;
	public float m_Flux;
	#endregion
	
	#region PrivateMemberVariables
	private Light m_Light;
	private int m_Random;
	private float m_Count = 0;
	private float m_Intens;
	private float m_FluxChange = 0;
	#endregion

	// Use this for initialization
	void Start () 
	{
		m_Light = this.GetComponent<Light>();
		m_Intens = m_Light.intensity;
	}

	public float Delay
	{ 
		set{m_Delay = value;}
		get{return m_Delay;}
	}
	public int Limit1
	{ 
		set{m_Limit1 = value;}
		get{return m_Limit1;}
	}
	public int Limit2
	{ 
		set{m_Limit2 = value;}
		get{return m_Limit2;}
	}
	public bool LampOn
	{ 
		set{m_LampOn = value;}
		get{return m_LampOn;}
	}
	public float Flux
	{ 
		set{m_Flux = value;}
		get{return m_Flux;}
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_LampOn) 
		{
			if(m_Count > m_Delay + m_FluxChange)
			{
				LightFlicker();
				m_Count = 0;
				m_FluxChange = Random.Range(-m_Flux, m_Flux);
			}
			else
			{
				m_Count += Time.deltaTime;
			}
		}
	}

	public void LightFlicker()
	{
		if(m_Random > m_Limit1)
		{
			m_Light.intensity = m_Intens;
			m_Light.enabled = true;
		}
		else if (m_Random > m_Limit2 && m_Random <= m_Limit1)
		{
			m_Light.enabled = true;
			m_Light.intensity = m_Intens * 0.60f;
		}
		else
		{
			m_Light.intensity = 0;
			m_Light.enabled = true;
		}
		m_Random = Random.Range (0, 100);
	}
}
