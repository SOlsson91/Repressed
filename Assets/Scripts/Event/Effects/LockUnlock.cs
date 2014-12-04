using UnityEngine;
using System.Collections;

/* Discription: Trigger components for locking or unlocking the Object
 * Created by: Robert 23/04-14
 * Modified by: 
 * 
 */
public class LockUnlock :  TriggerComponent
{
	public void Lock(Id obj)
	{
		obj.gameObject.GetComponent<Locked>().Lock();
	}
	public void UnLock(Id obj)
	{
		obj.gameObject.GetComponent<Locked>().UnLock();
	}
	
	override public string Name
	{ get{return"LockUnlock";}}
}
