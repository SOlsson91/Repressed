using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SelectCode : MonoBehaviour 
{
	#region PublicMemberVariables
	#endregion
	
	#region PrivateMemberVariables
	private GameObject  m_Parent;
	private GUITexture  m_Texture;
	private float 	  	m_WidthOffset;
	private float 	  	m_HeightOffset;
	private float 	  	m_Width;
	private float 	  	m_Height;
	private GUIText 	m_Number;
	#endregion
	// Use this for initialization
	void Start () 
	{
		m_Parent = transform.parent.gameObject;
		m_Texture = GetComponent<GUITexture> ();
		m_Number = m_Parent.GetComponentInChildren<GUIText> ();
		m_HeightOffset = 0;
		SetPosition (0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Parent.GetComponent<GUITexture>().enabled != m_Texture.enabled)
		{
			m_Texture.enabled = m_Parent.GetComponent<GUITexture>().enabled;
		}
	}

	public void SetPosition(int column)
	{
		m_Width 		= m_Texture.GetScreenRect ().width;
		m_Height 		= m_Texture.GetScreenRect ().height;
		m_HeightOffset  = -m_Height;
		m_WidthOffset   = -m_Parent.GetComponent<GUITexture>().GetScreenRect().width/2; 
		m_WidthOffset 	-= m_Number.GetScreenRect ().width / 2;
		m_WidthOffset 	+= 100 + 100*column;
		m_Texture.pixelInset = new Rect (m_WidthOffset, m_HeightOffset, m_Width, m_Height);
	}
}
