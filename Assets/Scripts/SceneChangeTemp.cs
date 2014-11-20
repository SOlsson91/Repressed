using UnityEngine;
using System.Collections;

public class SceneChangeTemp : MonoBehaviour 
{
	public string m_Scene;
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			if(m_Scene == "Menu")
			{
				Screen.showCursor = true;
				Screen.lockCursor = false;
			}
			Application.LoadLevel(m_Scene);
			gameObject.SetActive(false);
		}
	}
}
