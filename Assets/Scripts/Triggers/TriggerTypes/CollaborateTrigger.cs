using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/* Discription: ObjectComponent class for interactions between two specific objects
 * 
 * Created by: Robert Datum: 08/04/14
 * Modified by: Jimmy Datum 14/04/14
 * Modified by: Sebastian Datum 23/04/14: Changed the raycast to cast from the camera, and changed GetButtonDown to GetButtonUp
 * Modified by: Jon Wahlstrom 2014-04-29 "added functionallity for collaborate hover effect"
 * Modified by: Rasmus 04/08/14 Took away everything so we can use it with the new system
 */

public class CollaborateTrigger : ObjectComponent
{
	#region PublicMemberVariables
	private List<int>	m_ValidId = new List<int>();
	private string		m_Input;
	#endregion

	void Start()
	{
		SuperTrigger[] triggerArray;
		triggerArray = gameObject.GetComponents<SuperTrigger>();
		foreach(SuperTrigger c in triggerArray)
		{
			if(c.m_CollaborateSelf){
				m_ValidId = c.m_IDsCollaborate;
				m_Input = c.m_CollaborateInput;
			}
		}
	}

	public List<int> GetValidIds()
	{
		return m_ValidId;
	}
}
