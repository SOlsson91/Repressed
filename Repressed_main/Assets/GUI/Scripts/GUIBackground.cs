using UnityEngine;
using System.Collections;



public class GUIBackground : MonoBehaviour 
{
	#region PublicMemberVariables

	#endregion

	#region PrivateMemberVariables
	
	#endregion

	// Use this for initialization
	void Start () 
	{
		Vector3 size = new Vector3 ();
		size.x = Screen.width;
		size.y = 1;
		size.z = Screen.height;
		gameObject.transform.localScale = size;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 size = new Vector3 ();
		size.x = (float) Screen.width / 500;
		size.y = 1;
		size.z = (float )Screen.height / 500;
		gameObject.transform.localScale = size;
	}
}
