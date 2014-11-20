using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/* Discription: Trigger component for changling the mesh and texture on the said object
 * 
 * Created by: Robert 23/04-14
 * Modified by: 
 * 
 */

public class ChangeModel :  TriggerComponent
{
	#region PrivateMemberVariables
	private int				m_Counter	 	= 0;
	private	List<Mesh>		m_MeshStages 	= new List<Mesh>();
	private	List<Texture>	m_TextureStages = new List<Texture>();
	#endregion

	public void SetStages(List<Mesh> meshes, List<Texture> textures)
	{
		m_MeshStages 	= meshes;
		m_TextureStages = textures;
	}

	// Update is called once per frame
	public void ModelChange(Id obj)
	{
		if (m_Counter < m_MeshStages.Count()) 
		{
			if(m_MeshStages[m_Counter] != null)
			{
				obj.gameObject.GetComponent<MeshFilter> ().mesh = m_MeshStages [m_Counter];
			}
		}
		if (m_Counter < m_TextureStages.Count())
		{
			if(m_TextureStages[m_Counter] != null)
			{
				obj.renderer.material.mainTexture = m_TextureStages [m_Counter];
			}
		}
		m_Counter++;	
	}

	override public string Name
	{ get{return"ChangeModel";}}
}
