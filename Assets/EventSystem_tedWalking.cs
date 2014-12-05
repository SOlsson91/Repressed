using UnityEngine;
using System.Collections;

public class EventSystem_tedWalking : MonoBehaviour {

	bool m_unlockWalk;
	// Use this for initialization
	void Start () 
	{
		m_unlockWalk = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_unlockWalk)
		{
		
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + (1.2F * Time.deltaTime) );
		}
	}

	void setM_unlockWalk(bool unlock)
	{
		m_unlockWalk = unlock;
	}
}
