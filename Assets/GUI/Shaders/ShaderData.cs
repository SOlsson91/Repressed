using UnityEngine;
using System.Collections;

/*Description: Script for variables for the shaders

Made by: Rasmus 06/05
 */

public class ShaderData : MonoBehaviour 
{
	public Texture m_EffectTexture;

	public bool  m_UseRandom;
	public bool  m_FadeOut;
	public bool  m_RemoveWhite;
	public bool  m_BlackAndWhite;
	public bool  m_BlackAndWhiteEffect;
	public float m_Duration;
	public float m_Lerp;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameObject.activeInHierarchy)
		{
			Camera.main.GetComponent<CameraFilter>().UseEffect(this.gameObject);
			gameObject.SetActive(false);
		}
	}
}
