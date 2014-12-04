using UnityEngine;
using System.Collections;

/* Discription: Trigger component for opening and closing doors to fixed angles
 * 
 * Created by: Robert 23/04-14
 * Modified by: 
 * 
 */

public class OpenCloseDoor :  TriggerComponent
{
	public void CloseDoor(Id obj)
	{
		obj.GetComponent<RDoor>().CloseDoor();
	}
	public void OpenDoor(Id obj)
	{
		obj.GetComponent<RDoor>().OpenDoor();
	}
	public void AngleDoor(Id obj, float angle, float lerp)
	{
		bool tempB = false;
		if(lerp >= 0.001)
		{
			tempB = true;
		}
		obj.GetComponent<RDoor>().ChangeDoorAngle(angle, tempB, lerp);
	}
	public void PlaySoundEffect(Id obj)
	{
		obj.GetComponent<SoundEffect>().PlaySoundEffect();
	}
	public void StopSoundEffect(Id obj)
	{
		obj.GetComponent<SoundEffect>().StopSoundEffect();
	}
	public void ChangeSoundEffect(Id obj, float par)
	{
		obj.GetComponent<SoundEffect>().ChangeSoundEffectParameter(par);
	}
	public void AddEntry(Id obj, int par)
	{
		CheckEventBetweenScenes.AddEntry(par);
	}
	public void AddRigidbody(Id obj)
	{
		obj.gameObject.AddComponent<Rigidbody>();
	}

	//public void CloseDoor(Id obj)
	//{
	//	float angle = obj.GetComponent<RotationLimit>().m_Rotation.y;
	//
	//	if(obj.gameObject.GetComponent<RotationLimit>())
	//	{
	//		angle = obj.gameObject.GetComponent<RotationLimit>().CheckRotation(-angle, "y");
	//	}
	//
	//	obj.transform.Rotate (new Vector3 (0, 1, 0), angle,Space.Self);
	//}
	//
	//public void OpenDoor(Id obj, float angle)
	//{
	//	CloseDoor (obj);
	//
	//	if(obj.gameObject.GetComponent<RotationLimit>())
	//	{
	//		angle = obj.gameObject.GetComponent<RotationLimit>().CheckRotation(angle, "y");
	//	}
	//	
	//	obj.transform.Rotate(new Vector3 (0, 1, 0),angle,Space.Self);
	//}
	
	override public string Name
	{ get{return"OpenCloseDoor";}}
}
