using UnityEngine;
using System.Collections;

public class RoundRound : MonoBehaviour 
{
	public float m_Speed;

	private Vector3 m_RotationVector;
	// Use this for initialization
	void Start () 
	{
		m_RotationVector = new Vector3 ();
		m_RotationVector.y = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (m_RotationVector * m_Speed);
	}
}
