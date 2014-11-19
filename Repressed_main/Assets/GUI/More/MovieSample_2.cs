using UnityEngine;
using System.Collections;

public class MovieSample_ : MonoBehaviour 
{
	MovieTexture m_Movie;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		renderer.material.mainTexture = m_Movie;
		m_Movie.Play();
	}
}
