using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour 
{
	public string m_Scene = "Menu";
	public GameObject m_GUITextures;
	public GUITexture m_Button1;
	public GUITexture m_Button2;

	private GameObject m_Player;
	private bool m_MenuActive = false;

	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(m_MenuActive)
				EndMenu();
			else
				StartMenu();
		}
		if (m_MenuActive) 
		{
			AnotherOnGUI();
			//m_Player.GetComponent<FirstPersonController>().m_LockAndHideMouse = false;
		}
		else
		{
			//m_Player.GetComponent<FirstPersonController>().m_LockAndHideMouse = true;
		}

	}

	private void StartMenu()
	{
		m_MenuActive = true; 
		//Time.timeScale = 0;
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera();
		m_GUITextures.SetActive(true);
		Screen.showCursor = true;
		Screen.lockCursor = false;

	}
	private void EndMenu()
	{
		m_MenuActive = false; 
		//Time.timeScale = 1;
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
		Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();
		//m_Player.GetComponent<FirstPersonController>().m_LockAndHideMouse = true;
		m_GUITextures.SetActive(false);
	}

	private void AnotherOnGUI() 
	{
		Screen.showCursor = true;
		Screen.lockCursor = false;

		if(m_Button1.HitTest(Input.mousePosition))
			m_Button1.GetComponent<ChangeTextureHover>().ChangeToHoverTexture();
		else
			m_Button1.GetComponent<ChangeTextureHover>().ChangeBack();
		if(m_Button2.HitTest(Input.mousePosition))
			m_Button2.GetComponent<ChangeTextureHover>().ChangeToHoverTexture();
		else
			m_Button2.GetComponent<ChangeTextureHover>().ChangeBack();

		if(m_Button1.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0))
		{
			//Main menu
			EndMenu();
			Screen.showCursor = true;
			Screen.lockCursor = false;
			RasmusGameSave.SaveLevel();
			Application.LoadLevel(m_Scene);
		}
		if(m_Button2.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0))
		{
			//Cont
			Screen.showCursor = false;
			Screen.lockCursor = true;
			EndMenu();
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}

	}
}
