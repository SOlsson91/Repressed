using UnityEngine;
using System.Collections;
public static class ExchangeMaterialExtension
{
	public static void ExchangeMaterial(this GameObject go, Color color)
	{
		if(go.GetComponent<MeshRenderer>())
		{               
			Material[] materials = go.gameObject.renderer.materials;
			
			foreach(Material m in materials)
			{
				m.SetColor("_Color", color);
			}   
		}
	}
}