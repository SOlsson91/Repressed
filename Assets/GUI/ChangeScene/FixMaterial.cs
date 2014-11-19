using UnityEngine;
using System.Collections;

public class FixMaterial : MonoBehaviour 
{
	public  Material m_Material;
	private bool m_Start = true;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Start)
		{
			renderer.material = m_Material;
			m_Start = false;
		}
	}
}
