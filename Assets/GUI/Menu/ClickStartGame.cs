using UnityEngine;
using System.Collections;

public class ClickStartGame : MonoBehaviour 
{
	public string m_Game;
	private FirstPersonController m_Camera;

	void Start()
	{

	}
	void OnClick()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
		RasmusGameSave.ClearSaves();
		Application.LoadLevel(m_Game);
	}
}
