using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour 
{
	public Texture[] m_HoverTextures;

	private GUITexture m_Cursor;

	// Use this for initialization
	void Start () 
	{
		m_Cursor = GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SetCursor(int whichTexture)
	{
		m_Cursor.texture = m_HoverTextures[whichTexture];
	}

	public void ResetCursor()
	{
		m_Cursor.texture = m_HoverTextures[0];
		GetComponentInChildren<GUIText> ().text = "";
	}
}
