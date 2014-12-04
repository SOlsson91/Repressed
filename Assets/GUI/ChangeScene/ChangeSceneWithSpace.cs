using UnityEngine;
using System.Collections;

public class ChangeSceneWithSpace : MonoBehaviour 
{
	public string m_Level;
	public GameObject m_ToDestroy;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space"))
		{
			Destroy (m_ToDestroy);
			Application.LoadLevel(m_Level);
		}
	}
}
