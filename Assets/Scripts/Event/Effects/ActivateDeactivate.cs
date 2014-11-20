using UnityEngine;
using System.Collections;

/* Discription: Trigger components for inactivating the Object
 * Created by: Robert 23/04-14
 * Modified by: 
 * 
 */
//[RequireComponent(typeof(State))]
public class ActivateDeactivate :  TriggerComponent
{
	#region PrivateMemberVariables
	private bool m_IsActive = true; 
	#endregion
	
	void Start()
	{
		m_IsActive = gameObject.activeSelf;
	}
	
	public void DeActivate(GameObject obj)
	{
		m_IsActive = false;
		//if(Camera.main.GetComponent<Raycasting>().InteractingWith == obj.gameObject)
		//{
		//	Camera.main.GetComponent<Raycasting> ().Release ();
		//}
		obj.SetActive (m_IsActive);
	}

	public void Activate(GameObject obj)
	{
		m_IsActive = true;
		obj.SetActive(m_IsActive);
	}

	override public string Name
	{ get{return"ActivateDeactivate";}}
}
