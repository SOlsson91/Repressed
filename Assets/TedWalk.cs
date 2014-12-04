using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TedWalk : MonoBehaviour {

	Transform target;
	float m_timer;
	bool m_secondaryCameraActive;
	bool m_loadDemoEnd;
	float m_loadDemoEndTimer;





	void Start()
	{
		m_timer = 2.0F; 
		m_secondaryCameraActive = false;
		m_loadDemoEnd = false;
		m_loadDemoEndTimer = 3.0F;
		


		
	}
	// Update is called once per frame
	void Update ()
	{
		


		
				gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (-5.0F * Time.deltaTime));

				loadDemoEnd();
				
	}

	void OnTriggerEnter(Collider other)
	{


	
		m_secondaryCameraActive = true;

			
			Camera.main.depth = -3;
		Camera.main.enabled = false;
		GameObject.FindGameObjectWithTag("SecondaryCamera").gameObject.camera.enabled = true;
		m_loadDemoEnd = true;
		
		
	
		

	
	}


	void loadDemoEnd()
	{
	
		if(m_loadDemoEnd == true)
		{

			if(m_loadDemoEndTimer <= 0)
			{
	
	
				Application.LoadLevel ("Demo_endLevel");
			}		
			else
			{
	
				m_loadDemoEndTimer -= Time.deltaTime;
				
			}
		}
	}

	
}

