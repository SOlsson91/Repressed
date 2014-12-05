using UnityEngine;
using System.Collections;

public class ted_first_sight : MonoBehaviour {

	float m_speed;
	float m_timer;
	// Use this for initialization
	void Start () 

	{
		m_speed = .75F;
		m_timer = 10.0F;
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		this.gameObject.transform.position = (new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + (m_speed * Time.deltaTime)));
	
		if(m_timer < 0.0F)
		{
			Destroy(this.gameObject);
		}
		else
		{
			m_timer -= Time.deltaTime;
		}

	}
}
