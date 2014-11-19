using UnityEngine;
using System.Collections;

/*Class for aligning the camera for RenderTexturing
 * 
Created by: Rasmus
 */

public class GUICamera : MonoBehaviour 
{
	#region PublicMemberVariables
	public GameObject m_Plane;
	#endregion
	
	#region PrivateMemberVariables
	
	#endregion
	// Use this for initialization
	void Start () 
	{
		GetComponent<Camera> ().aspect= m_Plane.collider.bounds.size.x / m_Plane.collider.bounds.size.y;
		//Debug.Log (m_Plane.collider.bounds.size.x + " " + m_Plane.collider.bounds.size.y + " " + m_Plane.collider.bounds.size.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<Camera> ().aspect = m_Plane.collider.bounds.size.x / m_Plane.collider.bounds.size.y;
		GetComponent<Camera> ().orthographicSize = m_Plane.collider.bounds.size.y / 2;
		Vector3 temp = new Vector3 ();
		temp =  m_Plane.transform.position;
		temp.z -= 10;
		GetComponent<Camera> ().transform.position = temp;
	}
}
