using UnityEngine;
using System.Collections;

public class HitSide : MonoBehaviour 
{
	#region PublicMemberVariables
	public bool m_Left;
	#endregion
	
	#region PrivateMemberVariables
	//private GameObject m_LastObject;
	#endregion

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.GetComponent<InventoryItem>() != null)
		{
			if(m_Left)
			{
				col.gameObject.GetComponent<InventoryItem>().HitLeftWall();
			}
			else
			{
				col.gameObject.GetComponent<InventoryItem>().HitRightWall();
			}
		}
	}
	public void OnTriggerExit(Collider col)
	{
		col.gameObject.GetComponent<InventoryItem>().StopHitWall();
	}
}
