using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Used for the InventoryGUI, this script holds reference to real objects and help Manager to display objects throu m_Int
 *Need a child TextMesh to display Stacks.
 * 
Created by: Rasmus
 */

public class InventoryItem : MonoBehaviour 
{
	#region PublicMemberVariables

	#endregion
	
	#region PrivateMemberVariables
	private int m_Int;
	private int m_Stack;

	private GameObject[] m_Reference;
	private List<GameObject> m_List;
	#endregion
	// Use this for initialization
	void Start () 
	{
		m_Int = 0;
		GetComponentInChildren<TextMesh> ().renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//GetComponentInChildren<TextMesh> ().renderer.enabled = false;
	}

	public void SetReference(GameObject obj)
	{
		if(m_List == null)
		{
			m_List = new List<GameObject> ();
		}
		m_List.Add (obj);
		m_Reference = m_List.ToArray();

		if(m_Reference.Length > 1)
		{
			GetComponentInChildren<TextMesh> ().text = m_Reference.Length.ToString();
			GetComponentInChildren<TextMesh> ().renderer.enabled = true;
		}
	}
	public void TakeAwayOneItem()
	{
		m_List.RemoveAt (m_Reference.Length - 1);
		m_Reference = m_List.ToArray ();
		GetComponentInChildren<TextMesh> ().text = m_Reference.Length.ToString();
		if(m_Reference.Length < 2)
		{
			GetComponentInChildren<TextMesh> ().renderer.enabled = false;
		}
	}
	public void TakeAwayAndDestoy()
	{
		GameObject temp = m_Reference[m_Reference.Length - 1];
		m_List.RemoveAt (m_Reference.Length - 1);
		m_Reference = m_List.ToArray ();
		GetComponentInChildren<TextMesh> ().text = m_Reference.Length.ToString();
		Destroy (temp);
		if(m_Reference.Length < 2)
		{
			GetComponentInChildren<TextMesh> ().renderer.enabled = false;
		}
	}
	public GameObject[] GetGameObject()
	{
		return m_Reference;
	}

	public int ReturnInt ()
	{
		return m_Int;
	}
	public void HitLeftWall()
	{
		m_Int = -1;
	}
	public void HitRightWall()
	{
		m_Int = 1;
	}
	public void StopHitWall()
	{
		m_Int = 0;
	}
}
