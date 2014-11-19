using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour 
{
	public GameObject[] m_Positions;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(Input.GetKeyDown("f4"))
		//	RasmusGameSave.ClearSaves();
	}

	public void SpawnPlayer(int i)
	{
		GameObject.FindGameObjectWithTag("Player").transform.position = m_Positions[i].transform.position;
	}
}