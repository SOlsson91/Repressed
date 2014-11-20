using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor (typeof(EventSystem))]
public class EventSystemEditor : Editor
{
	private bool 			 m_SetUp		  = false;
	private GUIStyle 		 m_Style;
	private GUIStyle 		 m_ObjectLable;
	private EventSystem 	 ES;
	private GUISkin          m_Skin;
	private int 			 m_TextHeight 	  = 20;
	private int  			 m_BoxHeight  	  = 15;
	private int				 m_RowHeight	  = 20;
	private int 		 	 m_CloseHeight    = 13;
	
	// <---------------------------------------------------------------------------------------->
	
	public override void OnInspectorGUI()
	{
		SetUp();

		if(ES.m_Description == "")
		{
			ES.m_EventToggle = NGUIEditorTools.DrawHeader("Event: "+ES.m_ID,ES.m_EventToggle);
		}

		else
		{
			ES.m_EventToggle = NGUIEditorTools.DrawHeader(ES.m_Description+" ("+ES.m_ID+")",ES.m_EventToggle);
		}

		if(ES.m_EventToggle)
		{
			NGUIEditorTools.BeginContents ();
			EditorGUI.indentLevel++;
			Top ();
			EditorGUI.indentLevel++;
			Effects ();
			NGUIEditorTools.EndContents ();
		}
	
	}

	// <------------------------------STARTUP-SETTINGS--------------------------------------------------------->

	public void SetUp()
	{	
		if(!m_SetUp)
		{
			ES = (EventSystem)target;
			Color myColor = Color.blue;
			
			m_Skin = Resources.Load ("GUI Skins/Buttons") as GUISkin;
			m_Style = new GUIStyle(EditorStyles.foldout);
			m_Style.normal.textColor = myColor;
			m_Style.onNormal.textColor = myColor;
			m_Style.hover.textColor = myColor;
			m_Style.onHover.textColor = myColor;
			m_Style.focused.textColor = myColor;
			m_Style.onFocused.textColor = myColor;
			m_Style.active.textColor = myColor;
			m_Style.onActive.textColor = myColor;
			//m_Style.fontStyle = FontStyle.Bold;
			m_Style.fontSize = 11;
			
			m_ObjectLable = new GUIStyle (EditorStyles.label);
			m_ObjectLable.normal.textColor = Color.magenta;

			m_SetUp = true;
		}
	}

	// <----------------------------TOP-DRAW-IN-THE-INSPECTOR----------------------------------------------------------->

	public void Top()
	{
		GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
		EditorGUILayout.LabelField ("ID", GUILayout.Height (m_TextHeight), GUILayout.Width (103));	
		ES.m_ID = EditorGUILayout.IntField(ES.m_ID, GUILayout.Height (m_BoxHeight),GUILayout.Width (60));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
		EditorGUILayout.LabelField ("Description", GUILayout.Height (m_TextHeight), GUILayout.Width (103));	
		ES.m_Description = EditorGUILayout.TextField(ES.m_Description, GUILayout.Height (m_BoxHeight),GUILayout.Width (260));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
		EditorGUILayout.LabelField ("Effects: " + ES.m_Events.Count(), GUILayout.Height (m_TextHeight), GUILayout.Width (116));
		if (GUILayout.Button("Add Effect", GUILayout.Height (m_BoxHeight), GUILayout.Width (200)))
		{
			ES.m_Events.Add(new EventEffect());
		}
		GUILayout.EndHorizontal();
	}

	// <-------------------------------------EFFECTS--------------------------------------------------->

