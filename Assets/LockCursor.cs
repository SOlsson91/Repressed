using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour 
{

	private static bool lockCursor = true; 


	void Start () 
	{
		Screen.lockCursor = true;
	}
	


	void Update()
	{
		if (Screen.lockCursor != lockCursor)
		{
			if (lockCursor && Input.GetMouseButton(0))
			{
				Screen.lockCursor = true;
			}
			else if (!lockCursor)
			{
				Screen.lockCursor = false;
			}
		}
	}
}

