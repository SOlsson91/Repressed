using UnityEngine;
using System.Collections;

/*Class for Locking/unlocking

Made By: Rasmus 08/04
 */

public class Locked : ObjectComponent 
{
	#region PublicMemberVariables
	public bool m_LockedFromStart = true;
	#endregion
	
	#region PrivateMemberVariables
	private bool m_Locked;
	#endregion

	override public string Name
	{ get{return"Locked";}}

	// Use this for initialization
	void Start() 
	{
		if(m_LockedFromStart)
		{
			m_Locked = true;
		}
		else
		{
			m_Locked = false;
		}
	}

	public void Lock()
	{
		m_Locked = true;
	}

	public void UnLock()
	{
		m_Locked = false;
	}

	public bool GetLocked()
	{
		return m_Locked;
	}
}
