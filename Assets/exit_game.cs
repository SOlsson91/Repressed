using UnityEngine;
using System.Collections;

public class exit_game : MonoBehaviour {

	// Use this for initialization

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Application.Quit();
		}
	}
}
