using UnityEngine;
using System.Collections;

public class ClickSure : MonoBehaviour 
{
	public bool m_OpenMenu;
	public GameObject m_Menu;
	public GameObject[] m_UnderMenu;

	void OnClick()
	{
		if(m_OpenMenu)
			OpenMenu();
		else
			CloseMenu();
	}

	private void OpenMenu()
	{
		m_Menu.SetActive(true);
		foreach(GameObject obj in m_UnderMenu)
			obj.collider.enabled = false;
	}

	private void CloseMenu()
	{
		m_Menu.SetActive(false);
		foreach(GameObject obj in m_UnderMenu)
			obj.collider.enabled = true;
	}

}
