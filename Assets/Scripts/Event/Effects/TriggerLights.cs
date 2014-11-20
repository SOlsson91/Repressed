using UnityEngine;
using System.Collections;

/* Discription: TriggerLights
 * Used to toggel lights on or off
 * 
 * Created by: Sebastian Olsson 24-04-14
 * Modified by:
 */

public class TriggerLights : TriggerComponent 
{
	override public string Name
	{
		get{ return "LightSwitch"; }
	}

	public void LightSwitch(Id obj)
	{
		obj.GetComponent<Light> ().enabled = !obj.GetComponent<Light> ().enabled;
	}

	public void LightOn(Id obj)
	{
		obj.GetComponent<Light> ().enabled = true;
	}

	public  void LightOff(Id obj)
	{
		obj.GetComponent<Light> ().enabled = false;
	}
}
