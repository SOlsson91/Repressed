using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum m_DifferentEffects
{
	None,
	Active_No,
	Active_Yes,
	Change_Model,
	Delay,
	Door_Close,
	Door_Open,
	Door_Angle,
	Drawer_Close,
	Drawer_Open,
	Kill,
	Lights_Off,
	Lights_On,
	Lights_Toggle,
	Lock,
	Particles_Off,
	Particles_On,
	PlayMovie,
	Spawn,
	Sound_Effect,
	Unlock
};

public class EventSystem : MonoBehaviour
{
	#region SystemData
	public int   			m_ID;
	public bool     		m_HasBeenActivated = false;
	public float 			timer 	  		   = 0;
	public List<GameObject>	m_Objects 		   = new List<GameObject> ();
	#endregion

	#region EditorData
	public bool			 				  m_EventToggle 	= false;
	public string						  m_Description	    = "";
	public List<EventEffect>              m_Events          = new List<EventEffect>();
	#endregion

	void Start()
	{
		GetObjects ();

		if(gameObject.GetComponent<ActivateDeactivate>()){}
		else
		{
			gameObject.AddComponent("ActivateDeactivate");
		}
		if(gameObject.GetComponent<ChangeModel>()){}
		else
		{
			gameObject.AddComponent("ChangeModel");
		}
		if(gameObject.GetComponent<OpenCloseDoor>()){}
		else
		{
			gameObject.AddComponent("OpenCloseDoor");
		}
		if(gameObject.GetComponent<OpenCloseDrawer>()){}
		else
		{
			gameObject.AddComponent("OpenCloseDrawer");
		}
		if(gameObject.GetComponent<KillPlayer>()){}
		else
		{
			gameObject.AddComponent("KillPlayer");
		}
		if(gameObject.GetComponent<LockUnlock>()){}
		else
		{
			gameObject.AddComponent("LockUnlock");
		}
		if(gameObject.GetComponent<TriggerLights>()){}
		else
		{
			gameObject.AddComponent("TriggerLights");
		}
		//if(gameObject.GetComponent<SoundEffect>()){}
		//else
		//{
		//	gameObject.AddComponent("SoundEffect");
		//}
	}

	void Update()
	{

		timer -= Time.deltaTime;
	}

	public void ActivateEvents()
	{
		StartCoroutine ("RunEvents");
		m_HasBeenActivated = true;
	}


	public IEnumerator RunEvents()
	{
		for(int i = 0; i < m_Events.Count(); i++)
		{
			if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Active_No)
			{
				gameObject.GetComponent<ActivateDeactivate>().DeActivate(m_Objects[i]);
			}

			// <===============================================================================>
		
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Active_Yes)
			{
				gameObject.GetComponent<ActivateDeactivate>().Activate(m_Objects[i]);
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Change_Model)
			{
				gameObject.GetComponent<ChangeModel> ().SetStages (m_Events[i].m_ChangeModelMh, m_Events[i].m_ChangeModelTx);
				gameObject.GetComponent<ChangeModel> ().ModelChange(m_Objects[i]);
			}
				// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Delay)
			{
				timer = 0;
				timer += m_Events[i].m_DelayValue;
			}
		
			// <===============================================================================>
				
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Door_Close)
			{
				gameObject.GetComponent<OpenCloseDoor>().CloseDoor(m_Objects[i]);
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Door_Open)
			{
				gameObject.GetComponent<OpenCloseDoor>().OpenDoor(m_Objects[i]);
			}

			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Door_Angle)
			{
				gameObject.GetComponent<OpenCloseDoor>().AngleDoor(m_Objects[i], m_Events[i].m_OpenDoor);
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Drawer_Close)
			{
				
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Drawer_Open)
			{
				
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Kill)
			{
				gameObject.GetComponent<KillPlayer>().KillThePlayer();
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Lights_Off)
			{
				gameObject.GetComponent<TriggerLights>().LightOff(m_Objects[i]);
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Lights_On)
			{
				gameObject.GetComponent<TriggerLights>().LightOn(m_Objects[i]);
			}
		
			// <===============================================================================>
		
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Lights_Toggle)
			{
				gameObject.GetComponent<TriggerLights>().LightSwitch(m_Objects[i]);
			}
			
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Lock)
			{
				gameObject.GetComponent<LockUnlock>().Lock(m_Objects[i]);
			}
		
		// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Particles_Off)
			{
				
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Particles_On)
			{
				
			}
		
			// <===============================================================================>
		
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.PlayMovie)
			{
				
			}
			
			// <===============================================================================>
		
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Sound_Effect)
			{
				gameObject.GetComponent<OpenCloseDoor>().PlaySoundEffect(m_Objects[i]);
			}
		
			// <===============================================================================>
			
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Spawn)
			{
				
			}
		
			// <===============================================================================>
					
			else if(m_Events[i].m_ChosenEffect == m_DifferentEffects.Unlock)
			{
				gameObject.GetComponent<LockUnlock>().UnLock(m_Objects[i]);
			}

			// <===============================================================================>

			if(timer > 0)
			{	
				yield return new WaitForSeconds(timer);
			}
		}
	}

	public void GetObjects()
	{
		for(int i = 0; i < m_Events.Count();i++)
		{
			m_Objects.Add(null);
			if(m_Events[i].m_Object != null)
			{
				Debug.Log("Object with ID "+m_Events[i].m_Object.name+" Added");
				m_Objects[i] = m_Events[i].m_Object;
			}
			if(m_Objects[i] == null && m_Events[i].m_ChosenEffect != m_DifferentEffects.Delay)
			{
				Debug.Log("Item ID "+m_Events[i].m_Object+" not found");
			}
		}
	}
}

[System.Serializable]
public class EventEffect
{
	#region EffectVariables
	public GameObject				  m_Object;	    
	public m_DifferentEffects	  	  m_ChosenEffect 	= m_DifferentEffects.None;
	public bool  			  	 	  m_foldoutEffect	= false;  
	public float 					  m_DelayFloatVal	= 0;  
	public MovieTexture    		 	  m_DelayMovTxt;
	public List<Texture>	 		  m_ChangeModelTx   = new List<Texture>();
	public List<Mesh>		 		  m_ChangeModelMh   = new List<Mesh>();
	//public FMODAsset  	 		  m_SoundEffect;
	public float	 		 		  m_OpenDoor		= 0; 	   
	public float	 		 		  m_OpenDrawer		= 0;     
	public float	 		 		  m_DelayValue;     
	public MovieTexture	 		 	  m_PlayMovie;	   
	#endregion
}
