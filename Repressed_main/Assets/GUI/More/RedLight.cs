using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour 
{
	#region PublicMemberVariables
	public bool  m_Active;
	public float m_GreenLightTimer;
	public float m_YellowLightTimer;
	public float m_RedLightTimer;
	#endregion
	
	#region PrivateMemberVariables
	private float m_Timer = 0;
	private Light m_Light;
	private Vector3 m_StartPosition;
	private GameObject m_Player;
	#endregion
	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		m_StartPosition = m_Player.transform.position;
		m_Light = GetComponent<Light> ();
	}

	public void SetNewStartPosition()
	{
		m_StartPosition = m_Player.transform.position;
	}

	// Update is called once per frame
	void Update () 
	{	
		if(m_Active)
		{
			m_Timer += Time.deltaTime;
			if(m_Timer < m_GreenLightTimer)
			{
				m_Light.color = Color.green;
			}
			else if(m_Timer < m_YellowLightTimer + m_GreenLightTimer)
			{
				m_Light.color = Color.yellow;
			}
			else if(m_Timer < m_YellowLightTimer + m_GreenLightTimer + m_RedLightTimer)
			{
				m_Light.color = Color.red;
				if(Input.GetAxis("Vertical") != 0)
				{
					//Debug.Log("Player moved forwardways");
					m_Player.transform.position = m_StartPosition;
				}
				if(Input.GetAxis("Horizontal") != 0)
				{
					//Debug.Log("Player moved sideways");
					m_Player.transform.position = m_StartPosition;
				}
				//if(Input.GetAxis("Mouse X") != 0)
				//{
				//	//Debug.Log("Player Rotated");
				//	m_Player.transform.position = m_StartPosition;
				//}

			}
			else
			{
				m_Timer = 0;
			}
		}
	}
}
