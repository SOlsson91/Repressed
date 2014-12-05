using UnityEngine;
using System.Collections;

public class Rotate_Door : MonoBehaviour {

	float m_rotationTimer;
	// Use this for initialization
	void Start () 
	{
		m_rotationTimer = 0.0F;
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate (0, Time.deltaTime,0);
	}
}
