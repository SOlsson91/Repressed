
using UnityEngine;
using System.Collections;


public class Rotate : MonoBehaviour
{

	private  float Rotation = 10f;
	private  bool RotateLeft = true;
	private  float RotationSpeed = 2f;
	private  float RotationLimit = 7f;
	// Use this for initialization
	void Start () 
	{
	
	}


	void Update()
	{

		if (RotateLeft) 
		{
			if(Rotation < RotationLimit)
			{
				Rotation += RotationSpeed * Time.deltaTime;

			}
			else
			{
				RotateLeft = false;
			}
		}
		else
		{
			if(Rotation > -RotationLimit)
			{
				Rotation -= RotationSpeed * Time.deltaTime;

			}
			else
			{
				RotateLeft = true;


			}
		}
		transform.eulerAngles = new Vector3 (Rotation, 0,0);



	}
	
	// Update is called once per frame

}
