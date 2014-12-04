using UnityEngine;
using System.Collections;

public class SafeCode : ObjectComponent 
{
	#region PublicMemberVariables
	public Texture[] m_Textures;
	public int[]     m_CodeNumbers;
	#endregion
	
	#region PrivateMemberVariables
	private GUITexture  m_GUITexture;
	private int 		m_Number    	= 0;
	private bool 		m_Active 		= false;
	private bool 		m_GoRight   	= true;
	private string    	m_Left	 		= "ClickLeft";
	private string 	 	m_Right	 		= "ClickRight";
	private bool[] 		m_RightCode;
	private int 		m_CodePosition  = 0;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		GameObject temp = GameObject.FindGameObjectWithTag ("SinglePageGUI");
		m_GUITexture = temp.GetComponent<GUITexture> ();
		m_RightCode = new bool[m_CodeNumbers.Length];
		ResetCode ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_GUITexture.enabled == true && m_Active == true)
		{
			if(Input.GetButtonDown("Fire2") || Input.GetKeyUp(KeyCode.Escape))
			{
				m_GUITexture.enabled = false;
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
				Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();
				m_Active = false;
			}
			if(Input.GetButtonDown(m_Right))
			{
				if(!m_GoRight)
				{
					ResetCode();
				}
				if(m_Number < 9)
				{
					m_Number++;
				}
				else
				{
					m_Number = 0;
				}
				SetNumberTexture();

				if(m_Number == m_CodeNumbers[m_CodePosition])
				{
					OneNumberCorrect();
				}
			}
			if(Input.GetButtonDown(m_Left))
			{
				if(m_GoRight)
				{
					ResetCode();
				}
				if(m_Number > 0)
				{
					m_Number--;
				}
				else
				{
					m_Number = 9;
				}
				SetNumberTexture();

				if(m_Number == m_CodeNumbers[m_CodePosition])
				{
					OneNumberCorrect();
				}
			}
		}
	}

	private void SetNumberTexture()
	{
		m_GUITexture.texture = m_Textures[m_Number];
	}

	private void OneNumberCorrect()
	{
		m_RightCode [m_CodePosition] = true;
		m_GoRight = !m_GoRight;
		if(m_CodePosition < m_RightCode.Length - 1)
		{
			m_CodePosition++;
		}
		else
		{
			CheckIfRight();
		}
	}

	private void ResetCode()
	{
		for(int i = 0; i < m_RightCode.Length; i++)
		{
			m_RightCode[i] = false;
		}
		m_CodePosition = 0;
		m_GoRight = true;
	}

	private void CheckIfRight()
	{
		bool tempBool = true;
		for(int i = 0; i < m_RightCode.Length; i++)
		{
			if(m_RightCode[i] == false)
			{
				tempBool = false;
				i = m_RightCode.Length;
			}
		}
		if(tempBool)
		{
			m_GUITexture.texture = m_Textures[10];
			if(GetComponent<SuperTrigger>() != null)
			{
				GetComponent<SuperTrigger>().ActivateTrigger();
			}
			Debug.Log("Cracked The Code");
		}
	}

	public override void Interact ()
	{
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement ();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera ();
		m_GUITexture.texture = m_Textures[m_Number];
		m_GUITexture.enabled = true;
		m_Active = true;
	}
}
