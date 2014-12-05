using UnityEngine;
using System.Collections;

public class TedLook : MonoBehaviour {

	// Use this for initialization

	
	// Update is called once per frame
	void Update () 
{

		this.gameObject.transform.LookAt( GameObject.FindGameObjectWithTag("Player").transform.position);
	}
}
