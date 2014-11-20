using UnityEngine;
using System.Collections;

public class RDoor : ObjectComponent
{
	public string m_Input;
	private GameObject m_Player;

	private Vector3 m_Start;
	private bool 	m_Positive;
	private bool 	m_UseX;
	private float   m_Counter;
	private bool 	m_Active;
	private float   m_Magic;

	//private bool init = true;
	// Use this for initialization	
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () 
	{
		if(m_Active == true)
		{
			if(m_Counter > 0)
			{
				m_Counter-= Time.deltaTime;
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

	public override void Interact()
	{
		//Debug.Log ("Anropar dörr");
		if(GetComponent<Locked>().GetLocked() == false)
		{
			if(Input.GetButton(m_Input))
			{
				if(m_Active == false)
				{
					m_Magic = Mathf.DeltaAngle (transform.rotation.eulerAngles.y, m_Player.transform.rotation.eulerAngles.y);
				}
				m_Active = true;
				ImprovedDoorCheck();
				m_Counter = 0.1f;
				Camera.main.GetComponent<FirstPersonCamera>().LockCamera();
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
			}
		}
		else
		{
			if(Input.GetButton(m_Input))
			{
				m_Active = true;
				m_Counter = 0.1f;
				Camera.main.transform.parent.GetComponent<FirstPersonController> ().LockPlayerMovement();
				Camera.main.GetComponent<FirstPersonCamera>().LockCamera();
			}
			//Debug.Log("Låst dörr");
		}
	}

	public void CloseDoor()
	{
		transform.parent.GetComponent<RDoorDad> ().CloseDoor ();
	}
	public void OpenDoor()
	{
		transform.parent.GetComponent<RDoorDad> ().OpenDoor ();
	}
	public void ChangeDoorAngle(float angle)
	{
		transform.parent.GetComponent<RDoorDad> ().ChangeDoorAngle (angle);
	}

	private void ImprovedDoorCheck()
	{
		if(m_Magic > -90 && m_Magic < 90)
		{
			transform.parent.GetComponent<RDoorDad>().DadRotation1();
		}
		else
		{
			transform.parent.GetComponent<RDoorDad>().DadRotation2();
		}
	}
}
