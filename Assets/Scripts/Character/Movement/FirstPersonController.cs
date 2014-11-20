using UnityEngine;
using System.Collections;

/* Placed on the character. Handles movement WASD and with Controller.
 * Requires a rigidbody to work!
 * 
 * Created by: 	Jimmy Nilsson 14-04-02
 * Modified by:	Jon Wahlström 2014-04-21 (Crouch, cameraposition.y is set to half the height)
 */

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour 
{
	#region PublicMemberVariables
	public  float m_MovementSpeed = 10.0f;
	public  float m_SprintSpeed	 = 15.0f;
	public  float m_ChrouchSpeed	 = 5.5f;

	public float m_JumpForce	 = 1.0f;
	public float m_Gravity		 = 5.0f;
	public float m_Lerpspeed     = 1f;
	#endregion

	#region PrivateMemberVariables
	private bool    m_Locked = false;
	private float   m_Height;
	private Vector3 m_OriginalCenter;
	private float   m_UpRay = 0.75f;
	#endregion

	public Vector3 Position
	{
		get { return rigidbody.position; }
	}

	void Awake()
	{
		//m_CameraPositionY = Camera.main.transform.position.y;
		rigidbody.useGravity = false;
		rigidbody.freezeRotation = true;
		m_Height = GetComponent<CapsuleCollider>().height;
		m_OriginalCenter = GetComponent<CapsuleCollider>().center;
	}

	// The function that handles all movement 
	void Move(float deltaTime)
	{

		if(!m_Locked)
		{
			//Vector3 cameraPosition = Camera.main.transform.position;
			//Camera.main.transform.position = new Vector3(cameraPosition.x, m_CameraPositionY, cameraPosition.z);

			Vector3 forward			= transform.forward.normalized*Input.GetAxis("Vertical");
			Vector3 right			= -transform.right.normalized*Input.GetAxis("Horizontal");
			if(Input.GetAxis("xBoxVertical") > 0.5 || Input.GetAxis("xBoxVertical") < -0.5
			   || Input.GetAxis("xBoxHorizontal") > 0.5 || Input.GetAxis("xBoxHorizontal") < -0.5)
			{
				forward			= transform.forward.normalized*Input.GetAxis("xBoxVertical");
				right			= -transform.right.normalized*Input.GetAxis("xBoxHorizontal");
			}
			Vector3 targetVelocity  = new Vector3(forward.x-right.x, 0.0f, forward.z-right.z);

			Vector3 velocity 		= rigidbody.velocity;
			float	maxVelocity		= m_MovementSpeed;


			if(Input.GetButton("Crouch"))
			{
				maxVelocity = m_ChrouchSpeed;
				Vector3 temp = m_OriginalCenter;
				temp.y += 0.5f;
				GetComponent<CapsuleCollider>().center = Vector3.Lerp(GetComponent<CapsuleCollider>().center, temp, m_Lerpspeed);
				GetComponent<CapsuleCollider>().height = Mathf.Lerp(GetComponent<CapsuleCollider>().height, m_Height/2, m_Lerpspeed);
			}	
			else
			{
				RaycastHit hit;
				Ray ray = new Ray(transform.position, transform.up);
				Debug.DrawRay(ray.origin,ray.direction * m_UpRay, Color.blue);
				if(!Physics.Raycast(ray, out hit, m_UpRay))
				{
					if(Vector3.Distance(GetComponent<CapsuleCollider>().center, m_OriginalCenter) < 0.1f)
					{
						GetComponent<CapsuleCollider>().center = m_OriginalCenter;
					}
					else
					{
						GetComponent<CapsuleCollider>().center = Vector3.Lerp(GetComponent<CapsuleCollider>().center, m_OriginalCenter, m_Lerpspeed);
					}
					GetComponent<CapsuleCollider>().height = Mathf.Lerp(GetComponent<CapsuleCollider>().height, m_Height, m_Lerpspeed);
				}
			}

			targetVelocity	*= maxVelocity;

			velocity  		 = (targetVelocity-velocity);
			velocity.x 		 = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
			velocity.y 		 = 0; 
			velocity.z 		 = Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity);

			//add movement force, x/z
			rigidbody.AddForce(velocity, ForceMode.VelocityChange);
		}
		else if(m_Locked)
		{
			rigidbody.velocity = new Vector3(0f, 0f, 0f);
		}
		//Gravity
		
		rigidbody.AddForce (-transform.up * (m_Gravity * rigidbody.mass));

	}

	//Locks player movement WASD
	public void LockPlayerMovement()
	{
		m_Locked = true;
	}

	//Unlocks player movement WASD
	public void UnLockPlayerMovement()
	{
		m_Locked = false;
	}

	public float getMovementSpeed()
	{
		return m_MovementSpeed;
	}
	public void setMovementSpeed(float newMovementSpeed)
	{
		m_MovementSpeed = newMovementSpeed;

	}


	void FixedUpdate()
	{
		//Screen.showCursor = false;
		//Screen.lockCursor = true;
		Move(Time.fixedDeltaTime);
	}
}
