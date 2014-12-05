using UnityEngine;
using System.Collections;

public class Player_stepped_into_a_hole : MonoBehaviour {
	private FirstPersonController m_HideAndLock;
	// Use this for initialization
	void Start () 
	{
		m_HideAndLock = Component.FindObjectOfType<FirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (GameObject.FindGameObjectWithTag ("DeathCamera").camera.depth == 3) 
		{
			Debug.Log (m_HideAndLock.gameObject.name);
			m_HideAndLock.LockAndHideMouse = false;
			Screen.lockCursor = false;
			Screen.showCursor = true;
			if(Input.anyKeyDown)
			{
				Application.LoadLevel(0);
			}
		}

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
					
						GameObject.FindGameObjectWithTag ("DeathCamera").camera.enabled = true;
						GameObject.FindGameObjectWithTag ("DeathCamera").camera.depth = 3;
				}	
		}
}
