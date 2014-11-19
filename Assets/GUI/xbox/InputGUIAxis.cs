using UnityEngine;
using System.Collections;

public class InputGUIAxis : MonoBehaviour 
{
	private string m_Text;
	// Use this for initialization
	void Start () 
	{
		m_Text = "0";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis("Test1") > 0.5)
		{
			m_Text = "a1";
		}
		if(Input.GetAxis("Test1") < -0.5)
		{
			m_Text = "a2";
		}
		if(Input.GetAxis("Test2") > 0.5)
		{
			m_Text = "b1";
		}
		if(Input.GetAxis("Test2") < -0.5)
		{
			m_Text = "b2";
		}
		if(Input.GetAxis("Test3") > 0.5)
		{
			m_Text = "c1";
		}
		if(Input.GetAxis("Test3") < -0.5)
		{
			m_Text = "c2";
		}
		if(Input.GetAxis("Test4") > 0.5)
		{
			m_Text = "d1";
		}
		if(Input.GetAxis("Test4") < -0.5)
		{
			m_Text = "d2";
		}

		if(Input.GetAxis("Test5") > 0.5)
		{
			m_Text = "e1";
		}
		if(Input.GetAxis("Test5") < -0.5)
		{
			m_Text = "e2";
		}if(Input.GetAxis("Test6") > 0.5)
		{
			m_Text = "f1";
		}
		if(Input.GetAxis("Test6") < -0.5)
		{
			m_Text = "f2";
		}if(Input.GetAxis("Test7") > 0.5)
		{
			m_Text = "g1";
		}
		if(Input.GetAxis("Test7") < -0.5)
		{
			m_Text = "g2";
		}

		GetComponent<GUIText> ().text = m_Text;
	}
}