	public void Effects()
	{
		for(int i = 0; i < ES.m_Events.Count(); i++)
		{
			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			if(ES.m_Events[i].m_ChosenEffect == m_DifferentEffects.None)
			{
				ES.m_Events[i].m_foldoutEffect = NGUIEditorTools.DrawHeader((i+1)+". Effect: "+(i+1)+"    Object:  "+ES.m_Events[i].m_ObjectID,ES.m_Events[i].m_foldoutEffect);
			}
			else if(ES.m_Events[i].m_ChosenEffect == m_DifferentEffects.Delay)
			{
				ES.m_Events[i].m_foldoutEffect = NGUIEditorTools.DrawHeader((i+1)+". "+ES.m_Events[i].m_DelayValue+" Second "+ES.m_Events[i].m_ChosenEffect.ToString(),ES.m_Events[i].m_foldoutEffect);
			}
			else
			{
				ES.m_Events[i].m_foldoutEffect = NGUIEditorTools.DrawHeader((i+1)+". "+ES.m_Events[i].m_ChosenEffect.ToString()+"    Object:  "+ES.m_Events[i].m_ObjectID,ES.m_Events[i].m_foldoutEffect);
			}
			GUI.skin = m_Skin;
			if (GUILayout.Button("X",GUILayout.Height (m_CloseHeight), GUILayout.Width (20)))
			{																																								
				ES.m_Events.RemoveAt(i);
				GUILayout.EndHorizontal();
				break;
			}
			GUI.skin = null;

			GUILayout.EndHorizontal();

			if(ES.m_Events[i].m_foldoutEffect)
			{
				NGUIEditorTools.BeginContents();
				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				ES.m_Events[i].m_ChosenEffect = (m_DifferentEffects)EditorGUILayout.EnumPopup(ES.m_Events[i].m_ChosenEffect,GUILayout.Width(130));
				GUILayout.Space(-20);
				if(ES.m_Events[i].m_ChosenEffect != m_DifferentEffects.Delay)
				{
					EditorGUILayout.LabelField ("Object ID", GUILayout.Height (m_TextHeight), GUILayout.Width (90));
					GUILayout.Space(-20);					
					ES.m_Events[i].m_ObjectID = EditorGUILayout.IntField(ES.m_Events[i].m_ObjectID,GUILayout.Height (m_BoxHeight),GUILayout.Width(70));
				}
				else
				{
					GUILayout.Space(145);
					ES.m_Events[i].m_ObjectID = 0;
				}
				if(i != 0){
					if (GUILayout.Button("Up", GUILayout.Height (m_BoxHeight), GUILayout.Width (50)))
					{
						MoveUp(i);
					}
				}
				else
				{
					GUILayout.Space(53);
				}
				if(i != (ES.m_Events.Count()-1))
				{
					if (GUILayout.Button("Down", GUILayout.Height (m_BoxHeight), GUILayout.Width (50)))
					{
						MoveDown(i);
					}
				}


				GUILayout.EndHorizontal();
				EffectCases (i);
				NGUIEditorTools.EndContents();
			}
		}
	}
	// <------------------------------------EFFECTCASES------------------------------------------>

