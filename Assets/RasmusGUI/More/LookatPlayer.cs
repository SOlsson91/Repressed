using UnityEngine;
using System.Collections;

public class LookatPlayer : MonoBehaviour 
{
	private GameObject m_Player;
	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt(m_Player.transform.position);
	}
}
