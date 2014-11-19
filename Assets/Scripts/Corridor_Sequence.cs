using UnityEngine;
using System.Collections;

//Used to control the different stages of the corridor sequence

public class Corridor_Sequence : MonoBehaviour {



	private GameObject 		m_player;
	private Camera		   m_mainCamera;
	private int 			m_sequenceStage;
	private Vector3			m_playerPosition;




	void Start () 
	{
		Random.seed= int.Parse(System.DateTime.Now.ToString ("yy:dd:hh:mm:ss"));

		m_player = 				GameObject.FindGameObjectWithTag ("Player");

		m_sequenceStage = 		0;
		m_playerPosition = 		m_player.transform.position;
	

	}
	void LateUpdate()
	{

		switch(m_sequenceStage)
		{
			case 1:
				changeFow(170,5.0F);
			  	break;
		}

	}


	void OnTriggerEnter(Collider other) 
	{
	

		if(other.gameObject.tag == "CorridorSequence")
		{
			m_sequenceStage ++;
		}
		
		Destroy(other.gameObject);
	
		//Destroy(other.gameObject);
	}

	void changeFow(int targetFow, float fowChangeSpeed)
	{
		if(Camera.main.fieldOfView < targetFow)
		{
			Camera.main.fieldOfView += fowChangeSpeed * Time.deltaTime;
		}
		else
		{
			Camera.main.fieldOfView -= fowChangeSpeed * Time.deltaTime;
		}
		
	
	}

	void limitPlayerSpeed()
	{
		if(m_player.transform.position != m_playerPosition)
		{
			m_player.transform.position = m_playerPosition;
		}
	
	}


}