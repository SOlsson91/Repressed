using UnityEngine;
using System.Collections;
using FMOD.Studio;
using System;

public class FlashlightSound : TriggerComponent
{
	#region PrivateMemberVariables
	private GameObject	m_Player;
	private FlashLight	m_Flashlight;
	private FMOD.Studio.EventInstance m_Event;
	private string	m_Path;
	private bool		m_PlayedOnce = false;
	#endregion
	#region PublicMemberVariables
	public FMODAsset m_Asset;
	#endregion

	void Start ()
	{
		CacheEventInstance ();
		m_Player = Camera.main.transform.parent.gameObject;
	}
	void Play()
	{
		if(Camera.main.GetComponent<HandScript>().m_Active && !Camera.main.GetComponent<HandScript>().m_MatchSelected && getPlaybackState() != PLAYBACK_STATE.PLAYING && !m_PlayedOnce)
		{
			m_PlayedOnce = true;
			StartEvent();
		}
	}
	void Update ()
	{
		if (Camera.main.GetComponent<HandScript> () != null)
		{
			Play ();
		}
		if(!Camera.main.GetComponent<HandScript>().m_Active && m_PlayedOnce)
		{
			m_PlayedOnce = false;
			StartEvent();
		}

		var attributes = UnityUtil.to3DAttributes (m_Player);
		ERRCHECK (m_Event.set3DAttributes(attributes));	
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
			FMOD.Studio.UnityUtil.LogError("Event failed: " + m_Path);
		}
	}

	public void CacheEventInstance() 
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
			FMOD.Studio.UnityUtil.LogError("No Asset/path for the Event");
		}
	}
	
	
	public FMOD.Studio.PLAYBACK_STATE getPlaybackState() 
	{
		if (m_Event == null || !m_Event.isValid())
			return FMOD.Studio.PLAYBACK_STATE.STOPPED;
		
		FMOD.Studio.PLAYBACK_STATE state = PLAYBACK_STATE.IDLE;
		
		if (ERRCHECK (m_Event.getPlaybackState(out state)) == FMOD.RESULT.OK)
			return state;
		return FMOD.Studio.PLAYBACK_STATE.STOPPED;
	}
	
	//Checks for errors 
	public FMOD.RESULT ERRCHECK(FMOD.RESULT result)
	{
		FMOD.Studio.UnityUtil.ERRCHECK(result);
		return result; 
	}
}