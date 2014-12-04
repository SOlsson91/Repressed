using UnityEngine;
using System.Collections;

public class AgentTemp : MonoBehaviour 
{
	public GameObject m_Destination;

	// Use this for initialization
	void Start () 
	{
		GetComponent<NavMeshAgent>().SetDestination(m_Destination.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GetComponent<NavMeshAgent>().remainingDistance < 0.2f)
			Destroy(this.gameObject);
	}
}
