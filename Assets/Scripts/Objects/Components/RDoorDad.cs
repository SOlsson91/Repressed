using UnityEngine;
using System.Collections;

public class RDoorDad : ObjectComponent
{
	public float m_Sensitivity;
	public float m_XBoxSensitivity = 0.1f;
	public int m_PositiveRotation;
	public int m_NegativeeRotation;

	private float m_LastRotation = 0;
	private float m_Difference 	 = 0;
	private float m_StartAngle;
	private float m_MaxMovement = 5;

	// Use this for initialization
	void Start () 
	{
		m_StartAngle = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	public void DadRotation1()
	{
		float movement = Input.GetAxis("Mouse Y") * m_Sensitivity;
		if(Input.GetAxis("xBoxVertical") > 0.5 || Input.GetAxis("xBoxVertical") < -0.5)
		{
			movement = Input.GetAxis("xBoxVertical") * m_XBoxSensitivity;
		}
		if(movement > m_MaxMovement)
		{
			movement = m_MaxMovement;
		}
		if(movement < -m_MaxMovement)
		{
			movement = -m_MaxMovement;
		}
		if(m_Difference < m_PositiveRotation && m_Difference > -m_NegativeeRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
		else if((Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("xBoxVertical") < -0.5) && m_Difference > -m_NegativeeRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
		else if((Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("xBoxVertical") > 0.5) && m_Difference < m_PositiveRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
	}
	public void DadRotation2()
	{
		float movement = Input.GetAxis("Mouse Y") * -m_Sensitivity;
		if(Input.GetAxis("xBoxVertical") > 0.5 || Input.GetAxis("xBoxVertical") < -0.5)
		{
			movement = Input.GetAxis("xBoxVertical") * -m_XBoxSensitivity;
		}
		if(movement > m_MaxMovement)
		{
			movement = m_MaxMovement;
		}
		if(movement < -m_MaxMovement)
		{
			movement = -m_MaxMovement;
		}

		if(m_Difference < m_PositiveRotation && m_Difference > -m_NegativeeRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
		else if((Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("xBoxVertical") > 0.5) && m_Difference > -m_NegativeeRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
		else if((Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("xBoxVertical") < -0.5) && m_Difference < m_PositiveRotation)
		{
			transform.Rotate(0, movement, 0);
			CheckRotation();
		}
	}

	public void CloseDoor()
	{
		m_LastRotation = 0;
        m_Difference   = 0;
		transform.Rotate(0, m_StartAngle - transform.rotation.eulerAngles.y, 0);
	}
	public void OpenDoor()
	{
		m_LastRotation = 90;
        m_Difference   = 90;
		transform.Rotate(0, m_StartAngle - transform.rotation.eulerAngles.y + 90, 0);
	}
	public void ChangeDoorAngle(float angle)
	{
		m_LastRotation = angle;
		m_Difference   = angle;
		transform.Rotate(0, m_StartAngle - transform.rotation.eulerAngles.y + angle, 0);
	}

	private void CheckRotation()
	{
		//Debug.Log (m_Difference);
		if(m_LastRotation + 0.01 > transform.localRotation.eulerAngles.y && m_LastRotation - 0.01 < transform.localRotation.eulerAngles.y)
		{
			//Skillnad i float
		}
		else if(m_LastRotation + 50 > transform.localRotation.eulerAngles.y && m_LastRotation - 50 < transform.localRotation.eulerAngles.y)
		{
			m_Difference   += transform.localRotation.eulerAngles.y - m_LastRotation;
			m_LastRotation = transform.localRotation.eulerAngles.y;
		}
		else
		{
			m_LastRotation = transform.localRotation.eulerAngles.y;
		}
	}
}
