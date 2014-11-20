using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Discription: Push Component
 * Used to push/pull doors, open Amnesia style
 * 
 * Created by: Sebastian Olsson 2014-04-08
 * Modified by: Jimmy 2014-04-30
 */

public class DoorDrag : ObjectComponent
{
	#region PrivateMemberVariables
	private float				m_DeActivateCounter		= 5;
	private float 				m_MouseYPosition;
	private GameObject			m_Player;
	private float 				m_Delta;
	private Vector3 			m_RotationAxis;
	private Vector3 			m_ObjectGeneralForward;
	private bool   				m_IsDraging;
	#endregion

	#region PublicMemberVariables
	public float		m_Speed				= 60.0f;
	public string 		m_VerticalInput;
	public string 		m_Input;
	#endregion

	void Start () 
	{
		m_Player = Camera.main.transform.parent.gameObject;
	}

	void Update () 
	{	
		if(m_IsDraging)
		{
			m_ObjectGeneralForward = ClosestDirection(transform.forward);
			m_RotationAxis = PlayerForward();
			m_MouseYPosition = Input.GetAxis("Mouse Y");

			if(m_MouseYPosition != 0)
			{
				if(gameObject.GetComponent<RotationLimit>())
				{
					m_Delta = gameObject.GetComponent<RotationLimit>().CheckRotation(m_Delta, "y");
				}

				transform.Rotate(m_RotationAxis,m_Delta,Space.Self);				
			}
		}
	}

	public override void Interact ()
	{
		if(!m_IsDraging)
		{
			StartDrag();
		}
	}

	public void StartDrag()
	{
		m_IsDraging = true;
		Camera.main.transform.gameObject.GetComponent<FirstPersonCamera>().LockCamera();
		Camera.main.gameObject.GetComponent<Raycasting> ().m_HoldingObject = true;
		Camera.main.gameObject.GetComponent<Raycasting> ().HoldObject = gameObject;
	}

	public void StopDrag()
	{
		m_IsDraging = false;
		Camera.main.transform.gameObject.GetComponent<FirstPersonCamera>().UnLockCamera();
		Camera.main.gameObject.GetComponent<Raycasting> ().m_HoldingObject = false;
		Camera.main.gameObject.GetComponent<Raycasting> ().HoldObject = null;
	}

	//Calculates the general direction of a vector v
	Vector3 ClosestDirection(Vector3 v) 
	{
		Vector3[] compass = { new Vector3(-1,0,1), Vector3.forward, new Vector3(1,0,1), new Vector3(-1,0,-1), Vector3.back, new Vector3(1,0,-1), Vector3.left, Vector3.right};
		float maxDot = -Mathf.Infinity;
		Vector3 ret = Vector3.zero;

		foreach(Vector3 dir in compass) 
		{
			float t = Vector3.Dot(v, dir);
			if (t > maxDot) 
			{
				ret = dir;
				maxDot = t;
			}
		}
		return ret;
	}

	//Changes m_Delta according to the direction the player is facing
	private Vector3 PlayerForward()
	{
		Vector3 forward = ClosestDirection(m_Player.transform.forward);

		if(forward == m_ObjectGeneralForward)
		{
			m_Delta = ((-m_MouseYPosition)*m_Speed)*Time.deltaTime;
		}
		else if(forward == -m_ObjectGeneralForward)
		{
			m_Delta = ((m_MouseYPosition)*m_Speed)*Time.deltaTime;
		}

		return new Vector3(0,1,0);
	}
	public override string Name
	{
		get
		{
			return "DoorDrag";
		}
	}
}