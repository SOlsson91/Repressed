using UnityEngine;
using System.Collections;
/* Discription: Music Trigger
* Used for changing values when entering a trigger
*
* Created by: Sebastian Olsson 06-05-14
* Modified by: Sebastian: 21-05-14: Removed bad code
*/

[RequireComponent(typeof(BoxCollider))]
public class MusicTrigger : MonoBehaviour
{
	private bool	m_Entered = false;
	private MusicManager m_MusicManager;
	private FMOD.Studio.EventInstance m_Event;
	private string	m_PlayerName;
	[Range(0,1)]public float	m_Value;
	public string	m_Parameter;
	void Start ()
	{
		collider.isTrigger = true;
		m_PlayerName = Camera.main.transform.parent.gameObject.name;
		m_MusicManager = GameObject.FindObjectOfType<MusicManager>() as MusicManager;
		m_Event = m_MusicManager.GetEvent;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == m_PlayerName)
		{
			if(!m_Entered)
			{
				ChangeValue(m_Parameter, m_Value);
				m_Entered = true;
			}
		}
		m_Entered = false;
	}
	void ChangeValue(string p_Parameter, float p_Value)
	{
		m_MusicManager.SetParameterValue(p_Parameter, p_Value);
	}
}