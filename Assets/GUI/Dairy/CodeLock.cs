using UnityEngine;
using System.Collections;

public class CodeLock : ObjectComponent 
{
	#region PublicMemberVariables
	public int m_FirstNumber  = 0;
	public int m_SecondNumber = 0;
	public int m_ThirdNumber  = 0;
	public int m_FourthNumber = 0;
	
	public Texture  m_Locked;
	public Texture  m_Open;
	#endregion
	
	#region PrivateMemberVariables
	private int[] 	   m_TestNumber;
	private int[] 	   m_RightNumber;
	private GameObject m_GUI;
	private bool 	   m_Active = false;
	private int 	   m_SelectedNumber = 0;
	private string 	   m_Left  = "ClickLeft";
	private string 	   m_Right = "ClickRight";
	private string 	   m_Up    = "ClickUp";
	private string 	   m_Down  = "ClickDown";
	#endregion


	// Use this for initialization
	void Start () 
	{
		m_GUI = GameObject.FindGameObjectWithTag ("CodeLockGUI");
		m_RightNumber = new int[4];
		m_RightNumber [0] = m_FirstNumber;
		m_RightNumber [1] = m_SecondNumber;
		m_RightNumber [2] = m_ThirdNumber;
		m_RightNumber [3] = m_FourthNumber;
		m_TestNumber = new int[4];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Active)
		{
			if(Input.GetButtonDown(m_Up))
			{
				ChangeNumber(m_SelectedNumber, true);
			}
			if(Input.GetButtonDown(m_Down))
			{
				ChangeNumber(m_SelectedNumber, false);
			}
			if(Input.GetButtonDown(m_Left))
			{
				if(m_SelectedNumber > 0)
				{
					m_SelectedNumber--;
					m_GUI.GetComponentInChildren<SelectCode> ().SetPosition (m_SelectedNumber);
				}
			}
			if(Input.GetButtonDown(m_Right))
			{
				if(m_SelectedNumber < 3)
				{
					m_SelectedNumber++;
					m_GUI.GetComponentInChildren<SelectCode> ().SetPosition (m_SelectedNumber);
				}
			}

			if(m_GUI.GetComponent<GUITexture>().enabled == true && (Input.GetButtonDown("Fire2") || Input.GetKeyUp(KeyCode.Escape)) && m_Active == true)
			{
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
				Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();

				m_GUI.GetComponent<GUITexture>().enabled = false;
				m_Active = false;
			}
		}
	}

	private void UpdateGUINumbers()
	{
		CodeNumber[] tempNum = m_GUI.GetComponentsInChildren<CodeNumber> ();
		for(int i =0; i < tempNum.Length; i++)
		{
			tempNum[i].ChangeNumber(i, m_TestNumber[i]);
		}
		m_GUI.GetComponentInChildren<SelectCode> ().SetPosition (m_SelectedNumber);
		CheckIfRight ();
	}

	public void ChangeNumber(int number, bool positiv)
	{
		if(positiv)
		{
			if(m_TestNumber [number] < 9)
			{
				m_TestNumber [number]++;
			}
			else
			{
				m_TestNumber [number] = 0;
			}
		}
		else
		{
			if(m_TestNumber [number] > 0)
			{
				m_TestNumber [number]--;
			}
			else
			{
				m_TestNumber [number] = 9;
			}
		}

		UpdateGUINumbers ();
	}

	private void CheckIfRight()
	{
		bool TempBool = false;
		for(int i = 0; i < m_RightNumber.Length; i++)
		{
			if(m_TestNumber[i] != m_RightNumber[i])
			{
				TempBool = false;
				i = m_RightNumber.Length;
			}
			else
			{
				TempBool = true;
			}
		}
		if(TempBool)
		{
			if(GetComponent<SuperTrigger>() != null)
			{
				GetComponent<SuperTrigger>().ActivateTrigger();
			}
			m_GUI.GetComponent<GUITexture> ().texture = m_Open;
			Debug.Log("Cracked the code");
		}
		else
		{
			m_GUI.GetComponent<GUITexture> ().texture = m_Locked;
		}
	}

	public override void Interact ()
	{
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement ();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera ();

		m_GUI.GetComponent<GUITexture> ().texture = m_Locked;
		m_GUI.GetComponent<GUITexture> ().enabled = true;
		m_Active = true;
		UpdateGUINumbers ();
	}
}
