using UnityEngine;
using System.Collections;
using System;
using FMOD.Studio;

public class WalkingSound : TriggerComponent
{
	#region PrivateMemberVariables
	private float	m_Surface;
	private float	m_PlayerSpeed;
	private GameObject	m_Player;
	private float	m_Time;
	private float	m_StartTime;
	private bool	m_FirstTime = true;
	private bool	m_PlayWalkingSound = true;
	private string	m_Material;
	private Vector3	m_LastPosition;
	private float	m_DistanceBeforeSound = 0.035f;
	private FMOD.Studio.EventInstance m_Event;
	private string	m_Path;
	private string	m_Parameter;
	#endregion

	#region PublicMemberVariables

	[Tooltip("A higher value plays the sound slower")]
	public float	m_WalkingSoundSpeed = 0.55f;
	public FMODAsset m_Asset;

	#endregion

	public bool PlayWalkingSound
	{
		get{return m_PlayWalkingSound;}
		set{m_PlayWalkingSound = value;}
	}


	public void PlaySound()
	{
		Vector3 position = this.gameObject.GetComponent<FirstPersonController> ().Position;
		float distance;

		m_PlayerSpeed = this.gameObject.rigidbody.velocity.normalized.magnitude;
		distance = Vector3.Distance (position, m_LastPosition);

		//Debug.Log ("Distance = " + distance + " -- DistanceBFSound = " + m_DistanceBeforeSound);
		if (distance > m_DistanceBeforeSound)
		{
			m_LastPosition = position;
			if(m_FirstTime)
			{
				m_FirstTime = false;
				m_Surface = 0.99f;
				m_Event.setParameterValue(m_Parameter, m_Surface);
				StartEvent();
			}
			else
			{
				switch(GetMaterial())
				{
				case "Wood":
					m_Surface = 0.05f;
					m_Event.setParameterValue(m_Parameter, m_Surface);
					break;
				case "Carpet":
					m_Surface = 0.15f;
					m_Event.setParameterValue(m_Parameter, m_Surface);
					break;
				case "Glass":
					m_Surface = 0.25f;
					m_Event.setParameterValue(m_Parameter, m_Surface);
					break;
				}
				//Debug.Log ("Time = " + m_Time);
				if(getPlaybackState() == PLAYBACK_STATE.SUSTAINING && m_Time >= m_WalkingSoundSpeed)
				{
					StartEvent();
					m_StartTime = Time.time;
				}
			}
		}
		else
		{
			m_LastPosition = position;
		}

	}

	void Start () 
	{
		CacheEventInstance ();
		m_StartTime = Time.time;
		m_Player = this.gameObject;
		m_Parameter = "Surface";
		m_LastPosition = this.gameObject.GetComponent<FirstPersonController> ().Position;
	}

	string GetMaterial()
	{
		RaycastHit hit;
		Ray ray = new Ray (m_Player.transform.position, - transform.up);
		//Debug.DrawRay (ray.origin, ray.direction * (m_Player.transform.lossyScale.y + 0.10f), Color.green);

		if(Physics.Raycast(ray, out hit, (m_Player.transform.lossyScale.y + 0.25f)))
		{
			if(hit.collider.gameObject.GetComponent<FloorMaterial>() != null)
			{
				return hit.collider.gameObject.GetComponent<FloorMaterial>().FloorType;
			}
		}
		return null;
	}

	void Update()
	{
		var attributes = UnityUtil.to3DAttributes (m_Player);
		ERRCHECK (m_Event.set3DAttributes (attributes));
	}

	void FixedUpdate () 
	{
		Vector3 speed = new Vector3 ();
		speed = new Vector3 (this.gameObject.transform.rigidbody.velocity.normalized.x, 0, this.gameObject.	transform.rigidbody.velocity.normalized.z);
		m_PlayerSpeed = speed.normalized.magnitude;


		m_Time = Time.time - m_StartTime;
		if(m_PlayerSpeed != 0)
		{
			if(PlayWalkingSound)
			{
				PlaySound ();
			}
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
