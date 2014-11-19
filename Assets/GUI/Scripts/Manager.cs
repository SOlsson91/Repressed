using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*The class that handles the Items in the inventory. Items holds simple information and reference to the real GameObject
 * Also handles swapping and later stage probably combine as well, maybe inspect as well
 * Select and Manager is cooperating
 * 
Created by: Rasmus
 */

public class Manager : MonoBehaviour 
{
	#region PublicMemberVariables
	public float m_Offset;
	public GameObject m_Messure;
	public GameObject m_Select;
	public GameObject m_Mark;
	public string m_InputCombine = "Combine";
	public string m_InputEquip   = "Fire1";
	public string m_InputSwap 	 = "Swap";
	public string m_InputInspect = "Fire3";
	public string m_InputDrop 	 = "Drop";
	#endregion
	
	#region PrivateMemberVariables
	private bool    m_CanCollabarate = false;
	private Vector3 m_OrignalPosition;
	private int     m_Selected;
	private int     m_LastSelected;
	private float   m_InnerOffset;
	private int     m_StartOffset;
	private GameObject[]     m_Objects;
	private List<GameObject> m_List;
	
	private int m_ObjectToSwitch;
	private int m_StackPosition;
	private bool m_Inspecting;
	#endregion
	// Use this for initialization
	void Start () 
	{
		//Initierar listan ifall den inte är tom från början, och sätter startposition
		m_List = new List<GameObject> ();
		m_OrignalPosition = m_Messure.transform.position;
		m_InnerOffset = m_Messure.collider.bounds.size.x + m_Offset;
		foreach(Transform name in transform)
		{
			m_List.Add(name.gameObject);
		}
		if(m_List != null)
		{
			m_Objects = m_List.ToArray ();
		}
		PlaceChilds ();
		m_Selected = 0;
		m_StartOffset = 0;
		m_ObjectToSwitch = -1;
		m_StackPosition = 0;
		m_Inspecting = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Select.GetComponent<Select>().GetInventoryFocus() > 0)
		{
			UpdateInput();
		}
	}
	
	private void UpdateInput()
	{
		UpdateChildsPosition ();
		
		//Lägger ut ett object från inventoryt, aktiverar objektet och tar bort referensen i själva inventoryt
		//if(Input.GetButtonDown(m_InputEquip) && m_Objects.Length > 0 && m_Inspecting == false)
		//{
		//	SetInventoryFocusOnSelect();
		//	if(Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject == false)
		//	{
		//		TakeAwayOneItem(true);
		//	}
		//	m_Mark.GetComponent<Mark>().ExitMarkBox();
		//	m_ObjectToSwitch = -1;
		//}
		if(Input.GetButtonDown(m_InputDrop) && m_Objects.Length > 0 && m_Inspecting == false)
		{
			SetInventoryFocusOnSelect();
			TakeAwayOneItem(true);
			m_Mark.GetComponent<Mark>().ExitMarkBox();
			m_ObjectToSwitch = -1;
		}

		if(Input.GetKeyDown("z") && m_Inspecting == false && m_Objects.Length > 0)
		{
			SetInventoryFocusOnSelect();
			if(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<ObjectTrigger>())
			{
				Debug.Log("Skall använda use");
				//m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>();
				DestroyOneItem();
			}
			m_Mark.GetComponent<Mark>().ExitMarkBox();
			m_ObjectToSwitch = -1;
		}
		
		//SwapFunktion
		if(Input.GetButtonDown(m_InputSwap) && m_Objects.Length > 0 && m_Inspecting == false)
		{
			SetInventoryFocusOnSelect();
			if(m_ObjectToSwitch == -1)
			{
				m_ObjectToSwitch = m_Selected;
				m_Mark.GetComponent<Mark>().ChangeMarkBox(true, m_Objects[m_Selected].transform.position);
			}
			else
			{
				GameObject temp = m_Objects[m_ObjectToSwitch];
				m_Objects[m_ObjectToSwitch] = m_Objects[m_Selected];
				m_Objects[m_Selected] = temp;
				m_List.Clear();
				for(int i = 0; i < m_Objects.Length; i++)
				{
					m_List.Add(m_Objects[i]);
				}
				m_Objects = m_List.ToArray();
				UpdateChildsPosition();
				m_ObjectToSwitch = -1;
				m_Mark.GetComponent<Mark>().ExitMarkBox();
			}
		}
		
		//CombineFunktion
		if(Input.GetButtonDown(m_InputCombine) && m_Objects.Length > 0 && m_Inspecting == false &&
		   Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject == false)
		{
			SetInventoryFocusOnSelect();
			if(m_ObjectToSwitch == -1)
			{
				m_ObjectToSwitch = m_Selected;
				m_Mark.GetComponent<Mark>().ChangeMarkBox(false, m_Objects[m_Selected].transform.position);
			}
			else
			{
				if(m_ObjectToSwitch == m_Selected)
				{
					m_ObjectToSwitch = -1;
					m_Mark.GetComponent<Mark>().ExitMarkBox();
				}
				else
				{
					DestroyTwoItems();
					
					m_List.Clear();
					for(int i = 0; i < m_Objects.Length; i++)
					{
						m_List.Add(m_Objects[i]);
					}
					m_Objects = m_List.ToArray();
					
					UpdateChildsPosition();
					m_Select.GetComponent<Select>().UpdateSelected(m_Selected);
					m_ObjectToSwitch = -1;
					m_Mark.GetComponent<Mark>().ExitMarkBox();
				}
			}
		}
		
		//Inspect
		if(Input.GetButton(m_InputInspect) && m_Objects.Length > 0)
		{
			SetInventoryFocusOnSelect();
			if(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<Inspect>() != null)
			{
				m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].SetActive(true);
				m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<Inspect>().Interact();
				m_Inspecting = true;
			}
		}
		else if(Input.GetButtonUp(m_InputInspect) && m_Inspecting == true)
		{
			m_Inspecting = false;
			m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<Inspect>().StopInspecting();
			m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].SetActive(false);
		}
	}
	
	public void SetInventoryFocusOnSelect()
	{
		m_Select.GetComponent<Select>().SetInventoryFocus();
	}

	private void DestroyOneItem()
	{
		if(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length < 2)
		{
			m_List.Remove(m_Objects[m_Selected]);
			Destroy(m_Objects[m_Selected]);
			m_Objects = m_List.ToArray();
			
			if(m_Selected != m_Objects.Length && m_StartOffset < 1)
			{
				UpdateChildsPosition ();
			}
			else
			{
				if(m_StartOffset > 0)
				{
					m_StartOffset--;
				}
				m_Selected--;
				m_Select.GetComponent<Select>().UpdateSelected(m_Selected);
				UpdateChildsPosition ();
			}
			if(m_Objects.Length < 1)
			{
				m_Selected = 0;
			}
		}
		else
		{
			m_Objects[m_Selected].GetComponent<InventoryItem>().TakeAwayAndDestoy();
		}
	}

	//If Combine is successfull, 2 items is taken away and one item is added
	private void DestroyTwoItems()
	{
		List<int> m_TempIDList;
		if(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>() != null 
		   && m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>() != null)
		{
			m_TempIDList = m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>().m_IDsCollaborate;
			if(m_TempIDList.Contains(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<Id>().ObjectId))
			{
				m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>().ActivateTrigger();
				m_CanCollabarate = true;
				//Haxxor
				int TempInt = m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<SuperTrigger>().m_TriggerEvents[0].m_Event;
				List<EventSystem> es = Resources.FindObjectsOfTypeAll<EventSystem>().ToList();
				EventSystem TempEvent =  new EventSystem();
				foreach(EventSystem eve in es)
				{
					if(eve.m_ID == TempInt)
					{
						TempEvent = eve;
					}
				}	
				int TempInt2 = TempEvent.m_Objects[0].m_Id;
				//Debug.Log("Lyckades så här långt : " + TempInt2);
				Id[] TempObjects = Resources.FindObjectsOfTypeAll<Id>();
				//Id[] test2 = Resources.FindObjectsOfTypeAll<Id>().ToArray();
				//List<Id> TempObjects = Resources.FindObjectsOfTypeAll<Id>().ToList();
				//Debug.Log(test.Length + " " + test2.Length + " " + TempObjects.Count);
				
				GameObject TempObject = new GameObject();
				for(int i = 0; i < TempObjects.Length; i++)
				{
					if(TempObjects[i].m_Id == TempInt2)
					{
						TempObject = TempObjects[i].gameObject;
					}
				}
				TempObject.SetActive(true);
				TempObject.GetComponent<PickUp>().Test();
				AddInventoryItem(TempObject);
				TempObject.SetActive(false);	
			}
		}
		else
		{
			m_CanCollabarate = false;
		}
		if(m_CanCollabarate == true)
		{
			bool checkLast = false;
			if(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject().Length < 2)
			{
				Destroy(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject().Length - 1]);
				m_List.Remove(m_Objects[m_ObjectToSwitch]);
				checkLast = true;
			}
			else
			{
				Destroy(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject().Length - 1]);
				m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().TakeAwayAndDestoy();
			}
			
			if(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length < 2)
			{
				Destroy(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1]);
				
				m_List.Remove(m_Objects[m_Selected]);
				Destroy(m_Objects[m_Selected]);
				if(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject().Length < 2)
				{
					Destroy(m_Objects[m_ObjectToSwitch]);
				}
				
				m_Objects = m_List.ToArray();
				
				if(m_Selected != m_Objects.Length && m_StartOffset < 1)
				{
					UpdateChildsPosition ();
				}
				else
				{
					if(m_StartOffset > 0)
					{
						m_StartOffset--;
					}
					m_Selected--;
					m_Select.GetComponent<Select>().UpdateSelected(m_Selected);
					UpdateChildsPosition ();
				}
				if(m_Objects.Length < 1)
				{
					m_Selected = 0;
				}
			}
			else
			{
				Destroy(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1]);
				m_Objects[m_Selected].GetComponent<InventoryItem>().TakeAwayAndDestoy();
				
				if(m_Objects[m_ObjectToSwitch].GetComponent<InventoryItem>().GetGameObject().Length < 2 && checkLast == true)
				{
					Destroy(m_Objects[m_ObjectToSwitch]);
					m_Objects = m_List.ToArray();
				}
			}
		}
		else
		{
			Debug.Log("Ingen collabarate imellan objekten");
		}
	}
	
	//Takes out an object from inventoryt, to hand if empty, else it drops in front of the player
	private void TakeAwayOneItem(bool holdit)
	{
		if(m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length < 2)
		{
			m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1].SetActive(true);
			//Make the player hold the item if hand is empty..
			if(Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject == false && holdit == true)
			{

				Camera.main.GetComponent<RasmusRaycast>().HoldObject = m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1];
				Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject = true;
				Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<PickUp>().Interact();
			}
			else
			{
				m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1].GetComponent<PickUp>().Drop();
			}

			m_List.Remove(m_Objects[m_Selected]);
			Destroy(m_Objects[m_Selected]);
			m_Objects = m_List.ToArray();
			
			if(m_Selected != m_Objects.Length && m_StartOffset < 1)
			{
				UpdateChildsPosition ();
			}
			else
			{
				if(m_StartOffset > 0)
				{
					m_StartOffset--;
				}
				m_Selected--;
				m_Select.GetComponent<Select>().UpdateSelected(m_Selected);
				UpdateChildsPosition ();
			}
			if(m_Objects.Length < 1)
			{
				m_Selected = 0;
			}
		}
		else
		{
			m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1].SetActive(true);
			if(Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject == false && holdit == true)
			{
				Camera.main.GetComponent<RasmusRaycast>().HoldObject = m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1];
				Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject = true;
				Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<PickUp>().Interact();
			}
			else
			{
				m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject()[m_Objects[m_Selected].GetComponent<InventoryItem>().GetGameObject().Length - 1].GetComponent<PickUp>().Drop();
			}
			m_Objects[m_Selected].GetComponent<InventoryItem>().TakeAwayOneItem();
		}
	}
	
	//Add a new item that reference to the gameobject
	public void AddInventoryItem(GameObject obj)
	{
		if(CheckForStack(obj))
		{
			AddStackItem(obj);
		}
		else
		{
			m_List.Add (CreateItem (obj));
		}
		
		m_Objects = m_List.ToArray ();
		PlaceChilds ();
	}
	
	//Check if Item allready exist in the inventory
	private bool CheckForStack(GameObject obj)
	{
		bool tempBool = false;
		for(int i = 0; i < m_Objects.Length; i++)
		{
			if(m_Objects[i].GetComponent<InventoryItem>().GetGameObject()[0].renderer.material.mainTexture == obj.renderer.material.mainTexture)
			{
				tempBool = true;
				m_StackPosition = i;
			}
		}
		return tempBool;
	}
	//Make a new inventoryIcon
	private GameObject CreateItem(GameObject obj)
	{
		GameObject Item = Instantiate(m_Messure) as GameObject;
		Item.AddComponent<InventoryItem>();
		Item.GetComponent<InventoryItem> ().SetReference (obj);
		Item.renderer.enabled = true;
		Item.transform.parent = this.transform;
		Item.renderer.material.mainTexture = obj.renderer.material.mainTexture;
		return Item;
	}
	private void AddStackItem(GameObject obj)
	{
		m_Objects [m_StackPosition].GetComponent<InventoryItem> ().SetReference (obj);
	}
	
	//Place the items in inventory so you allways se the selected one
	private void PlaceChilds()
	{
		if(m_LastSelected != 0 && m_LastSelected < m_Objects.Length - 1)
		{
			//To move the objects offset to right or left
			if(m_Objects[m_LastSelected].GetComponent<InventoryItem>().ReturnInt() == 1)
			{
				if(m_LastSelected == m_Selected - 1)
				{
					m_StartOffset++;
				}
			}
			else if(m_Objects[m_LastSelected].GetComponent<InventoryItem>().ReturnInt() == -1)
			{
				if(m_LastSelected == m_Selected + 1)
				{
					m_StartOffset--;
				}
			}
		}
		else
		{
			//To move when at last/first item
			if(m_LastSelected == 0 && m_Selected == m_Objects.Length - 1 && m_Objects.Length > 6)
			{
				m_StartOffset = m_Objects.Length -6;
			}
			else if(m_LastSelected == m_Objects.Length - 1 && m_Selected == 0)
			{
				m_StartOffset = 0;
			}
		}
		
		for(int i = 0; i < m_Objects.Length; i++)
		{
			Vector3 temp = m_OrignalPosition;
			temp.x -=  (m_StartOffset * m_InnerOffset);
			temp.x += i * m_InnerOffset;
			m_Objects[i].transform.position = temp;
		}
	}
	
	//Update child, only used after a item is dropped atm, 
	private void UpdateChildsPosition()
	{
		if(m_List.Count != m_Objects.Length)
		{
			Debug.Log("Något har blivit fel");
		}
		for(int i = 0; i < m_Objects.Length; i++)
		{
			Vector3 temp = m_OrignalPosition;
			temp.x -=  (m_StartOffset * m_InnerOffset);
			temp.x += i * m_InnerOffset;
			m_Objects[i].transform.position = temp;
		}
	}
	
	//Chose selected from the Input (GUI for now)
	public void SetSelected(int sel)
	{
		m_LastSelected = m_Selected;
		m_Selected = sel;
		PlaceChilds ();
	}
	
	
	//Return the selected gameobject
	public GameObject SelectedGameObject()
	{
		if(m_Objects.Length > 0 && m_Selected < m_Objects.Length)
		{
			return m_Objects [m_Selected];
		}
		else 
		{
			return null;
		}
	}
	
	public float GetInventoryFocus()
	{
		return m_Select.GetComponent<Select> ().GetInventoryFocus ();
	}
	
	//Return the list of all items in inventoryt
	public GameObject[] GetObjectVector()
	{
		return m_Objects;
	}
	
}
