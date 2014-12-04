using UnityEngine;
using System.Collections;

public class DollDeathAnimationDelay : MonoBehaviour {

	float timer;
	bool animationHasPlayed;

	void Start () 
	{
		timer = 1.25F;
		animationHasPlayed = false;
		animation.Stop ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (timer <= 0 && !animationHasPlayed) 
		{
			animation.wrapMode = WrapMode.Once;
			animation.Play ();	
			animationHasPlayed = true;
			this.gameObject.GetComponent<AudioSource>().enabled = true;

				
		} 
		else 
		{
			timer -= Time.deltaTime;
		}

	}
}
