using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	//public NavMeshAgent m_Agent;
	int damp = 5;
	GameObject m_Player;
	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		//m_Agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Player.activeInHierarchy) 
		{


			var rotationAngle = Quaternion.LookRotation(m_Player.transform.position - transform.position);
			rotationAngle *= Quaternion.Euler(0,270,0);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * damp);

		}


		//m_Agent.SetDestination (m_Player.transform.position);
	}
}
