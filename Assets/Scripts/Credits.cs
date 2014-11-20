using UnityEngine;
using System.Collections;

//Made by Johan Ahlgren 2014-08-10
//Wohoo!

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		Vector3 position = this.transform.position;
		if ( position.y <= 0)
		{
			position.y = position.y+0.0003f;
			this.transform.position = position;
		}


		if (Input.GetKey(KeyCode.UpArrow) && position.y <= (0))
		{
			Vector3 position1 = this.transform.position;
			position.y = position.y+0.1f;
			this.transform.position = position;
		}

		if (Input.GetKey(KeyCode.DownArrow) && position.y >= (-7))
		{
			Vector3 position1 = this.transform.position;
			position.y = position.y-0.1f;
			this.transform.position = position;
		}

		if (Input.GetKey(KeyCode.W) && position.y <= (0))
		{
			Vector3 position1 = this.transform.position;
			position.y = position.y+0.1f;
			this.transform.position = position;
		}
		
		if (Input.GetKey(KeyCode.S) && position.y >= (-7))
		{
			Vector3 position1 = this.transform.position;
			position.y = position.y-0.1f;
			this.transform.position = position;
		}


	}
}
