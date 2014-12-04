using UnityEngine;
using System.Collections;

public class LockFrameRate : MonoBehaviour {

	// Use this for initialization
	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	


}
