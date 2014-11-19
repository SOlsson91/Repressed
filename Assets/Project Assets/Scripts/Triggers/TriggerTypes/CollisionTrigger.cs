using UnityEngine;
using System.Collections;

/* If player collide with the gameObject Trigger, start the triggereffects on gameObject

	Made By: Rasmus 30/4
 */

public class CollisionTrigger : MonoBehaviour {	

	public GameObject player;
	public bool m_intersecting = false;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void Update()
	{
		if (gameObject.collider.bounds.Intersects (player.collider.bounds)) 
		{
			if(gameObject.GetComponent<SuperTrigger>() && !m_intersecting)
			{
				SuperTrigger[] triggerArray;
				triggerArray = gameObject.GetComponents<SuperTrigger>();
				foreach(SuperTrigger c in triggerArray)
				{
					if(c.m_Collision){
						c.ActivateTrigger();
					}
				}
			}
			m_intersecting = true;
		}
		else
		{
			m_intersecting = false;
		}
	}
}
