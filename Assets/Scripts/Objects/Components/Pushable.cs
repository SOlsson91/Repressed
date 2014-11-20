using UnityEngine;
using System.Collections;

/* Discription: Push Component
 * Used to push/pull drawers, open them Amnesia style
 * 
 * Created by: Sebastian Olsson 2014-04-08
 * Modified by: Robert Siik 2014-04-17 : Added compability with MovementLimit
 */

public class Pushable : ObjectComponent 
{
	#region PrivateMemberVariables
	private float 		m_MouseXPosition;
	private float 		m_MouseYPosition;
	private Vector3 	m_Delta				= Vector3.zero;
	private bool		m_UnlockedCamera	= true;
	private Transform   m_Camera;
	private GameObject 	m_Player;
	private bool 		m_Active;
	private float		m_Counter;
	#endregion
	
	#region PublicMemberVariables
	public string 	m_HorizontalInput = "Mouse X";
	public string 	m_VerticalInput   = "Mouse Y";
	public string 	m_Input;
	public float	m_MoveSpeed		  = 1f;
	public bool     m_Invert;
	#endregion
	
	public Vector3 Delta
	{
		get {return m_Delta;}
	}

	void Start () 
	{
		m_Camera  = Camera.main.transform;
		m_Player  = m_Camera.transform.parent.gameObject; 
	}
	
	void Update () 
	{
		if(m_Active == true)
		{
			if(m_Counter > 0)
			{
				m_Counter-= Time.deltaTime;
				m_MouseXPosition = Input.GetAxis(m_HorizontalInput);
				m_MouseYPosition = Input.GetAxis(m_VerticalInput);
				if(m_Invert)
				{
					m_MouseXPosition *= -1;
					m_MouseYPosition *= -1;
				}
				
				PlayerForward();
				Vector3 targetPosition = transform.localPosition + m_Delta * m_MoveSpeed;
				if(gameObject.GetComponent<MovementLimit>())
				{
					targetPosition = gameObject.GetComponent<MovementLimit>().CheckPosition(targetPosition);
				}
				transform.localPosition = targetPosition;
			}
			else
			{
				m_Active = false;
				//Camera.main.GetComponent<Raycasting>()
				Camera.main.GetComponent<FirstPersonCamera>().UnLockCamera();
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
			}
		}
	}

	//By checking the players forward we change the axis of the mouse position
	public override void Interact()
	{
		m_Active = true;
		m_Counter = 0.1f;
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
		Camera.main.GetComponent<FirstPersonCamera>().LockCamera();
	}

	//Calculates the general direction of a vector v
	Vector3 ClosestDirection(Vector3 v) 
	{
		Vector3[] compass = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
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

	private void PlayerForward()
	{
		Vector3 playerGeneralForward = ClosestDirection(m_Player.transform.forward);
		Vector3 objectGeneralForward = ClosestDirection(transform.forward);
		Vector3 objectGeneralRight	 = ClosestDirection(transform.right);

		if(playerGeneralForward == objectGeneralForward)
		{
			m_Delta = new Vector3(m_MouseXPosition, 0, m_MouseYPosition)*Time.deltaTime;
		}
		else if(playerGeneralForward == -objectGeneralForward)
		{
			m_Delta = new Vector3(m_MouseXPosition, 0, -m_MouseYPosition)*Time.deltaTime;
		}
		else if(playerGeneralForward == objectGeneralRight)
		{
			m_Delta = new Vector3(m_MouseYPosition, 0, -m_MouseXPosition)*Time.deltaTime;
		}
		else if(playerGeneralForward == -objectGeneralRight)
		{
			m_Delta = new Vector3(m_MouseYPosition, 0, m_MouseXPosition)*Time.deltaTime;
		}
	}
}