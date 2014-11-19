using UnityEngine;
using System.Collections;

public class HideAndLockCursor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
		Screen.lockCursor = true; 
	}
	

}
