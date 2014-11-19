using UnityEngine;
using System.Collections;

/*Class for the camera, works for mouse or xBoxcontroller
 * 
 * Made by: Rasmus 02/04
 * 
 * Will support Oculus in the future
 * */

public class FirstPersonCamera : MonoBehaviour 
{
	#region PublicMemberVariables
	public float m_minimumY = -60F;
	public float m_maximumY = 60F;
	public bool  m_Oculus   = false;
	public bool  m_Locked   = false;
	//public bool m_XboxController = false;
	public float m_sensitivity = 5;
	public float m_XBoxsensitivity = 0.1f;
	#endregion

	#region PrivateMemberVariables
	private float 	   m_rotationY = 0F;
	private GameObject m_Player;
	#endregion

	// Use this for initialization
	void Start () 
	{
		//m_Player = transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		//Camera without Oculus
		if(m_Oculus == false)
		{
			//Locks the camera when inspection objects
			if(m_Locked == false)
			{
				//Rotation Y
				m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivity;
				m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
				transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);

				//Call parents for x rotation
				this.SendMessageUpwards("RotateCharacter", m_sensitivity);
			}

			//Locks the camera when inspection objects
			if(m_Locked == false)
			{
				//Rotation Y
				if(Input.GetAxis("xBoxMouse Y") > 0.5 || Input.GetAxis("xBoxMouse Y") < -0.5)
				{
					m_rotationY += Input.GetAxis("xBoxMouse Y") * m_XBoxsensitivity;
					m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);
					transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
				}
				
				//Call parents for x rotation
				this.SendMessageUpwards("RotateCharacterJoystick", m_XBoxsensitivity);
			}
		}
		//Camera with Oculus rift, for later use
		else
		{
			if(m_Locked == false)
			{
				this.SendMessageUpwards("RotateCharacter", m_sensitivity);
			}
		}
	}

	//Function for locking and unlocking the camera.
	public void LockCamera()
	{
		m_Locked=true;
	}

	public void UnLockCamera()	
	{
		m_Locked=false;
	}

}
