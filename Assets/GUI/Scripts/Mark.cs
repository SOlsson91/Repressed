using UnityEngine;
using System.Collections;

/*Class for marking one object in inventory when combining or swapping items
 * 
Created by: Rasmus
 */

public class Mark : MonoBehaviour 
{
	#region PublicMemberVariables
	public Texture m_SwapTexture;
	public Texture m_CombineTexture;
	#endregion
	
	#region PrivateMemberVariables
	#endregion
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//Which texture to show and at what position
	public void ChangeMarkBox(bool swap, Vector3 pos)
	{
		if(swap)
		{
			renderer.material.mainTexture = m_SwapTexture;
		}
		else
		{
			renderer.material.mainTexture = m_CombineTexture;
		}
		renderer.enabled = true;
		Vector3 offset = new Vector3 (0, 0, -0.1f);
		transform.position = pos - offset;
	}

	public void ExitMarkBox()
	{
		renderer.enabled = false;
	}
}
