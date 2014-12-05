using UnityEngine;
using System.Collections;

public class GUITextPos : MonoBehaviour 
{
	public bool m_PositionFromParent;
	public int  m_InsetX;
	public int  m_InsetY;
	public bool m_Top;
	public bool m_Left;
	public bool m_Update;
	public bool m_Middle;
	//public int  m_Target = 1;

	private int m_PixelInsetX;
	private int m_PixelInsetY;
	private int m_XOffset;
	private int m_YOffset;
	private int m_Width;
	private int m_Height;
	private Vector2 m_ParentCenter;
	
	// Use this for initialization
	void Start () 
	{
		if(m_PositionFromParent)
		{
			m_XOffset = (int)transform.parent.GetComponent<GUITexture>().pixelInset.x;
			m_YOffset = -(int)transform.parent.GetComponent<GUITexture>().pixelInset.y;
			m_ParentCenter = new Vector2();
			m_ParentCenter.x = transform.parent.GetComponent<GUITexture>().pixelInset.x - (transform.parent.GetComponent<GUITexture>().pixelInset.width/2);
			m_ParentCenter.y = transform.parent.GetComponent<GUITexture>().pixelInset.y + (transform.parent.GetComponent<GUITexture>().pixelInset.height/2);
		}
		else
		{
			m_XOffset = Screen.width/2;
			m_YOffset = Screen.height/2;
		}

		if(m_PositionFromParent)
		{
			MoveToPositionParent();
		}
		else
		{
			MoveToPosition ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Update)
		{
			if(m_PositionFromParent)
			{
				MoveToPositionParent();
			}
			else
			{
				MoveToPosition ();
			}
			//GetComponent<GUIText>().fontSize = (int)transform.parent.GetComponent<GUITexture>().GetScreenRect().width / m_Target;
		}
	}

	private void MoveToPosition()
	{
		m_Width  = (int)GetComponent<GUIText>().GetScreenRect().width;
		m_Height = (int)GetComponent<GUIText>().GetScreenRect().height;

		if (m_Left) 
		{
			m_PixelInsetX = -m_XOffset;
		} 
		else 
		{
			m_PixelInsetX = m_XOffset - m_Width;
		}
		if (m_Top) 
		{
			m_PixelInsetY = m_YOffset;
		}
		else
		{
			m_PixelInsetY = -m_YOffset + m_Height; 
		}

		GetComponent<GUIText> ().pixelOffset = new Vector2 (m_PixelInsetX, m_PixelInsetY);
	}

	private void MoveToPositionParent()
	{
		m_ParentCenter.x = transform.parent.GetComponent<GUITexture>().pixelInset.x + (transform.parent.GetComponent<GUITexture>().GetScreenRect().width/2);
		m_ParentCenter.y = transform.parent.GetComponent<GUITexture>().pixelInset.y + (transform.parent.GetComponent<GUITexture>().GetScreenRect().height/2);

		m_Width  = (int)GetComponent<GUIText>().GetScreenRect().width;
		m_Height = (int)GetComponent<GUIText>().GetScreenRect().height;

		m_PixelInsetX = (int)m_ParentCenter.x;
		m_PixelInsetY = (int)m_ParentCenter.y;

		m_XOffset = (int)transform.parent.GetComponent<GUITexture>().GetScreenRect().width/2;
		m_YOffset = (int)transform.parent.GetComponent<GUITexture>().GetScreenRect().height/2;

		if(m_Middle)
		{
			m_PixelInsetX += -(int)GetComponent<GUIText>().GetScreenRect().width/2;
		}
		else
		{
			if (m_Left) 
			{
				m_PixelInsetX += -m_XOffset + m_InsetX;
			} 
			else 
			{
				m_PixelInsetX += m_XOffset - m_Width - m_InsetX;
			}
		}
		if (m_Top) 
		{
			m_PixelInsetY += m_YOffset - m_InsetY;
		}
		else
		{
			m_PixelInsetY += -m_YOffset + m_Height + m_InsetY; 
		}

		
		GetComponent<GUIText> ().pixelOffset = new Vector2 (m_PixelInsetX, m_PixelInsetY);
	}
}
