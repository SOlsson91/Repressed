using UnityEngine;
using System.Collections;

public class InputGUITexture : MonoBehaviour 
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
		Debug.Log (Input.inputString);
		m_Text += Input.inputString;
		if(Input.GetButtonDown ("Butt1"))
		{
			m_Text += "0";
		}
		if(Input.GetButtonDown ("Butt2"))
		{
			m_Text += "1";
		}
		if(Input.GetButtonDown ("Butt3"))
		{
			m_Text += "2";
		}
		if(Input.GetButtonDown ("Butt4"))
		{
			m_Text += "3";
		}
		if(Input.GetButtonDown ("Butt5"))
		{
			m_Text += "4";
		}
		if(Input.GetButtonDown ("Butt6"))
		{
			m_Text += "5";
		}
		if(Input.GetButtonDown ("Butt7"))
		{
			m_Text += "6";
		}
		if(Input.GetButtonDown ("Butt8"))
		{
			m_Text += "7";
		}

		GetComponent<GUIText> ().text = m_Text;

	}
}
