using UnityEngine;
using System.Collections;

	

public class DairyPage : ObjectComponent  
{
	#region PublicMemberVariables
	public Texture m_Texture;
	public bool    m_ScaleGUI;
	public float   m_Width;
	public float   m_Height;
	#endregion
	
	#region PrivateMemberVariables
	private GUITexture m_GUITexture;
	private bool 	   m_Active = false;
	#endregion

	// Use this for initialization
	void Start () 
	{
		GameObject temp = GameObject.FindGameObjectWithTag ("SinglePageGUI");
		m_GUITexture = temp.GetComponent<GUITexture> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_GUITexture.enabled == true && Input.GetButtonDown("Fire2") && m_Active == true)
		{
			m_GUITexture.enabled = false;
			Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
			Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();
			m_GUITexture.GetComponent<GUITexturePos> ().ScaleBackOriginal();
			m_Active = false;

			if(GetComponent<SuperTrigger>())
			{
				GetComponent<SuperTrigger>().ActivateTrigger();
			}

		}
	}

	public override void Interact ()
	{
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement ();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera ();
		m_GUITexture.texture = m_Texture;
		m_GUITexture.enabled = true;
		if(m_ScaleGUI)
		{
			m_GUITexture.GetComponent<GUITexturePos> ().ReScaleGUI (m_Width, m_Height);
		}
		m_Active = true;
	}
}
