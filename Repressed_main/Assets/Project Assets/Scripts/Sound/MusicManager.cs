using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using FMOD.Studio;

/* Discription: Music Managers
* Managing the Music with FMOD, has full controll over the music
*
* Created by: Sebastian Olsson 15/04-14
* Modified by:
*/
public class MusicManager : MonoBehaviour
{
	#region PrivateMemberVariables
	private FMOD.Studio.EventInstance m_Event;
	private bool m_Started	= false;
	private string m_Path;
	#endregion
	#region PublicMemberVariables
	[Range(0,1)] public float m_Loggor;
	[Range(0,1)] public float m_Meny;
	[Range(0,1)] public float m_Cutscenes;
	[Range(0,1)] public float m_Tutorial;
	[Range(0,1)] public float m_Act1;

	public bool startEventOnAwake	= true;
	public FMODAsset	m_Asset;
	private string	m_LoggorParameter ="loggor";
	private string	m_MenyParameter = "meny";
	private string	m_CutscenesParameter = "Cutscenes";
	private string	m_TutorialParameter = "tutorial";
	private string	m_Act1Parameter = "Act 1";
	#endregion
	public FMOD.Studio.EventInstance GetEvent
	{
		get{ return m_Event; }
	}
	public void SetParameterValue(string p_Name, float p_Value)
	{
		m_Event.setParameterValue (p_Name, p_Value);
	}
	void Start()
	{
		CacheEventInstance();
		m_Event.setParameterValue (m_LoggorParameter, m_Loggor);
		m_Event.setParameterValue (m_MenyParameter, m_Meny);
		m_Event.setParameterValue (m_CutscenesParameter, m_Cutscenes);
		m_Event.setParameterValue (m_TutorialParameter, m_Tutorial);
		m_Event.setParameterValue (m_Act1Parameter, m_Act1);
		if (startEventOnAwake)
		{
			StartEvent();
		}
	}
	void Update()
	{
	}
	void OnDisable()
	{
		m_Event.stop (STOP_MODE.ALLOWFADEOUT);
		m_Event.release ();
	}
	void CacheEventInstance()
	{
		if (m_Asset != null)
		{
			m_Event = FMOD_StudioSystem.instance.GetEvent(m_Asset.id);
		}
		else if (!String.IsNullOrEmpty(m_Path))
		{
			m_Event = FMOD_StudioSystem.instance.GetEvent(m_Path);
		}
		else
		{
			FMOD.Studio.UnityUtil.LogError("No asset or path specified for Event Emitter");
		}
	}
	public void StartEvent()
	{	
		if (m_Event == null || !m_Event.isValid())
		{
			CacheEventInstance();
		}
		if (m_Event != null && m_Event.isValid())
		{
			ERRCHECK(m_Event.start());
		}
		else
		{
			FMOD.Studio.UnityUtil.LogError("Event retrieval failed: " + m_Path);
		}
		m_Started = true;
	}
	public FMOD.Studio.ParameterInstance getParameter(string name)
	{
		FMOD.Studio.ParameterInstance param = null;
		ERRCHECK(m_Event.getParameter(name, out param));
		return param;
	}
	FMOD.RESULT ERRCHECK(FMOD.RESULT result)
	{
		FMOD.Studio.UnityUtil.ERRCHECK(result);
		return result;
	}
}