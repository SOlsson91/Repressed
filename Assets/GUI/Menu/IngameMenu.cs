using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour 
{
	public string m_Scene = "Menu";
	public GameObject m_GUITextures;
	public GUITexture m_Button1;
	public GUITexture m_Button2;
	public GUITexture m_Singlepage;
	public GUITexture m_Book;
	public GUITexture m_Code;

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
		if(Input.GetKeyDown(KeyCode.Escape) && !m_MenuActive && !m_Singlepage.enabled && !m_Book.enabled && !m_Code.enabled)
		{
			StartMenu();
		}
		else if(Input.GetKeyDown(KeyCode.Escape) && m_MenuActive)
		{
			EndMenu();
		}
		if(m_MenuActive)
			OnTheGUI();
	}

	private void StartMenu()
	{
		m_MenuActive = true; 
		//Time.timeScale = 0;
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
		Camera.main.GetComponent<FirstPersonCamera> ().LockCamera();
		Screen.showCursor = true;
		//Screen.lockCursor = false;
		m_GUITextures.SetActive(true);

	}
	private void EndMenu()
	{
		m_MenuActive = false; 
		//Time.timeScale = 1;
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
		Camera.main.GetComponent<FirstPersonCamera> ().UnLockCamera();
		m_Player.GetComponent<FirstPersonController>().m_LockAndHideMouse = true;
		m_GUITextures.SetActive(false);
		Screen.showCursor = false;
	}

	private void OnTheGUI() 
	{
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
			EndMenu();
			RasmusGameSave.SaveLevel();
			Application.LoadLevel(m_Scene);
		}
		if(m_Button2.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0))
		{
			EndMenu();
		}
	}
}
