using UnityEngine;
using System.Collections;

/* Discription: Class that holds data for the player, example health and alive/dead status
 * 
 * Created by: Jimmy Date: 2014-04-04
 * Modified by: Jon Wahlstöm 2014-04-14 and Rasmus 2014-05-02
 */

public class CharacterData : MonoBehaviour 
{
	#region PublicMemberVariables
	public GameObject m_DeathMenu;
	public GameObject m_Camera;
	public GameObject m_Oculus;
	public bool       m_UseUculus = false;
	#endregion

	#region PrivateMemberVariables
	private static bool 		 m_Alive  = true;
	private static CharacterData m_Instance;
	#endregion

	void Start()
	{
		m_Instance = this;
		Alive = true;
		//m_DeathMenu.GetComponent<ActivateDeathMenu>().DeactivateWindow();
		UpdateOculus();
	}

	public void SetOculus(bool oculus)
	{
		m_UseUculus = oculus;
	}

	public bool GetOculus()
	{
		return m_UseUculus;
	}



	//Set or get the player alive status
	public static bool Alive
	{
		get 
		{ 
			return m_Alive; 
		}
		set 
		{ 
			m_Alive = value;
			if(!m_Alive)
			{
				m_Instance.ShowDeathMenu();
			}
		}
	}

	public void ShowDeathMenu()
	{
		//m_DeathMenu.GetComponent<ActivateDeathMenu>().ActivateWindow();
	}

	private void UpdateOculus()
	{
		if(m_UseUculus == true)
		{
			m_Camera.SetActive(false);
			m_Oculus.SetActive(true);
		}
		else
		{
			m_Camera.SetActive(true);
			Camera.main.enabled = true;
			m_Camera.GetComponent<Camera>().enabled = true;
			m_Oculus.SetActive(false);
		}
	}
}
