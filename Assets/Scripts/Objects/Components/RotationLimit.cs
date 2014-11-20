using UnityEngine;
using System.Collections;

/* Discription: Objectcomponent class for limiting the movement on this object
 * Created by: Robert Datum: 08/04-14
 * Modified by:
 * 
 */

public class RotationLimit : ObjectComponent
{
	#region PublicMemberVariables
	public Vector3 m_Positive;
	public Vector3 m_Negative;
	public float m_LockedLimit = 1.5f;
	public Vector3 m_Rotation;
	#endregion
	
	#region PrivateMemberVariables
	private bool 	m_IsLocked;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		m_Negative *= -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.GetComponent<Locked>())
		{
			m_IsLocked = gameObject.GetComponent<Locked> ().GetLocked ();
		}
	}

	public float CheckRotation(float angle, string axis)
	{

		axis.ToLower ();
		if (axis.Equals ("x")) 
		{
			float Check = m_Rotation.x;
			Check += angle;
			if(m_IsLocked){
				if(Check > m_LockedLimit)
				{
					angle = m_LockedLimit - m_Rotation.x;
				}
				else if(Check < (m_LockedLimit * -1))
				{
					angle = (m_LockedLimit * -1) + (m_Rotation.x * -1); 
				}
			}
			else
			{
				if(Check > m_Positive.x)
				{
					angle = m_Positive.x - m_Rotation.x; 
				}
				else if(Check < m_Negative.x)
				{
					angle = m_Negative.x + (m_Rotation.x * -1); 
				}
			}
			
			m_Rotation.x +=angle;
		}
		else if (axis.Equals ("y")) 
		{
			float Check = m_Rotation.y;
			Check += angle;
			if(m_IsLocked)
			{
				if(Check > m_LockedLimit)
				{
					angle = m_LockedLimit - m_Rotation.y;
				}
				else if(Check < (m_LockedLimit * -1))
				{
					angle = (m_LockedLimit * -1) + (m_Rotation.y * -1); 
				}
			}
			else
			{
				if(Check > m_Positive.y)
				{
					angle = m_Positive.y - m_Rotation.y; 
				}
				else if(Check < m_Negative.y)
				{
					angle = m_Negative.y + (m_Rotation.y * -1); 
				}
			}
			m_Rotation.y +=angle;
		}
		else if (axis.Equals ("z")) 
		{
			float Check = m_Rotation.z;
			Check += angle;
			if(m_IsLocked){
				if(Check > m_LockedLimit)
				{
					angle = m_LockedLimit - m_Rotation.z;
				}
				else if(Check < (m_LockedLimit * -1))
				{
					angle = (m_LockedLimit * -1) + (m_Rotation.z * -1); 
				}
			}
			else
			{
				if(Check > m_Positive.z)
				{
					angle = m_Positive.z - m_Rotation.z; 
				}
				else if(Check < m_Negative.z)
				{
					angle = m_Negative.z + (m_Rotation.z * -1); 
				}
			}
			m_Rotation.z +=angle;
		}
		return angle;
	}
}