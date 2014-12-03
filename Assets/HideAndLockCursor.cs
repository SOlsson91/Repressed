using UnityEngine;
using System.Collections;

public class HideAndLockCursor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
		Screen.lockCursor = true; 
		Application.targetFrameRate = 30;
		QualitySettings.vSyncCount = 2;
	}
	

}