	public void EffectCases(int Effected)
	{
		if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Active_No)
		{
			EditorGUILayout.LabelField ("Deactivates the object", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}
		
		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Active_Yes)
		{
			EditorGUILayout.LabelField ("Activates the object", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Change_Model)
		{
			EditorGUILayout.LabelField ("Changes the model of the object", GUILayout.Height (m_TextHeight), GUILayout.Width (263));
			int capacity = EditorGUILayout.IntField("No. of models",ES.m_Events[Effected].m_ChangeModelMh.Capacity, GUILayout.Height (m_BoxHeight),GUILayout.Width (182));
			
			if(capacity != ES.m_Events[Effected].m_ChangeModelTx.Count())
			{
				ES.m_Events[Effected].m_ChangeModelTx.Clear();
				ES.m_Events[Effected].m_ChangeModelTx.TrimExcess();
				ES.m_Events[Effected].m_ChangeModelTx.Capacity = capacity;
									
				ES.m_Events[Effected].m_ChangeModelMh.Clear();
				ES.m_Events[Effected].m_ChangeModelMh.TrimExcess();
				ES.m_Events[Effected].m_ChangeModelMh.Capacity = capacity;

				for(int i = 0; i < ES.m_Events[Effected].m_ChangeModelMh.Capacity; i++)
				{		
					ES.m_Events[Effected].m_ChangeModelTx.Add(null);
					ES.m_Events[Effected].m_ChangeModelMh.Add(null);
				}
			}
			for(int i = 0; i < ES.m_Events[Effected].m_ChangeModelTx.Count(); i++)
			{
				EditorGUILayout.LabelField ("Model: "+(i+1), GUILayout.Height (m_TextHeight), GUILayout.Width (263));
				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				ES.m_Events[Effected].m_ChangeModelMh[i] = (Mesh)EditorGUILayout.ObjectField(ES.m_Events[Effected].m_ChangeModelMh[i],typeof(Mesh),false,GUILayout.Height (m_BoxHeight), GUILayout.Width (160));
				ES.m_Events[Effected].m_ChangeModelTx[i] = (Texture)EditorGUILayout.ObjectField(ES.m_Events[Effected].m_ChangeModelTx[i],typeof(Texture),false,GUILayout.Height (m_BoxHeight), GUILayout.Width (160));
				GUILayout.EndHorizontal();
			}
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Delay)
		{
			EditorGUILayout.LabelField ("Add a delay between effects", GUILayout.Height (m_TextHeight), GUILayout.Width (203));

			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			GUILayout.Space(5);
			EditorGUILayout.LabelField ("Float value", GUILayout.Height (m_TextHeight), GUILayout.Width (103));	
			ES.m_Events[Effected].m_DelayFloatVal = EditorGUILayout.FloatField(ES.m_Events[Effected].m_DelayFloatVal, GUILayout.Height (m_BoxHeight),GUILayout.Width (60));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			GUILayout.Space(5);
			EditorGUILayout.LabelField ("Float value", GUILayout.Height (m_TextHeight), GUILayout.Width (103));	
			ES.m_Events[Effected].m_DelayMovTxt = (MovieTexture)EditorGUILayout.ObjectField(ES.m_Events[Effected].m_DelayMovTxt,typeof(MovieTexture),false,GUILayout.Height (m_BoxHeight), GUILayout.Width (160));
			GUILayout.EndHorizontal();

			if(ES.m_Events[Effected].m_DelayMovTxt != null)
			{
				ES.m_Events[Effected].m_DelayValue =ES.m_Events[Effected].m_DelayMovTxt.duration + ES.m_Events[Effected].m_DelayFloatVal;
			}
			else
			{
				ES.m_Events[Effected].m_DelayValue = ES.m_Events[Effected].m_DelayFloatVal;
			}

		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Door_Close)
		{
			EditorGUILayout.LabelField ("Closes the door", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Door_Open)
		{
			EditorGUILayout.LabelField ("Opens the door to set angle", GUILayout.Height (m_TextHeight), GUILayout.Width (253));
			
			GUILayout.BeginHorizontal(GUILayout.Height(20));
			GUILayout.Space(5);
			EditorGUILayout.LabelField ("Angle", GUILayout.Height (m_TextHeight), GUILayout.Width (123));
			ES.m_Events[Effected].m_OpenDoor = EditorGUILayout.FloatField(ES.m_Events[Effected].m_OpenDoor, GUILayout.Height (m_BoxHeight),GUILayout.Width (90));
			GUILayout.EndHorizontal();
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Door_Angle)
		{
			EditorGUILayout.LabelField ("Opens the door to set angle", GUILayout.Height (m_TextHeight), GUILayout.Width (253));
			
			GUILayout.BeginHorizontal(GUILayout.Height(20));
			GUILayout.Space(5);
			EditorGUILayout.LabelField ("Angle", GUILayout.Height (m_TextHeight), GUILayout.Width (123));
			ES.m_Events[Effected].m_OpenDoor = EditorGUILayout.FloatField(ES.m_Events[Effected].m_OpenDoor, GUILayout.Height (m_BoxHeight),GUILayout.Width (90));
			GUILayout.EndHorizontal();
		}
		
		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Drawer_Close)
		{
			EditorGUILayout.LabelField ("Closes the drawer", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Drawer_Open)
		{
			EditorGUILayout.LabelField ("Opens the drawer to set position", GUILayout.Height (m_TextHeight), GUILayout.Width (253));
			
			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			GUILayout.Space(5);
			EditorGUILayout.LabelField ("OpenPosition", GUILayout.Height (m_TextHeight), GUILayout.Width (123));
			ES.m_Events[Effected].m_OpenDrawer = EditorGUILayout.FloatField(ES.m_Events[Effected].m_OpenDrawer, GUILayout.Height (m_BoxHeight),GUILayout.Width (90));
			GUILayout.EndHorizontal();
		}
		
		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Kill)
		{
			EditorGUILayout.LabelField ("Kills the player", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Lights_Off)
		{
			EditorGUILayout.LabelField ("Deactivates the lightsource", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Lights_On)
		{
			EditorGUILayout.LabelField ("Activates the lightsource", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Lights_Toggle)
		{
			EditorGUILayout.LabelField ("Toggles the lightsource", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}
		
		// <===============================================================================>

		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Lock)
		{
			EditorGUILayout.LabelField ("Locks the object", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Particles_Off)
		{
			
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Particles_On)
		{
			
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.PlayMovie)
		{
			EditorGUILayout.LabelField ("Starts a movie on object", GUILayout.Height (m_TextHeight), GUILayout.Width (203));

			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			GUILayout.Space(5);
			ES.m_Events[Effected].m_PlayMovie = (MovieTexture)EditorGUILayout.ObjectField(ES.m_Events[Effected].m_PlayMovie,typeof(MovieTexture),false,GUILayout.Height (m_BoxHeight), GUILayout.Width (160));
			GUILayout.EndHorizontal();
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Sound_Effect)
		{
			//int capacity = EditorGUILayout.IntField("No. meshes",ES.ef_SoundEffect[Object].Capacity, GUILayout.Height (m_BoxHeight),GUILayout.Width (182));
			//
			//if(capacity != ES.ef_SoundEffect[Object].Count())
			//{
			//	ES.ef_SoundEffect[Object].Clear();
			//	ES.ef_SoundEffect[Object].TrimExcess();
			//	ES.ef_SoundEffect[Object].Capacity = capacity;
			//	for(int i = 0; i < ES.ef_SoundEffect[Object].Capacity; i++)
			//	{		
			//		ES.ef_SoundEffect[Object].Add(null);
			//	}
			//}
			//for(int i = 0; i < ES.ef_SoundEffect[Object].Count(); i++)
			//{
			//	ES.ef_SoundEffect[Object][i] = (FMODAsset)EditorGUILayout.ObjectField(ES.ef_SoundEffect[Object][i],typeof(FMODAsset),GUILayout.Height (m_BoxHeight), GUILayout.Width (200));
			//}
		}

		// <===============================================================================>
		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Spawn)
		{
			
		}

		// <===============================================================================>

		
		else if(ES.m_Events[Effected].m_ChosenEffect == m_DifferentEffects.Unlock)
		{
			EditorGUILayout.LabelField ("Unlocks the object", GUILayout.Height (m_TextHeight), GUILayout.Width (203));
		}
	}

	void MoveUp(int i){
		EventEffect temp = new EventEffect ();
		temp = ES.m_Events[i];
		ES.m_Events [i] = ES.m_Events[i-1];
		ES.m_Events [i-1] = temp;
	}

	void MoveDown(int i){
		EventEffect temp = new EventEffect ();
		temp = ES.m_Events[i];
		ES.m_Events [i] = ES.m_Events[i+1];
		ES.m_Events [i+1] = temp;
	}
}	
