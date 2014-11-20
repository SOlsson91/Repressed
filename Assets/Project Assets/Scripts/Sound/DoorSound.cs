using UnityEngine;
using System.Collections;
using FMOD.Studio;
using System;

public class DoorSound : TriggerComponent
{
	#region PrivateMemberVariables
	private string	m_Path;
	private FMOD.Studio.EventInstance m_Event;
	private float	m_StartRotation;
	private float	m_Rotation;
	private bool	m_Locked;
	private float	m_MouseMovement;
	private float	m_Action;
	private GameObject	m_GameObject;
	private GameObject m_ThisDoor;
	private string	m_Parameter;
	private string m_Input = "Fire1";
	private bool	m_NegativeOpen;
	#endregion

	#region PublicMemberVariables
	[Range(0,5f)]public float m_Margin = 1f;
	public FMODAsset m_Asset;
	public bool	m_Open = false;
	#endregion

	void Start()
	{
		m_Parameter = "Action";
		m_GameObject = this.gameObject;
		CacheEventInstance();
		//m_StartRotation = transform.parent.GetComponent<RDoorDad>().Diffrence; //this.GetComponentInParent<RDoorDad> ().Diffrence;
		m_Action = 1;
		m_Event.setParameterValue(m_Parameter, m_Action);
		StartEvent ();
	}
	public void PlaySound()
	{
		m_Locked = this.GetComponent<Locked> ().GetLocked ();
		m_MouseMovement = Input.GetAxis ("Mouse Y");
		//m_Rotation = transform.parent.GetComponent<RDoorDad> ().Diffrence; //.GetComponentInParent<RDoorDad> ().Diffrence;
		m_Rotation = Mathf.Abs(m_Rotation);
		if(!m_Locked)
		{
			//Debug.Log ("Gameobject = " + m_GameObject.transform.position);
			//Debug.Log ("Not Locked");
			if(m_MouseMovement != 0)
			{
		
				if(getPlaybackState() == FMOD.Studio.PLAYBACK_STATE.PLAYING)
				{
					if(!m_Open && ((m_Rotation > (m_StartRotation + m_Margin)))) /*|| (m_Rotation < -(m_StartRotation +m_Margin*/
					{
						m_Action = 0.05f;
						m_Open = true;
						m_Event.setParameterValue(m_Parameter, m_Action);
						StartEvent();
					}
					else if(m_Open &&((m_Rotation <= (m_StartRotation + m_Margin)))) /*|| (m_Rotation >= -(m_StartRotation + m_Margin)*/
					{
						m_Action = 0.15f;
						m_Open = false;
						m_Event.setParameterValue(m_Parameter, m_Action);
						StartEvent();
					}
				}
			}
			if(getPlaybackState() == FMOD.Studio.PLAYBACK_STATE.SUSTAINING)
			{
				m_Action = 1f;
				m_Event.setParameterValue(m_Parameter, m_Action);
				m_Event.stop(STOP_MODE.ALLOWFADEOUT);
			}
			if(getPlaybackState() == FMOD.Studio.PLAYBACK_STATE.STOPPED)
			{
				m_Action = 1f;
				m_Event.setParameterValue(m_Parameter, m_Action);
				StartEvent();
			}
		}
		if(m_Locked && m_MouseMovement != 0 && getPlaybackState() == PLAYBACK_STATE.SUSTAINING && m_GameObject == m_ThisDoor)
		{
		//	Debug.Log ("Locked");
			m_Action = 0.25f;
			m_Event.setParameterValue(m_Parameter, m_Action);
			this.StartEvent();
		}

	}
	void Update()
	{
		if(Camera.main.GetComponent<RasmusRaycast> ().HoldObject != null)
		{
			DoorSound doorSound = Camera.main.GetComponent<RasmusRaycast> ().HoldObject.GetComponent<DoorSound>();
			if(doorSound != null)
			{
		//		Debug.Log (doorSound.transform.position);
				m_ThisDoor = doorSound.gameObject;
				PlaySound ();
			}
		}

		var attributes = UnityUtil.to3DAttributes (m_GameObject);
		ERRCHECK (m_Event.set3DAttributes(attributes));	
	}


//-----------------FMOD ---------------
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
