using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/* Casts a ray from the Objects position in the forward direction of the object
* until it hits an object with ObjectComponent's then activates Interact on that object.
*
* Created by: Sebastian / Jimmy Date: 2014-04-04
* Modified by: Jon Wahlström 2014-04-29 "Added collaborate hover effect"
*
*/

public class RasmusRaycast : MonoBehaviour
{
	#region PublicMemberVariables
	public float m_Distance = 3;
	public bool m_HoldingAnObject = false;
	private GameObject m_Manager;
	public LayerMask m_LayerMask = (1 << 0) | (1 << 2) | (1 << 8) | (1 << 9) | (1 << 10) | (1 << 11);
	public LayerMask m_LayerMaskHover =  (1 << 0) |(1 << 8) | (1 << 9) | (1 << 10);
	public float m_OutlineWidth = 3;
	#endregion
	
	#region PrivateMemberVariables
	private string m_InputFire1   = "Fire1";
	private string m_InputDrop    = "Fire2";
	private string m_InputInspect = "Fire3";
	private string m_InputPocket  = "Pocket";
	private GameObject m_HoldObject;
	private GameObject m_HitObject;
	private GameObject m_HitCollaborateObject;
	private GameObject m_Cursor;
	private GameObject m_LastHit;
	#endregion
	
	public GameObject HoldObject
	{
		set { m_HoldObject = value; }
		get { return m_HoldObject; }
	}
	public GameObject HitObject
	{
		set { m_HitObject = value; }
		get { return m_HitObject; }
	}
	
	void Start()
	{
		m_Manager = GameObject.FindGameObjectWithTag("ItemManager");
		m_Cursor  = GameObject.FindGameObjectWithTag("CursorGUI");
	}
	
	// Update is called once per frame
	void Update ()
	{
		int tempInt = 0;
		if(m_HoldObject == null)
		{
			HooverRaycast(true, false);
			if(Input.GetButtonDown(m_InputFire1))
			{
				m_HitObject = null;
				tempInt = Cast ();
				if(tempInt == 10 || tempInt == 9)
				{
					m_HoldObject = m_HitObject;
					m_HoldingAnObject = true;
				}

				if(m_HitObject != null)
				{
					ObjectComponent[] objectArray;
					objectArray = m_HitObject.GetComponents<ObjectComponent>();
					foreach(ObjectComponent c in objectArray)
					{
						c.Interact();
					}
				}
			}
		}
		else if(m_HoldObject.activeInHierarchy == true)
		{
			if(m_HoldObject.layer == 9)
			{
				if(m_HoldObject.GetComponent<RasmusInspect>() == null)
				{
					HooverRaycast(false, false);
				}
				if(Input.GetButtonDown(m_InputDrop))
				{
					bool dropIt = false;
					if(m_HoldObject.GetComponent<PickUp>() != null)
					{
						if(m_HoldObject.GetComponent<PickUp>().GetInspecting() == false)
						{
							m_HoldObject.GetComponent<PickUp>().Drop();
							dropIt = true;
						}
					}
					if(m_HoldObject.GetComponent<RasmusInspect>() != null)
					{
						m_HoldObject.GetComponent<RasmusInspect>().DropInspect();
						dropIt = true;

					}
					if(dropIt)
					{
						m_HoldObject = null;
						m_HoldingAnObject = false;
					}
				}
				else if(Input.GetButtonDown(m_InputFire1))
				{
					//Debug.Log("Låtsas skjuta ut en ny raycast som skall köras collabarate med");
					if(m_HoldObject.GetComponent<CollaborateTrigger>() != null)
					{
						if(m_HoldObject.GetComponent<PickUp>().GetInspecting() == false)
						{
							int tempColInt = CastCollaborate();
							//if(tempColInt == rätt lager)
							if(m_HitCollaborateObject != null)
							{
								if(m_HitCollaborateObject.GetComponent<Id>() != null)
								{
									if(m_HoldObject.GetComponent<CollaborateTrigger>().GetValidIds().Contains(m_HitCollaborateObject.GetComponent<Id>().ObjectId))
									{
										//Debug.Log ("Id match");
										m_HoldObject.GetComponent<SuperTrigger>().ActivateTrigger();
									}
								}
							}
							m_HitCollaborateObject = null;
						}
					}
				}
				else if(Input.GetButtonDown(m_InputInspect))
				{
					if(m_HoldObject.GetComponent<PickUp>())
					{
						m_HoldObject.GetComponent<PickUp>().ToggleInspecting();
					}
				}
				//else if(Input.GetButtonDown(m_InputPocket))
				//{
				//	if(m_HoldObject.GetComponent<PickUp>())
				//	{
				//		//if(m_HoldObject.GetComponent<PickUp>().GetInspecting() == false && m_HoldObject.GetComponent<ItemDiscription>() != null)
				//		{
				//			m_Manager.GetComponent<Manager>().AddInventoryItem(m_HoldObject);
				//			m_Manager.GetComponent<Manager>().SetInventoryFocusOnSelect();
				//			m_HoldObject.SetActive(false);
				//			m_HoldingAnObject = false;
				//			m_HoldObject = null;
				//		}
				//	}
				//}
			}
			else if(m_HoldObject.layer == 10)
			{
				HooverRaycast(false, true);
				if(Input.GetButton(m_InputFire1))
				{
					ObjectComponent[] objectArray;
					objectArray = m_HitObject.GetComponents<ObjectComponent>();
					foreach(ObjectComponent c in objectArray)
					{
						c.Interact();
					}
				}
				else if(Input.GetButtonUp(m_InputFire1))
				{
					m_HoldObject = null;
					m_HoldingAnObject = false;
				}
			}
		}
		else
		{
			m_HoldingAnObject = false;
			m_HoldObject = null;
		}
	}

