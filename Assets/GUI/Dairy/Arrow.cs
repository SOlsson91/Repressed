using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{
	#region PublicMemberVariables
	public Texture m_TextureActive;
	public Texture m_TextureIdle;
	public bool    m_Left;
	#endregion
	
	#region PrivateMemberVariables
	private GUITexture  m_Parent;
	private GUITexture  m_Texture;
	private float 		m_PixelInsetX;
	private float 		m_PixelInsetY;
	private float 		m_Width;
	private float 		m_Height;
	#endregion

	// Use this for initialization
	void Start () 
	{
		m_Parent  = transform.parent.GetComponent<GUITexture> ();
		m_Texture = GetComponent<GUITexture> ();
		m_Width   = m_Texture.GetScreenRect ().width;
		m_Height  = m_Texture.GetScreenRect ().height;
		SetPosition ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Parent.enabled != m_Texture.enabled)
		{
			m_Texture.enabled = m_Parent.enabled;
			SetPosition();
		}
	}

	public void SetLeft()
	{
		if(m_Left)
		{
			m_Texture.texture = m_TextureIdle;
		}
		else
		{
			m_Texture.texture = m_TextureActive;
		}
	}
	public void SetMiddle()
	{
		m_Texture.texture = m_TextureActive;
	}
	public void SetRight()
	{
		if(!m_Left)
		{
			m_Texture.texture = m_TextureIdle;
		}
		else
		{
			m_Texture.texture = m_TextureActive;
		}
	}

	private void SetPosition()
	{
		if(m_Left)
		{
			m_PixelInsetX = -m_Parent.GetScreenRect().width/2;
		}
		else
		{
			m_PixelInsetX = m_Parent.GetScreenRect().width/2 - m_Width;
		}
		m_PixelInsetY = -m_Height/2;
		GetComponent<GUITexture> ().guiTexture.pixelInset = new Rect (m_PixelInsetX, m_PixelInsetY, m_Width, m_Height);
		Vector3 tempVec = new Vector3 ();
		tempVec.z = 10;
		m_Texture.transform.position = tempVec;
	}
}
