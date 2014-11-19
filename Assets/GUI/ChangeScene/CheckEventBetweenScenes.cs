using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CheckEventBetweenScenes
{
	public static List<int> m_ListWithID = new List<int>();

	public static void AddEntry(int i)
	{
		m_ListWithID.Add(i);
	}
}
