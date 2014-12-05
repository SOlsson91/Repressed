using UnityEngine;
using System.Collections;

public class RasmusInspect : ObjectComponent 
{
	#region PublicMemberVariables
	public float m_Sensitivity 			  = 20.0f;
	public float m_InspectionViewDistance = 2.0f;
	public float m_LerpSpeed			  = 1f;
	#endregion
	
	#region PrivateMemberVariables
	private Vector3 	m_OriginalPosition;
	private Quaternion  m_OriginalRotation;
	private bool 		m_Active = false;
	private bool		m_IsOriginalPosition = true;
	#endregion
	
	
	void Start()
	{
		m_OriginalPosition = transform.position;
		m_OriginalRotation = transform.rotation;
		
	}
	
	void Update()
	{
		if(m_Active)
		{
			MoveToInspectDistance(true);
			
			float m_moveX = Input.GetAxis("Mouse X") * m_Sensitivity;
			float m_moveY = Input.GetAxis("Mouse Y") * m_Sensitivity;
			
			//rotates the object based on mouse input
			transform.RotateAround(collider.bounds.center,Vector3.left, m_moveY);
			transform.RotateAround(collider.bounds.center,Vector3.up, m_moveX);
		}
		else if(m_IsOriginalPosition == false)
		{
			MoveToInspectDistance(false);
		}
	}
	
	//Lerps position and rotation of the object to the inspection Mode distance and back to original position/rotation
	void MoveToInspectDistance(bool shouldInspect)
	{
		Vector3 cameraPosition 		 = Camera.main.transform.position;
		Vector3 targetPosition;
		float   cameraObjectDistance = Vector3.Distance(cameraPosition, transform.position);
		float	lerpSpeed			 = m_LerpSpeed;
		
		if(shouldInspect)
		{
			Vector3 cameraForward  = Camera.main.transform.forward.normalized;
			cameraForward *= m_InspectionViewDistance;
			targetPosition = cameraPosition+cameraForward;
			transform.position = Vector3.Lerp(transform.position, targetPosition, m_LerpSpeed/10.0f);
			m_IsOriginalPosition = false;
		}
		else
		{
			if(Vector3.Distance(transform.position, m_OriginalPosition) > 0.03f  || Quaternion.Angle(transform.rotation, m_OriginalRotation) > 5)
			{
				m_IsOriginalPosition = false;
				transform.rotation = Quaternion.Lerp(transform.rotation, m_OriginalRotation, lerpSpeed/10.0f);
				transform.position = Vector3.Lerp(transform.position, m_OriginalPosition, lerpSpeed/10.0f);
			}
			else if(Vector3.Distance(transform.position, m_OriginalPosition) < 0.03f && Quaternion.Angle(transform.rotation, m_OriginalRotation) < 5)
			{
				m_IsOriginalPosition = true;
			}
		}
	}
	
	public Vector3 OriginalPosition
	{
		set { m_OriginalPosition = value; } 
	}
	
	public override void Interact ()
	{
		m_Active = true;
		Camera.main.GetComponent<FirstPersonCamera>().LockCamera();
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
	}

	public void DropInspect()
	{
		m_Active = false;
		Camera.main.GetComponent<FirstPersonCamera>().UnLockCamera();
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
	}
}