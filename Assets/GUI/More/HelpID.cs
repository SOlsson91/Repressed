using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelpID : MonoBehaviour 
{
	public int  m_CheckID;
	//public bool m_CheckEvent;

	public string[] m_ShowIdAndGameobjects;
	public string[] m_ShowEventIdAndGameobjects;
	public GameObject[] m_GameObjectsWithID;
	public GameObject[] m_GameObjectsWithEventID;
	public GameObject[] m_FoundGameObject;

	private GameObject[] m_AllGameObjects;
	private List<GameObject> ObjectList = new List<GameObject>();
	private List<GameObject> ErrorList  = new List<GameObject>();
	private List<string> StringList = new List<string>();
	private bool m_Start = true;

	private float m_FixCounter = 0;
	private float m_FixCount = 1f;

	// Use this for initialization
	void Start () 
	{
		m_AllGameObjects = ObjectList.ToArray();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Start)
		{
			m_FixCounter += Time.deltaTime;
			if(m_FixCounter > m_FixCount)
			{
				FindAllObjects();
				m_Start = false;
			}
		}
		if(Input.GetKeyDown("f1"))
		{
			//if(m_CheckEvent)
			//{
				FindObjectWithEventID();
			//}
			//else
			//{
			//	FindObjectWithID();
			//}
		}
		if(Input.GetKeyDown("f2"))
		{
			FindObjectWithID();
		}
	}

	//Gather all objects after start
	private void FindAllObjects()
	{
		//GetAllGameObjects
		m_AllGameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		Debug.Log("All gameobjects: " + m_AllGameObjects.Length);
		//GetAllGameObjectsWithID-component
		foreach(GameObject obj in m_AllGameObjects)
		{
			//if(obj.GetComponent<Id>())
			//{
			//	ObjectList.Add(obj);
			//}
			foreach(Transform child in obj.transform)
			{
				if(child.gameObject.GetComponent<Id>())
				{
					ObjectList.Add(child.gameObject);
				}
			}
		}
		m_GameObjectsWithID = ObjectList.ToArray();
		Debug.Log("Gameobjects with Id: " + m_GameObjectsWithID.Length);
		//GetAllGameObjectsWithEventSystem-component
		ObjectList.Clear();
		foreach(GameObject obj in m_AllGameObjects)
		{
			if(obj.GetComponent<EventSystem>())
				ObjectList.Add(obj);
		}
		m_GameObjectsWithEventID = ObjectList.ToArray();
		Debug.Log("Gameobjects with EventId: " + m_GameObjectsWithEventID.Length);

		//Other Functions
		SortByID();
		ListID();
		ListEventsID();
		CheckForDuplicates();
	}

	//Find gameobject with an ID
	private void FindObjectWithID()
	{
		int tempI = 0;
		ObjectList.Clear();
		foreach(GameObject obj in m_GameObjectsWithID)
		{
			if(obj.GetComponent<Id>().m_Id == m_CheckID)
			{
				ObjectList.Add(obj);
				tempI++;
			}
		}
		m_FoundGameObject = ObjectList.ToArray();
		if(tempI > 1)
		{
			Debug.Log("More than one Gameobject have this ID");
		}
		else if(tempI == 0)
		{
			Debug.Log("No Gameobject have this ID");
		}
		else
		{
			Debug.Log("One Gameobject have this ID = " + m_FoundGameObject[0]);
		}
	}

	//Find Gameibject with an EventID
	private void FindObjectWithEventID()
	{
		int tempI = 0;
		ObjectList.Clear();
		foreach(GameObject obj in m_GameObjectsWithEventID)
		{
			if(obj.GetComponent<EventSystem>().m_ID == m_CheckID)
			{
				ObjectList.Add(obj);
				tempI++;
			}
		}
		m_FoundGameObject = ObjectList.ToArray();
		if(tempI > 1)
		{
			Debug.Log("More than one Gameobject have this EventID");
		}
		else if(tempI == 0)
		{
			Debug.Log("No Gameobject have this EventID");
		}
		else
		{
			Debug.Log("One Gameobject have this EventID = " + m_FoundGameObject[0]);
		}
	}

	//Sort all objects
	private void SortByID()
	{
		QuickSort_Recursive(m_GameObjectsWithID, 0, m_GameObjectsWithID.Length-1);
		QuickSort_Recursive2(m_GameObjectsWithEventID, 0, m_GameObjectsWithEventID.Length-1);
	}

	#region Sorting
	public int Partition(GameObject [] numbers, int left, int right)
	{
		int pivot = numbers[left].GetComponent<Id>().m_Id;
		while (true)
		{
			while (numbers[left].GetComponent<Id>().m_Id < pivot)
				left++;
			
			while (numbers[right].GetComponent<Id>().m_Id > pivot)
				right--;

			if (numbers[right].GetComponent<Id>().m_Id == pivot && numbers[left].GetComponent<Id>().m_Id == pivot)
			{
				left++;
			}

			if (left < right)
			{
				GameObject temp = numbers[right];
				numbers[right] = numbers[left];
				numbers[left] = temp;
			}
			else
			{
				return right;
			}
		}
	}
	
	public void QuickSort_Recursive(GameObject [] arr, int left, int right)
	{
		// For Recusrion
		if(left < right)
		{
			int pivot = Partition(arr, left, right);
			
			if(pivot > 1)
				QuickSort_Recursive(arr, left, pivot - 1);
			
			if(pivot + 1 < right)
				QuickSort_Recursive(arr, pivot + 1, right);
		}
	}

	public int Partition2(GameObject [] numbers, int left, int right)
	{
		int pivot = numbers[left].GetComponent<EventSystem>().m_ID;
		while (true)
		{
			while (numbers[left].GetComponent<EventSystem>().m_ID < pivot)
				left++;
			
			while (numbers[right].GetComponent<EventSystem>().m_ID > pivot)
				right--;

			if (numbers[right].GetComponent<EventSystem>().m_ID == pivot && numbers[left].GetComponent<EventSystem>().m_ID == pivot)
			{
				left++;
			}

			if (left < right)
			{
				GameObject temp = numbers[right];
				numbers[right] = numbers[left];
				numbers[left] = temp;
			}
			else
			{
				return right;
			}
		}
	}
	
	public void QuickSort_Recursive2(GameObject [] arr, int left, int right)
	{
		// For Recusrion
		if(left < right)
		{
			int pivot = Partition2(arr, left, right);
			
			if(pivot > 1)
				QuickSort_Recursive2(arr, left, pivot - 1);
			
			if(pivot + 1 < right)
				QuickSort_Recursive2(arr, pivot + 1, right);
		}
	}

	public int Partition3(EventSystem [] numbers, int left, int right)
	{
		int pivot = numbers[left].m_ID;
		while (true)
		{
			while (numbers[left].m_ID < pivot)
				left++;
			
			while (numbers[right].m_ID > pivot)
				right--;
			
			if (numbers[right].m_ID == pivot && numbers[left].m_ID == pivot)
			{
				left++;
			}
			
			if (left < right)
			{
				EventSystem temp = numbers[right];
				numbers[right] = numbers[left];
				numbers[left] = temp;
			}
			else
			{
				return right;
			}
		}
	}
	
	public void QuickSort_Recursive3(EventSystem [] arr, int left, int right)
	{
		// For Recusrion
		if(left < right)
		{
			int pivot = Partition3(arr, left, right);
			
			if(pivot > 1)
				QuickSort_Recursive3(arr, left, pivot - 1);
			
			if(pivot + 1 < right)
				QuickSort_Recursive3(arr, pivot + 1, right);
		}
	}
	#endregion

	//Check for duplicated ids
	private void CheckForDuplicates()
	{
		for(int i = 0; i < m_GameObjectsWithID.Length - 1; i++)
		{
			if(m_GameObjectsWithID[i].GetComponent<Id>().m_Id == m_GameObjectsWithID[i+1].GetComponent<Id>().m_Id)
				Debug.Log("Duplicated ID: " + m_GameObjectsWithID[i].GetComponent<Id>().m_Id);
		}
		CheckForEventDuplicates();
	}

	private void CheckForEventDuplicates()
	{
		List<EventSystem> tempList = new List<EventSystem>();
		EventSystem[] tempEvents = tempList.ToArray();
		for(int i = 0; i < m_GameObjectsWithEventID.Length; i++)
		{
			foreach(EventSystem ev in m_GameObjectsWithEventID[i].GetComponents<EventSystem>())
			{
				tempList.Add(ev);
				ev.GetObjects();
			}
		}
		tempEvents = tempList.ToArray();
		QuickSort_Recursive3(tempEvents, 0, tempEvents.Length-1);
		for(int i = 0; i < tempEvents.Length - 1; i++)
		{
			if(tempEvents[i].m_ID == tempEvents[i+1].m_ID)
				Debug.Log("Duplicated EventID: " + tempEvents[i].m_ID);
		}
	}

	private void ListID()
	{
		StringList.Clear();
		foreach(GameObject obj in m_GameObjectsWithID)
		{
			string tempS = obj.GetComponent<Id>().m_Id + " " + obj.name;
			StringList.Add(tempS);
		}
		m_ShowIdAndGameobjects = StringList.ToArray();
	}

	private void ListEventsID()
	{
		StringList.Clear();
		foreach(GameObject obj in m_GameObjectsWithEventID)
		{
			string tempS = "";
			foreach(EventSystem ev in obj.GetComponents<EventSystem>())
			{
				tempS += ev.m_ID + " ";
			}
			tempS += obj.name;
			StringList.Add(tempS);
		}
		m_ShowEventIdAndGameobjects = StringList.ToArray();
	}
}
