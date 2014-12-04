using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public NavMeshAgent m_Agent;
	public GameObject m_Player;
	// Use this for initialization
	void Start () 
	{
		m_Agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Agent.SetDestination (m_Player.transform.position);
	}
}
