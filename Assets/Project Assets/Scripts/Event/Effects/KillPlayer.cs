using UnityEngine;
using System.Collections;

/* Discription: Trigger component for killing the player
 * Created by: Robert 29/04-14
 * Modified by: 
 * 
 */
public class KillPlayer :  TriggerComponent
{
	public void KillThePlayer()
	{
		CharacterData.Alive = false;
		//Kalla på deathmenu som sedan sköter resten av "dödsdelen"
	}
	
	//void OnTriggerEnter(Collider other)
	//{
	//	CharacterData.Alive = false;
	//}

	override public string Name
	{ get{return"KillThePlayer";}}
}