	public void HooverRaycast(bool showFirst, bool hold)
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Debug.DrawRay(ray.origin,ray.direction * m_Distance, Color.yellow);

		if(m_LastHit != null && m_LastHit.renderer != null)
			m_LastHit.renderer.material.SetFloat("_Outline", 0f);

		if(hold)
		{
			m_Cursor.GetComponent<Cursor>().SetCursor(3);
		}
		else
		{
			if(Physics.Raycast(ray, out hit, m_Distance, m_LayerMaskHover))
			{
				m_LastHit = hit.collider.gameObject;
				if(m_LastHit.renderer != null)
					m_LastHit.renderer.material.SetFloat("_Outline", 0.001f * m_OutlineWidth);
				//m_LastHit.renderer.material.SetColor("_OutlineColor", Color.blue);
				if(hit.collider.gameObject.GetComponent<HoverTrigger>())
				{
					hit.collider.gameObject.GetComponent<HoverTrigger>().HoverTriggerActivate();
				}
				//if(hit.collider.gameObject.GetComponent<HoverText>())
				//{
				//	m_Cursor.GetComponentInChildren<GUIText>().text = hit.collider.gameObject.GetComponent<HoverText>().m_Text;
				//}
				if(hit.collider.gameObject.layer != 0)
				{
					//if(hit.collider.gameObject.GetComponent<HoverTrigger>())
					//{
					//	hit.collider.gameObject.GetComponent<HoverTrigger>().ActivateTrigger();
					//}
					if(showFirst)
					{
						//Ifall det skall vara olika ikoner beroende på scripts
						if(hit.collider.gameObject.GetComponent<RasmusInspect>() == null)
						{
							m_Cursor.GetComponent<Cursor>().SetCursor(1);
						}
						else
						{
							m_Cursor.GetComponent<Cursor>().SetCursor(4);
						}
					}
					else
					{
						m_Cursor.GetComponent<Cursor>().SetCursor(2);
					}
				}
				else
				{
					m_Cursor.GetComponent<Cursor>().ResetCursor();
				}
			}
			else
			{
				m_Cursor.GetComponent<Cursor>().ResetCursor();
			}
		}


	}

	public int Cast()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Debug.DrawRay(ray.origin,ray.direction * m_Distance, Color.blue);
		if(Physics.Raycast(ray, out hit, m_Distance, m_LayerMask))
		{
			m_HitObject = hit.collider.gameObject;
			return hit.collider.gameObject.layer;
		}
		return 0;
	}

	public int CastCollaborate()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Debug.DrawRay(ray.origin,ray.direction * m_Distance, Color.red);
		if(Physics.Raycast(ray, out hit, m_Distance, m_LayerMask))
		{
			m_HitCollaborateObject = hit.collider.gameObject;
			return hit.collider.gameObject.layer;
		}
		return 0;
	}
}