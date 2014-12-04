using UnityEngine;
using System.Collections;

public class TedWalk2 : MonoBehaviour {

	float timer1;
	float timer2;
	float timer3;
	float slowDownTimer;
	float endTimer;
	int playerIsAwakeStage;
	float playerIsAwakeTimer;
	bool playerIsAwakeTransition;
	bool playerIsAwake;
	bool playerMoveEyelids;
	float playerMoveEyelidsTimer;

	public GameObject colliderBox;
	// Use this for initialization
	void Start () 
	{

		
		timer1 = 3.0F;
		timer2 = 10F;
		timer3 = 4.5F;
		slowDownTimer = 1.5F;
		endTimer = 1.5F;
		playerIsAwake = false;
		playerIsAwakeStage = 0;
		playerIsAwakeTimer = 3.0F;
		playerIsAwakeTransition = false;
		playerMoveEyelids = false;
		playerMoveEyelidsTimer = 3.0F;




	}

	
	// Update is called once per frame
	void Update () 
	{

		if(playerIsAwake && playerMoveEyelids)
		{
			timerSequence();
		}
		else if(playerIsAwake)
		{

			if(playerMoveEyelidsTimer >0)
			{
			GameObject Eyelid = GameObject.FindGameObjectWithTag("Eyelid");
			Eyelid.gameObject.transform.position = new Vector3(Eyelid.transform.position.x - (1F * Time.deltaTime), Eyelid.transform.position.y, Eyelid.transform.position.z);
			
			GameObject Eyelid_2 = GameObject.FindGameObjectWithTag("Eyelid_2");
			Eyelid_2.gameObject.transform.position = new Vector3(Eyelid_2.transform.position.x + (1F * Time.deltaTime), Eyelid_2.transform.position.y, Eyelid_2.transform.position.z);
			playerMoveEyelidsTimer-=Time.deltaTime;
			
			}
			else
			{
				GameObject Eyelid = GameObject.FindGameObjectWithTag("Eyelid");
				Destroy(Eyelid);
	
				GameObject Eyelid_2 = GameObject.FindGameObjectWithTag("Eyelid");
				Destroy(Eyelid_2);
				playerMoveEyelids = true;
				
				
			}
		}
		else if(!playerIsAwake)
		{
		

  			awaken();

		

		}
	

	}

	void timerSequence()
	{
		
		if(timer1 < 0.0F)
		{
			this.gameObject.transform.position = new Vector3(transform.position.x -(1.1F * Time.deltaTime), transform.position.y, transform.position.z);
			timer2-= Time.deltaTime;
			
		}	
		else
		{
			timer1 -= Time.deltaTime;
		}
		
		
		
		if(timer2 < 0.0F)
		{
			
			GameObject.FindGameObjectWithTag("Player"). rigidbody.AddForce(0, 0, -.5F);
			timer3 -= Time.deltaTime;
			
			
		}
		
		if(timer3 < 0.0F && slowDownTimer >= 0)
		{
			
			GameObject.FindGameObjectWithTag("Player").rigidbody.velocity = Vector3.zero;
			slowDownTimer -= Time.deltaTime;	
		}
		if(slowDownTimer <= 0)
		{
			
			if(endTimer <= 0)
			{
				
				GameObject.FindGameObjectWithTag("SecondaryCamera").GetComponent<Camera>().depth = 1;
				Application.LoadLevel("Menu_endOfDemo");
				
			}
			else
			{
				endTimer -= Time.deltaTime;
			}
			
			
			
		}
		
	}

	void awaken()
	{

		if(playerIsAwakeStage == 0)
		{
			GameObject Eyelid = GameObject.FindGameObjectWithTag("Eyelid");
			Eyelid.gameObject.transform.position = new Vector3(Eyelid.transform.position.x + (0.015F * Time.deltaTime), Eyelid.transform.position.y, Eyelid.transform.position.z);
		}
		else if(playerIsAwakeStage == 1)
		{
			GameObject Eyelid = GameObject.FindGameObjectWithTag("Eyelid");
			Eyelid.gameObject.transform.position = new Vector3(Eyelid.transform.position.x - (0.015F * Time.deltaTime), Eyelid.transform.position.y, Eyelid.transform.position.z);
			
		}
			if(playerIsAwakeTimer <= 0.0F)
			{
				if(playerIsAwakeStage ==1)
				{	
					playerIsAwakeStage = 0;
					if(playerIsAwakeTransition)
					{
						playerIsAwake = true;
						GameObject Eyelid = GameObject.FindGameObjectWithTag("Eyelid");
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect>().enabled = false;
					
					
					}
					else
					{
						playerIsAwakeTransition = true; 
					}
				
				}
				else
				{
					++playerIsAwakeStage; 
				}
			
				playerIsAwakeTimer = 3.0F;
			
			}
			else
			{
				playerIsAwakeTimer -= Time.deltaTime;		
			}
	}
}
