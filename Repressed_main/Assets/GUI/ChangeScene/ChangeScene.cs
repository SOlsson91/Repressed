using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour 
{
	public string m_Level;
	public int    m_SpawnPosition;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			//if(Camera.main.GetComponent<RasmusRaycast>().m_HoldingAnObject)
			//{
			//	Camera.main.GetComponent<RasmusRaycast>().HoldObject.AddComponent<DontStoreObjectInRoom>();
			//	RasmusGameSave.m_HoldingObject = true;
			//	//RasmusGameSave.m_Data = LevelSerializer.SaveObjectTree(Camera.main.GetComponent<RasmusRaycast>().HoldObject);
			//	//Destroy(Camera.main.GetComponent<RasmusRaycast>().HoldObject);
			//}
			//else
			//{
			//	RasmusGameSave.m_HoldingObject = false;
			//}
			////else
			////{
			////	RasmusGameSave.m_HoldingObject = false;
			////}
			RasmusGameSave.m_SpawnPosition = m_SpawnPosition;
			RasmusGameSave.SaveLevel();
			//RoomManager.SaveCurrentRoom();
			//RoomManager.LoadRoom(m_Level);
			Application.LoadLevel(m_Level);
			gameObject.SetActive(false);
		}
	}
}
