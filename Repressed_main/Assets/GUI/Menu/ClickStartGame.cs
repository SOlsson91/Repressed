using UnityEngine;
using System.Collections;

public class ClickStartGame : MonoBehaviour 
{
	public string m_Game;

	void OnClick()
	{
		RasmusGameSave.ClearSaves();
		Application.LoadLevel(m_Game);
	}
}
