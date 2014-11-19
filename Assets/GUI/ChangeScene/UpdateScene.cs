using UnityEngine;
using System.Collections;

public class UpdateScene : MonoBehaviour 
{
	//public RasmusGameSave m_Game;

	// Use this for initialization
	void Start () 
	{
		//RoomManager.LoadRoom(Application.loadedLevelName);
		//RasmusGameSave.UpdateLevel();
		//Debug.Log(RasmusGameSave.m_HoldingObject);
		//if(RasmusGameSave.m_HoldingObject)
		//{
		//	Destroy(Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<DontStoreObjectInRoom>(), 1);
		//	Destroy(Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<StoreInformation>(), 1);
		//	Destroy(Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<UniqueIdentifier>(), 1);
		//	Camera.main.GetComponent<RasmusRaycast>().HoldObject.AddComponent<StoreInformation>();
		//	//Camera.main.GetComponent<RasmusRaycast>().HoldObject.AddComponent<StoreMaterials>();
		//	Camera.main.GetComponent<RasmusRaycast>().HoldObject.AddComponent<UniqueIdentifier>();
		//	//Camera.main.GetComponent<RasmusRaycast>().HoldObject.renderer.enabled = false;
		//	//Camera.main.GetComponent<RasmusRaycast>().HoldObject.renderer.enabled = true;
		//	//Camera.main.GetComponent<RasmusRaycast>().HoldObject.GetComponent<StoreMaterials>().
		//	//Camera.main.GetComponent<RasmusRaycast>().HoldObject.AddComponent<EmptyObjectIdentifier>();
		//	RasmusGameSave.m_HoldingObject = false;
		//}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(Input.GetKeyDown("l"))
		//{
		//	RasmusGameSave.UpdateLevel();
		//}
		if(gameObject.activeInHierarchy)
		{
			RasmusGameSave.UpdateLevel();
			gameObject.SetActive(false);
		}
	}
}
