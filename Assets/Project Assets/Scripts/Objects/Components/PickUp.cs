using UnityEngine;
using System.Collections;


/* Discription: Class for picking up an object and holding in front of you
 * 
 * Created By: Rasmus 04/04
 * Modified by: Sebastian 23-04-2014 : Changed so pickup put the object in the bottom right corner 
 */
//TODO: Fix Interact

[RequireComponent(typeof(Rigidbody))]
public class PickUp : ObjectComponent 
{
	#region PublicMemberVariables
	[Range(0, 2)]public float m_ChangeSize	= 0.80f;
	public float 		m_ScaleTime			= 30f;
	public float 		m_Sensitivity 		= 20.0f;
	public float m_InspectionViewDistance   = 2.0f;
	public float m_LerpSpeed			    = 1f;
	#endregion

	#region PrivateMemberVariables
	private float 		m_DropPointMax = 2.0f;	//Här kan du ändra martin.. 
	private float		m_DropDistance = 2.0f; 
	private Transform   m_CameraTransform;
	private int			m_DeActivateCounter;
	private bool 		m_HoldingObject	= false;
	private Vector3		m_OriginalScale;
	private Transform	m_HoldObject;
	private bool 		m_IsInspecting = false;
	private bool 		m_IsOriginalPosition = true;
	#endregion

	void Start () 
	{
		m_CameraTransform	= Camera.main.transform;
		m_OriginalScale		= transform.lossyScale;
		m_HoldObject		= m_CameraTransform.FindChild("ObjectHoldPosition");
		m_HoldingObject 	= false;
	}

	public void Test()
	{
		m_CameraTransform	= Camera.main.transform;
		m_OriginalScale		= transform.lossyScale;
		m_HoldObject		= m_CameraTransform.FindChild("ObjectHoldPosition");
		m_HoldingObject 	= false;
	}

	public Vector3 OriginalScale
	{
		get{return m_OriginalScale; }
	}

	public bool HoldingObject
	{
		set{m_HoldingObject = value;}
		get{ return m_HoldingObject;}
	}

	public bool GetInspecting()
	{
		return m_IsInspecting;
	}

	void Update () 
	{
		if(m_HoldingObject)
		{
			if(m_IsInspecting)
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
			else
			{
				transform.position = m_HoldObject.transform.position;
				transform.rotation = m_HoldObject.transform.rotation;
			}
		}
	}

	void MoveToInspectDistance(bool shouldInspect)
	{
		Vector3 cameraPosition 		 = Camera.main.transform.position;
		Vector3 targetPosition;
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
			//Debug.Log(Vector3.Distance(transform.position, m_HoldObject.transform.position) + " " + Quaternion.Angle(transform.rotation, m_HoldObject.transform.rotation));
			if(Vector3.Distance(transform.position, m_HoldObject.transform.position) > 0.1f  || Quaternion.Angle(transform.rotation, m_HoldObject.transform.rotation) > 10)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, m_HoldObject.transform.rotation, lerpSpeed/10.0f);
				transform.position = Vector3.Lerp(transform.position, m_HoldObject.transform.position, lerpSpeed/10.0f);
			}
			else if(Vector3.Distance(transform.position, m_HoldObject.transform.position) < 0.1f && Quaternion.Angle(transform.rotation, m_HoldObject.transform.rotation) < 10)
			{
				m_IsOriginalPosition = true;
			}
		}
	}

	public void ToggleInspecting()
	{
		m_IsInspecting = !m_IsInspecting;
		ToggleLock();
	}

	private void ToggleLock()
	{
		if(m_IsInspecting)
		{
			Camera.main.GetComponent<FirstPersonCamera>().LockCamera();
			Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
		}
		else
		{
			Camera.main.GetComponent<FirstPersonCamera>().UnLockCamera();
			Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();
		}
	}

	public override void Interact ()
	{
		Pick ();
	}

	public void Pick()
	{	
		transform.position = m_HoldObject.transform.position;
		transform.rotation = m_HoldObject.transform.rotation;

		rigidbody.velocity   		= Vector3.zero;
		rigidbody.angularVelocity 	= Vector3.zero;
		rigidbody.useGravity 		= false;

		collider.enabled = false;

		m_HoldingObject = true;
	}

	public void Drop()
	{
		Cast();
		transform.position 	 = m_CameraTransform.position + (m_CameraTransform.forward * m_DropDistance);

		rigidbody.velocity   		= Vector3.zero;
		rigidbody.angularVelocity 	= Vector3.zero;
		m_HoldingObject 			= false;
		m_IsInspecting 				= false;
		rigidbody.useGravity 		= true;
		Camera.main.GetComponent<FirstPersonCamera>().UnLockCamera();
		Camera.main.transform.parent.GetComponent<FirstPersonController> ().UnLockPlayerMovement();

		collider.enabled = true;

		m_HoldingObject = false;
	}

	void Cast()
	{
		RaycastHit hit;
		Ray ray = new Ray(m_CameraTransform.transform.position, m_CameraTransform.transform.forward);
		Debug.DrawRay (ray.origin, ray.direction * m_DropPointMax, Color.magenta);

		if (Physics.Raycast (ray, out hit, m_DropPointMax))
		{
			m_DropDistance = Vector3.Distance(hit.point, m_CameraTransform.position);
			m_DropDistance *= 0.88f;
		}
		else
		{
			m_DropDistance = m_DropPointMax;
		}
	}
}