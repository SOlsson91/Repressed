using UnityEngine;
using System.Collections;

/*For the placement of GUITextures.
 * Offset is set from the sides of the screen and can be updated during runtime when placing.
 * You can also use Middle to make GUI stick to the center of the screen
Created by: Rasmus
 */

public class GUITexturePos : MonoBehaviour 
{
	#region PublicMemberVariables
	public bool  m_Update;
	public float m_InsetX;
	public float m_InsetY;
	public float m_Width;
	public float m_Height;
	public bool  m_Middle;
	public bool  m_MiddleWidth;
	public bool  m_Left;
	public bool  m_Top;
	public bool  m_BoolScale;
	public int 	 m_Target;
	#endregion
	
	#region PrivateMemberVariables
	private int     m_XOffset = 0;
	private int     m_YOffset = 0;
	private float   m_PixelInsetX;
	private float   m_PixelInsetY;
	private float   m_X;
	private float   m_Y;
	private float   m_Scale;
	private Vector2 m_OriginalSize;
	#endregion

	// Use this for initialization
	void Start () 
	{
		if (m_Width == 0) 
		{
			m_Width = guiTexture.pixelInset.width;
			m_Height = guiTexture.pixelInset.height;
		}
		m_X = m_Width;
		m_Y = m_Height;
		m_OriginalSize = new Vector2 (m_Width, m_Height);
		if(m_BoolScale)
		{
			m_Scale = (float)Screen.width / m_Target;
			m_Width = m_X * m_Scale;
			m_Height = m_Y * m_Scale;
		}
		if(!m_Middle)
		{
			MoveTextureOffset ();
		}
		else
		{
			SetToMiddle();
		}
	}

	//Moves the gui around if M_Update == true. Tanken är att man skall använda detta för utplacering
	// Update is called once per frame
	void Update () 
	{
		if(m_Update)
		{
			MoveTextureOffset ();
		}
		if(m_BoolScale)
		{
			m_Scale = (float)Screen.width / m_Target;
			m_Width = m_X * m_Scale;
			m_Height = m_Y * m_Scale;
		}
		//Debug.Log (Screen.width);
	}

	private void SetToMiddle()
	{
		m_PixelInsetX = -m_Width/2;
		m_PixelInsetY = -m_Height/2;
		GetComponent<GUITexture> ().guiTexture.pixelInset = new Rect (m_PixelInsetX, m_PixelInsetY, m_Width, m_Height);
	}

	public void ReScaleGUI(float x, float y)
	{
		m_Width = x;
		m_Height = y;
		SetToMiddle ();
	}

	public void ScaleBackOriginal()
	{
		m_Width = m_OriginalSize.x;
		m_Height = m_OriginalSize.y;
		SetToMiddle ();
	}

	//Move the texture into position depending on the screenSides
	private void MoveTextureOffset()
	{
		if(!m_MiddleWidth)
		{
			m_XOffset = Screen.width/2;
		}
		else
		{
			m_XOffset = (int)m_Width/2;
		}
		m_YOffset = Screen.height/2;

		if (m_Left) 
		{
			m_PixelInsetX = -m_XOffset + m_InsetX;
		} 
		else 
		{
			m_PixelInsetX = m_XOffset - m_InsetX - m_Width;
		}
		if (m_Top) 
		{
			m_PixelInsetY = m_YOffset - m_InsetY - m_Height;
		}
		else
		{
			m_PixelInsetY = -m_YOffset + m_InsetY;
		}
		GetComponent<GUITexture> ().guiTexture.pixelInset = new Rect (m_PixelInsetX, m_PixelInsetY, m_Width, m_Height);
	}
}
