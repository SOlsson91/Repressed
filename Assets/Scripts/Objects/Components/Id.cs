using UnityEngine;
using System.Collections;

/* Discription: Objectcomponent class that sets an object ID
 * 
 * Created by: Robert Datum: 08/04-14
 * Modified by:
 * 
 */

public class Id : ObjectComponent
{
	#region PublicMemberVariables
	public int	m_Id	= 0;
	#endregion

	public int ObjectId
	{
		get { return m_Id; }
	}
}
