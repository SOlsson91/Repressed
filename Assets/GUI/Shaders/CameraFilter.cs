using UnityEngine;
using System.Collections;

/*Description: Script for adding an effect to the camera for fixed time

Made by: Rasmus 30/04
Modified: Rasmus 06/05
 */

public class CameraFilter : MonoBehaviour 
{
	#region PublicMemberVariables
	public Material     m_DefaultMaterial;
	#endregion

	#region PrivateMemberVariables
	private GameObject m_ObjectShader;
	private bool    m_EffectActive = false;
	private float   m_Timer   	   = 0;
	private float   m_EffectLerp   = 0;
	private Texture m_Texture;

	private bool    m_UseRandom;
	private bool    m_FadeOut;
	private bool    m_RemoveWhite;
	private bool    m_BlackAndWhite;
	private bool    m_BlackAndWhiteEffect;
	private float   m_Duration;
	private float   m_Lerp;
	#endregion

	// Use this for initialization
	void Start () 
	{
		ResetDefualt();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_EffectActive == true)
		{
			m_Timer -= Time.deltaTime;
			if(m_Timer <= 0)
			{
				ResetDefualt();
			}
		}
	}

	public bool GetEffectActive()
	{
		return m_EffectActive;
	}

	private void ResetDefualt()
	{
		m_EffectActive = false;
		m_DefaultMaterial.SetInt("_UseRandom", 0);
		m_DefaultMaterial.SetFloat("_Lerp", 0.0f);
		m_DefaultMaterial.SetInt("_Alpha", 0);
		m_DefaultMaterial.SetInt("_BlackAndWhite", 0);
		m_DefaultMaterial.SetInt("_BlackAndWhiteEffect", 0);
	}

	public void UseEffect(GameObject effect)
	{
		m_ObjectShader 			= effect;
		m_Texture 				= m_ObjectShader.GetComponent<ShaderData> ().m_EffectTexture;
		m_EffectActive 			= true;

		m_UseRandom 			= m_ObjectShader.GetComponent<ShaderData> ().m_UseRandom;
		m_FadeOut 				= m_ObjectShader.GetComponent<ShaderData>().m_FadeOut;
		m_RemoveWhite 			= m_ObjectShader.GetComponent<ShaderData>().m_RemoveWhite;
		m_BlackAndWhite			= m_ObjectShader.GetComponent<ShaderData>().m_BlackAndWhite;
		m_BlackAndWhiteEffect   = m_ObjectShader.GetComponent<ShaderData>().m_BlackAndWhiteEffect;
		m_Duration				= m_ObjectShader.GetComponent<ShaderData>().m_Duration;
		m_Lerp					= m_ObjectShader.GetComponent<ShaderData>().m_Lerp;

		m_Timer		   			= m_Duration;
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if(m_EffectActive == true)
		{
			SetVariables();
			Graphics.Blit (source, destination, m_DefaultMaterial);
		}
		else
		{
			Graphics.Blit (source, destination);
		}
	}

	private void SetVariables()
	{
		m_DefaultMaterial.SetTexture("_SecondTex", m_Texture);
		if(m_RemoveWhite == true)
		{
			m_DefaultMaterial.SetInt("_Alpha", 1);
		}
		if(m_BlackAndWhite == true)
		{
			m_DefaultMaterial.SetInt("_BlackAndWhite", 1);
		}
		if(m_BlackAndWhiteEffect == true)
		{
			m_DefaultMaterial.SetInt("_BlackAndWhiteEffect", 1);
		}
		if(m_UseRandom == true)
		{
			m_DefaultMaterial.SetFloat("_UseRandom", 1);
			m_DefaultMaterial.SetFloat("_Random", Random.Range(0f, 1f));
			m_DefaultMaterial.SetFloat("_Random2", Random.Range(0f, 1f));
		}
		else
		{
			m_DefaultMaterial.SetFloat("_Random", 0);
			m_DefaultMaterial.SetFloat("_Random2", 0);
			m_DefaultMaterial.SetFloat("_UseRandom", 0);
		}
		GetLerp();
		m_DefaultMaterial.SetFloat("_Lerp", m_EffectLerp);
	}

	private void GetLerp()
	{
		if(m_FadeOut == true)
		{
			if(m_Timer > m_Lerp)
			{
				m_EffectLerp = m_Lerp;
			}
			else
			{
				m_EffectLerp = m_Timer;
			}
		}
		else
		{
			m_EffectLerp = m_Lerp;
		}
	}
}



















