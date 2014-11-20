using UnityEngine;
using System.Collections;

/* Discription: Base class for triggers on objects
 * 
 * Created by: Robert 23/04-14
 * Modified by: 
 * 
 */

public abstract class TriggerComponent : MonoBehaviour
{
	public virtual string Name
	{
		get{return"";}
	}

	// Use this for initialization
	void Start()
	{
		
	}
	
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	public virtual void Init()
	{
		
	}

}
