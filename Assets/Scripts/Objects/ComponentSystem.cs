using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum m_DifferentComponents
{
	Revolvable,
	Drawer,
	Inventory_Throw,
	Inventory_NoThrow,
	Throw_Inspect,
	Throw_NoInspect,
	Static_Inspect,
	Static
}


public class ComponentSystem : MonoBehaviour 
{
	#region All
	public int 		 			 m_ID;
	public Texture2D 			 m_HoverEffect;
	public m_DifferentComponents m_ChosenComponent;
	#endregion

	#region Casebased
	//public FMODAsset m_InspectSound;
	#endregion
	
	void Start()
	{
		gameObject.AddComponent ("Id");
		gameObject.AddComponent ("HoverEffect");

		if(m_ChosenComponent == m_DifferentComponents.Drawer)
		{

		}

		if(m_ChosenComponent == m_DifferentComponents.Inventory_NoThrow)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Inventory_Throw)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Revolvable)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Static)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Static_Inspect)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Throw_Inspect)
		{
			
		}

		if(m_ChosenComponent == m_DifferentComponents.Throw_NoInspect)
		{
			
		}
	}

	public void ActivateTrigger()
	{

	}
}