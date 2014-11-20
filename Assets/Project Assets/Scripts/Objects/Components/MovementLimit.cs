using UnityEngine;
using System.Collections;

/* Discription: Objectcomponent class for limiting the movement on this object
 * Created by: Robert Datum: 08/04-14
 * Modified by:
 * 
 */

public class MovementLimit : ObjectComponent
{
	#region PublicMemberVariables
	//public float m_PositiveX;
	//public float m_NegativeX;
	//public float m_PositiveY;
	//public float m_NegativeY;
	//public float m_PositiveZ;
	//public float m_NegativeZ;
	public Vector3 m_Positive;
	public Vector3 m_Negative;
	#endregion
	
	#region PrivateMemberVariables
	//private float MaxX;
	//private float MinX;
	//private float MaxY;
	//private float MinY;
	//private float MaxZ;
	//private float MinZ;
	private Vector3 m_Max;
	private Vector3 m_Min;
	#endregion
	
	// Use this for initialization
	void Start () 
	{	
	
		m_Max = gameObject.transform.localPosition + m_Positive;
		m_Min = gameObject.transform.localPosition - m_Negative;

		Activate();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition = CheckPosition (gameObject.transform.localPosition);
	}
	
	public Vector3 CheckPosition(Vector3 TargetPosition)
	{
		float x = TargetPosition.x;
		float y = TargetPosition.y;
		float z = TargetPosition.z;

		if(x > m_Max.x){
			TargetPosition.x = m_Max.x;
		}
		else if (x < m_Min.x) {
			TargetPosition.x = m_Min.x;
		}
		if(y > m_Max.y){
			TargetPosition.y = m_Max.y;
		}
		else if (y < m_Min.y) {
			TargetPosition.y = m_Min.y;		
		}
		if(z > m_Max.z){
			TargetPosition.z = m_Max.z;	
		}
		else if (z < m_Min.z) {
			TargetPosition.z = m_Min.z;
		}
		return TargetPosition;
	}
}