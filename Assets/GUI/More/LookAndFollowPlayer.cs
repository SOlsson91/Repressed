using UnityEngine;
using System.Collections;

public class LookAndFollowPlayer : MonoBehaviour 
{
	public int 		  m_Field = 130;

	private GameObject m_Player;
	private GameObject m_Ghost;
	//private float      m_Height;

	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		m_Ghost = new GameObject();
		m_Ghost.transform.position = this.transform.position;
		//m_Height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Ghost.transform.LookAt (m_Player.transform);
		float f = Mathf.DeltaAngle (m_Ghost.transform.rotation.eulerAngles.y, m_Player.transform.rotation.eulerAngles.y);

		if(f < m_Field && f > -m_Field)
		{
			transform.LookAt (m_Player.transform);
			Vector3 temp = transform.eulerAngles;
			temp.x = 0;
			temp.z = 0;
			transform.eulerAngles = temp;
			//Vector3 TempVec = Vector3.Lerp(transform.position, GetComponent<NavMeshAgent>().nextPosition, 0.001f);
			//TempVec.y = m_Height;
				


			//transform.position = TempVec;
			m_Ghost.transform.position = transform.position;
			GetComponent<NavMeshAgent>().SetDestination(m_Player.transform.position);
			GetComponent<NavMeshAgent>().updateRotation = false;
		}
		else
		{

			GetComponent<NavMeshAgent>().Stop();
		}
	}
}
