using UnityEngine;
using System.Collections;

public class AngelLookAtPlayer : MonoBehaviour 
{
	public GameObject m_Player;
	public int m_Field = 130;

	private GameObject m_Ghost;


	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		m_Ghost = new GameObject();
		m_Ghost.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Ghost.transform.LookAt (m_Player.transform);
		float f = Mathf.DeltaAngle (m_Ghost.transform.rotation.eulerAngles.y, m_Player.transform.rotation.eulerAngles.y);
		//Debug.Log (f);
		if(f < m_Field && f > -m_Field)
		{
			transform.LookAt (m_Player.transform);
			Vector3 temp = transform.eulerAngles;
			temp.x = 0;
			temp.z = 0;
			transform.eulerAngles = temp;
			//transform.rotation.eulerAngles.x = 0;
			//transform.rotation.eulerAngles.z = 0;
		}
	}
}
