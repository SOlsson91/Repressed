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

public class Raycasting : MonoBehaviour
{
    #region PublicMemberVariables
    public float m_Distance = 3;
    public bool m_HoldingObject = false;
	public GameObject m_Manager;
    public LayerMask m_LayerMask = (1 << 2) | (1 << 9) | (1 << 10) | (1 << 11) | (1 << 12) | (1 << 13) | (1 << 14) | (1 << 15);
    #endregion

    #region PrivateMemberVariables
    private string m_InputFire1  = "Fire1";
    private string m_InputFire2  = "Fire3";
    private string m_InputPocket = "Pocket";
    private string m_InputDrop   = "Fire2";
    private int m_FrameCount 	 = 0;
    private GameObject m_HoldObject;
    private bool m_FirstFrame = false;
    private GameObject m_HitObject;
    #endregion

    public GameObject HoldObject
    {
        set { m_HoldObject = value; }
        get { return m_HoldObject; }
    }

    void Start()
    {

    }

    // Update is called once per frame
	void Update ()
	{
		if(Input.GetButton(m_InputFire1) || Input.GetButtonDown(m_InputFire2) 
		   || Input.GetButtonDown(m_InputPocket) || Input.GetButtonDown(m_InputDrop) 
		   || Input.GetButtonUp(m_InputFire1))
		{
			int layer = Cast ();
			if(layer == 2)
			{
				return;
			}
			if(!m_HoldingObject)
			{
				if(Input.GetButtonDown(m_InputFire1))
				{
					if(layer == 15)
					{
						if(m_HitObject.GetComponent<Book>())
							m_HitObject.GetComponent<Book>().Interact();

						if(m_HitObject.GetComponent<DairyPage>())
							m_HitObject.GetComponent<DairyPage>().Interact();

						if(m_HitObject.GetComponent<CodeLock>())
							m_HitObject.GetComponent<CodeLock>().Interact();

						if(m_HitObject.GetComponent<SafeCode>())
							m_HitObject.GetComponent<SafeCode>().Interact();
					}
					if(layer == 10)
					{
						m_HitObject.GetComponent<OnClickTrigger>().Trigger();
					}
					if(layer == 12)
					{
						m_HitObject.GetComponent<RDoor>().Interact();
						m_HoldingObject = true;
						m_HoldObject = m_HitObject;
						//m_HitObject.GetComponent<DoorDrag>().Interact();
					}
					if(layer == 13 || layer == 14)
					{
						m_HitObject.GetComponent<PickUp>().Interact();
					}
				}
				if((layer == 11 || layer == 14) && Input.GetButtonDown(m_InputFire2))
				{
					m_HitObject.GetComponent<Inspect>().Interact();
				}
			}
			else if(m_HoldingObject)
			{
				if(layer == 12)
				{
					m_HoldObject.GetComponent<RDoor>().Interact();
					//m_HitObject.GetComponent<DoorDrag>().Interact();
				}
				if(layer == 12 && Input.GetButtonUp(m_InputFire1))
				{
					m_HoldingObject = false;
					m_HoldObject = null;
				}
				//if(layer == 12 && Input.GetButton(m_InputFire1))
				//{
				//	int missLayer = Cast ();
				//	if(missLayer == 0)
				//	{
				//		m_HoldObject.GetComponent<DoorDrag>().StopDrag();
				//	}
				//}
				if(Input.GetButtonDown(m_InputFire2))
				{
					HoldingInspect();
				}
				else if(Input.GetButtonDown(m_InputDrop))
				{
					DropItems();
				}
				else if(Input.GetButtonDown(m_InputPocket) && (m_HoldObject.layer == 13 || m_HoldObject.layer == 14))
				{
					m_Manager.GetComponent<Manager>().AddInventoryItem(m_HoldObject);
					m_Manager.GetComponent<Manager>().SetInventoryFocusOnSelect();
					m_HoldObject.SetActive(false);
					m_HoldingObject = false;
					m_HoldObject = null;
				}
				//else if(Input.GetButtonUp(m_InputFire1))
				//{
				//	if(m_HoldObject.GetComponent<DoorDrag>())
				//	{
				//		m_HoldObject.GetComponent<DoorDrag>().StopDrag();
				//	}
				//}
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

	public void HoldingInspect()
	{
		if(m_HoldObject.gameObject.GetComponent<Inspect>())
		{
			if(m_HoldObject.gameObject.GetComponent<PickUp>())
			{
				if(m_HoldObject.gameObject.GetComponent<PickUp>().HoldingObject && !m_HoldObject.gameObject.GetComponent<Inspect>().IsInspecting)
				{			
					m_HoldObject.gameObject.GetComponent<Inspect>().Interact();
				}
				else if(m_HoldObject.gameObject.GetComponent<Inspect>().IsInspecting)
				{
					m_HoldObject.gameObject.GetComponent<Inspect>().StopInspecting();
				}
			}
			else if(m_HoldObject.gameObject.GetComponent<Inspect>().IsInspecting)
			{
				m_HoldObject.gameObject.GetComponent<Inspect>().StopInspecting();
			}
		}
	}

	public void DropItems()
	{
		if(m_HoldObject.gameObject.GetComponent<Inspect>() && m_HoldObject.GetComponent<PickUp>())
		{
			m_HoldObject.gameObject.GetComponent<Inspect>().Drop();
		}
		else if(m_HoldObject.GetComponent<PickUp>())
		{
			m_HoldObject.GetComponent<PickUp>().Drop();
		}
	}
}