using UnityEngine;
using System.Collections;

public class Push_player : MonoBehaviour {


	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
		GameObject.FindGameObjectWithTag ("Player").rigidbody.AddForce (0,0,5000);

	}
}
