using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RasmusGameSave
{
	public static int m_SpawnPosition;
	public static int m_MatchCount = 0;
	//public static bool m_HoldingObject;
	//public static byte[] m_Data;
	//public static KeyValuePair<string, Vector3>[] TestPair;

	private static List<LevelSerializer.SaveEntry> m_List;

	public static void UpdateLevel()
	{
		m_List = LevelSerializer.SavedGames[LevelSerializer.PlayerName];
		LevelSerializer.SaveEntry[] tempL = m_List.ToArray();
		int tempI = -1;

		for(int i = 0; i < tempL.Length; i++) 
		{
			if(tempL[i].Name == Application.loadedLevelName)
			{
				tempI = i;
			}
		}

		if(tempI != -1)
		{
			if(m_List[tempI].Name == Application.loadedLevelName)
			{
				if(m_SpawnPosition != -1)
				{
					PlayerSpawn tempG = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpawn>();//.SpawnPlayer(m_SpawnPosition);
					if(tempG.m_Positions.Length > m_SpawnPosition)
					{
						tempG.SpawnPlayer(m_SpawnPosition);
					}
				}
				//Debug.Log("Kommer hit " + m_SpawnPosition);
				LevelSerializer.LoadNow(m_List[tempI].Data);
			}
		}
	}

	public static void ClearSaves()
	{
		LevelSerializer.SavedGames[LevelSerializer.PlayerName].Clear();
	}

	public static void SaveLevel()
	{
		m_List = LevelSerializer.SavedGames[LevelSerializer.PlayerName];
		LevelSerializer.SaveEntry[] tempL = m_List.ToArray();
		int tempI = -1;
		
		for(int i = 0; i < tempL.Length; i++)
		{
			if(tempL[i].Name == Application.loadedLevelName)
			{
				tempI = i;
			}
		}
		
		if(tempI != -1)
		{
			LevelSerializer.SavedGames[LevelSerializer.PlayerName].Remove(LevelSerializer.SavedGames[LevelSerializer.PlayerName][tempI]);
		}

		LevelSerializer.SaveGame(Application.loadedLevelName);
	}
}
