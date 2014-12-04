using UnityEngine;
using System.Collections;

public class ChangeLightSource : MonoBehaviour 
{
	public Texture m_MatchesTexture;
	public Texture m_FlashlightTexture;

	// Use this for initialization
	void Start () 
	{
		ChooseMatches ();
	}
			
	// Update is called once per frame
	void Update () 
	{
		if(renderer.material.mainTexture == m_FlashlightTexture)
			GetComponentInChildren<TextMesh> ().text = "";
		else
			GetComponentInChildren<TextMesh> ().text = RasmusGameSave.m_MatchCount.ToString();
	}

	public void ChooseFlashLight()
	{
		renderer.material.mainTexture = m_FlashlightTexture;
		GetComponentInChildren<TextMesh> ().text = "";
	}
	public void ChooseMatches()
	{
		renderer.material.mainTexture = m_MatchesTexture;
		GetComponentInChildren<TextMesh> ().text = RasmusGameSave.m_MatchCount.ToString();
	}
}