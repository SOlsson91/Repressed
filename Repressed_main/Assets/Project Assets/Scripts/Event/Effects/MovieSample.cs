using UnityEngine;
using System.Collections;

public class MovieSample : MonoBehaviour 
{
	public MovieTexture m_Movie;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Movie.loop = true;
		renderer.material.mainTexture = m_Movie;
		m_Movie.Play(); 
	}
}

//Modified by Johan 14-09-15