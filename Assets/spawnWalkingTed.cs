using UnityEngine;
using System.Collections;

public class spawnWalkingTed : MonoBehaviour 
	{
	

		
	// Use this for initialization

	void Start()
	{
		
	}
	void OnTriggerEnter(Collider other)
	{
		GameObject.FindGameObjectWithTag ("TedWalking").SendMessage("setM_unlockWalk",true);
		
		
	}

}
