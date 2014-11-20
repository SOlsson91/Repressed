using UnityEngine;
using System.Collections;

public class Book : ObjectComponent 
{
	#region PublicMemberVariables
	public Texture[] m_Textures;
	public bool      m_ScaleGUI;
	public float     m_Width;
	public float     m_Height;
	#endregion
	
	#region PrivateMemberVariables
	private GUITexture  m_GUITexture;
	private int 		m_Page   = 0;
	private bool 		m_Active = false;
	private string    	m_Left	 ="ClickLeft";
	private string 	 	m_Right	 ="ClickRight";
	#endregion

	// Use this for initialization
	void Start () 
	{
		GameObject temp = GameObject.FindGameObjectWithTag ("BookGUI");
		m_GUITexture = temp.GetComponent<GUITexture> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_GUITexture.enabled == true && m_Active == true)
		{
			if(Input.GetButtonDown("Fire2"))
			{
				m_GUITexture.enabled = false;
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
				Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();
				Camera.main.gameObject.GetComponent<RasmusRaycast> ().m_HoldingAnObject = false;
				Camera.main.gameObject.GetComponent<RasmusRaycast> ().HoldObject = null;
				m_GUITexture.GetComponent<GUITexturePos> ().ScaleBackOriginal();

				m_Active = false;

				if(GetComponent<SuperTrigger>())
				{
					GetComponent<SuperTrigger>().ActivateTrigger();
				}
			}
			else if(Input.GetButtonDown(m_Right))
			{
				if(m_Page < m_Textures.Length - 1)
				{
					m_Page++;
					SwitchPage();
				}
				SetArrows();
			}
			else if(Input.GetButtonDown(m_Left))
			{
				if(m_Page > 0)
				{
					m_Page--;
					SwitchPage();
				}
				SetArrows();
			}
		}
	}

	private void SetArrows()
	{
		Arrow[] test = 	m_GUITexture.GetComponentsInChildren<Arrow> ();
		if(m_Page == 0)
		{
			test[0].SetLeft();
			test[1].SetLeft();
		}
		else if(m_Page == m_Textures.Length - 1)
		{
			test[0].SetRight();
			test[1].SetRight();
		}
		else
		{
			test[0].SetMiddle();
			test[1].SetMiddle();
		}
	}

	private void SwitchPage()
	{
		m_GUITexture.enabled = false;
		m_GUITexture.texture = m_Textures[m_Page];
		m_GUITexture.enabled = true;
	}
	
	public override void Interact ()
	{
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement ();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera ();
		Camera.main.gameObject.GetComponent<RasmusRaycast> ().m_HoldingAnObject = true;
		Camera.main.gameObject.GetComponent<RasmusRaycast> ().HoldObject = gameObject;
		m_GUITexture.texture = m_Textures[m_Page];
		SetArrows ();
		m_GUITexture.enabled = true;
		if(m_ScaleGUI)
		{
			m_GUITexture.GetComponent<GUITexturePos> ().ReScaleGUI (m_Width, m_Height);
		}
		m_Active = true;
	}
}
