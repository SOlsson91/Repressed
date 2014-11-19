using UnityEngine;
using System.Collections;

public class ClickNewScene : MonoBehaviour 
{
	public string m_Scene;
	void OnClick()
	{
		Application.LoadLevel(m_Scene);
	}
}
