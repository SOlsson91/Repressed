using UnityEngine;
using System.Collections;

public class AssignFlashlightVar : MonoBehaviour {

	public static GameObject s_mainLight;
	void Start()
	{
	
		s_mainLight = gameObject;

	}
	// Use this for initialization

	// Update is called once per frame
	void Update () {
	
	}
}
