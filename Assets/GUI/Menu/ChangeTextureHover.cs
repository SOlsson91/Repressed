using UnityEngine;
using System.Collections;

public class ChangeTextureHover : MonoBehaviour 
{
	public Texture m_Texture;
	public Texture m_OriginalTexture;

	void Start()
	{
		//m_OriginalTexture = GetComponent<GUITexture>().texture;
	}

	public void ChangeToHoverTexture()
	{
		GetComponent<GUITexture>().texture = m_Texture;
	}

	public void ChangeBack()
	{
		GetComponent<GUITexture>().texture = m_OriginalTexture;
	}
}
