using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor (typeof(SuperTrigger))]
public class SuperTriggerEditor : Editor
{
	private SuperTrigger ST;
	private int 		 m_TextHeight 	   = 20;
	private int  		 m_BoxHeight  	   = 15;
	private int 		 m_CloseHeight     = 13;
	private int		  	 m_RowHeight	   = 20;
	private bool 		 m_FoldoutCollabor = false;
	private bool   		 m_FoldoutTriggers = false;
	private bool   		 m_FoldoutEvent    = false;
	private bool   		 m_FoldoutAll 	   = false;
	private bool		 m_SetUp 		   = false;
	private string 		 m_Description     = "";
	private GUISkin      m_Skin;

	public override void OnInspectorGUI()
    {
		SetUp ();

		if(m_Description == "")
		{
			m_FoldoutAll = NGUIEditorTools.DrawHeader("SuperTrigger",m_FoldoutAll);
		}
		else
		{
			m_FoldoutAll = NGUIEditorTools.DrawHeader(ST.m_Description,m_FoldoutAll);
		}
		if(m_FoldoutAll)
		{
			NGUIEditorTools.BeginContents();
			GUILayout.Space(6);

			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			GUILayout.Space(10);
			EditorGUILayout.LabelField ("Description", GUILayout.Height (m_TextHeight), GUILayout.Width (103));	
			m_Description = EditorGUILayout.TextField(m_Description, GUILayout.Height (m_BoxHeight),GUILayout.Width (260));
			GUILayout.EndHorizontal();

			EditorGUI.indentLevel++;
			
			m_FoldoutTriggers = NGUIEditorTools.DrawHeader("Triggers",m_FoldoutTriggers);
			if(m_FoldoutTriggers)
			{
				NGUIEditorTools.BeginContents();
				GUILayout.Space(2);
				CollaborateSelfLayout ();
				CollaborateGetLayout ();
				CollisionLayout ();
				ZoneTimerLayout();
				OnClickLayout ();
				HoverTrigger ();
				NGUIEditorTools.EndContents();
				EditorGUILayout.Separator();
			}
			m_FoldoutEvent = NGUIEditorTools.DrawHeader("Events to trigger",m_FoldoutEvent);
			if(m_FoldoutEvent)
			{
				NGUIEditorTools.BeginContents();
				GUILayout.Space(2);
				Events();
				NGUIEditorTools.EndContents();
			}
			NGUIEditorTools.EndContents();
		}
		EditorGUILayout.Separator();
	}

	void SetUp()
	{
		if(!m_SetUp)
		{
			GUIStyle m_Style = new GUIStyle(EditorStyles.foldout);
			m_Skin = Resources.Load ("GUI Skins/Buttons") as GUISkin;
			
			ST = (SuperTrigger)target;
			Color myColor = Color.blue;
			m_Style.normal.textColor = myColor;
			m_Style.onNormal.textColor = myColor;
			m_Style.hover.textColor = myColor;
			m_Style.onHover.textColor = myColor;
			m_Style.focused.textColor = myColor;
			m_Style.onFocused.textColor = myColor;
			m_Style.active.textColor = myColor;
			m_Style.onActive.textColor = myColor;
			m_Style.fontStyle = FontStyle.Bold;
			m_Style.fontSize = 12;
		}
	}

	void Events()
	{
		GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
		EditorGUILayout.LabelField ("Events: " + ST.m_TriggerEvents.Count(), GUILayout.Height (m_TextHeight), GUILayout.Width (116));
		if (GUILayout.Button("Add Event", GUILayout.Height (m_BoxHeight), GUILayout.Width (150)))
		{
			ST.m_TriggerEvents.Add(new TriggerEvent());
		}
		GUILayout.EndHorizontal();

		for(int i = 0; i < ST.m_TriggerEvents.Count(); i++)
		{
			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
	
			ST.m_TriggerEvents[i].m_FoldoutEvent = NGUIEditorTools.DrawHeader(ST.m_TriggerEvents[i].m_Event.ToString(),ST.m_TriggerEvents[i].m_FoldoutEvent);			
			GUI.skin = m_Skin;
			if (GUILayout.Button("X",GUILayout.Height (m_CloseHeight), GUILayout.Width (20)))
			{
				ST.m_TriggerEvents.RemoveAt(i);
				GUILayout.EndHorizontal();
				break;
			}
			GUI.skin = null;		
			GUILayout.EndHorizontal();

			if(ST.m_TriggerEvents[i].m_FoldoutEvent)
			{
				NGUIEditorTools.BeginContents(8);

				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				EditorGUILayout.LabelField ("Event ID", GUILayout.Height (m_TextHeight), GUILayout.Width (95));
				ST.m_TriggerEvents[i].m_Event = EditorGUILayout.IntField((int)Mathf.Clamp(ST.m_TriggerEvents[i].m_Event,0,Mathf.Infinity) , GUILayout.Height (m_BoxHeight),GUILayout.Width (50));
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));

				if(!ST.m_TriggerEvents[i].m_TriggerAfterEvents)
				{
					EditorGUILayout.LabelField ("Trigger at ",GUILayout.Height (m_TextHeight), GUILayout.Width (95));
					ST.m_TriggerEvents[i].m_TriggerAt = EditorGUILayout.Toggle(ST.m_TriggerEvents[i].m_TriggerAt,GUILayout.Height (m_BoxHeight), GUILayout.Width (30));
				}

				if(ST.m_TriggerEvents[i].m_TriggerAt)
				{
					EditorGUILayout.LabelField ("count: ",GUILayout.Height (m_TextHeight), GUILayout.Width (70));
					ST.m_TriggerEvents[i].m_CounterValue = EditorGUILayout.IntField((int)Mathf.Clamp(ST.m_TriggerEvents[i].m_CounterValue,1,Mathf.Infinity),GUILayout.Height(m_BoxHeight), GUILayout.Width(50));
					GUILayout.EndHorizontal();
					NGUIEditorTools.BeginContents(23);
					if(!ST.m_TriggerEvents[i].m_TriggerOnce)
					{
						GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
						EditorGUILayout.LabelField ("Trigger times", GUILayout.Height (m_TextHeight), GUILayout.Width (95));
						ST.m_TriggerEvents[i].m_TriggerTimes = EditorGUILayout.Toggle(ST.m_TriggerEvents[i].m_TriggerTimes,GUILayout.Height (m_BoxHeight), GUILayout.Width (30));
						if(ST.m_TriggerEvents[i].m_TriggerTimes)
						{
							EditorGUILayout.LabelField ("count: ",GUILayout.Height (m_TextHeight), GUILayout.Width (70));
							ST.m_TriggerEvents[i].m_TimesToTrigger = EditorGUILayout.IntField((int)Mathf.Clamp(ST.m_TriggerEvents[i].m_TimesToTrigger,1,Mathf.Infinity),GUILayout.Height(m_BoxHeight), GUILayout.Width(50));
						}
						GUILayout.EndHorizontal();
					}
					if(!ST.m_TriggerEvents[i].m_TriggerTimes)
					{
						GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
						EditorGUILayout.LabelField ("Trigger once", GUILayout.Height (m_TextHeight), GUILayout.Width (95));
						ST.m_TriggerEvents[i].m_TriggerOnce = EditorGUILayout.Toggle(ST.m_TriggerEvents[i].m_TriggerOnce,GUILayout.Height (m_BoxHeight), GUILayout.Width (30));
						GUILayout.EndHorizontal();
					}
					NGUIEditorTools.EndContents();
				}
				else
				{
					GUILayout.EndHorizontal();
				}
				if(!ST.m_TriggerEvents[i].m_TriggerAt)
				{
					GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
					EditorGUILayout.LabelField ("Trigger after events ",GUILayout.Height (m_TextHeight), GUILayout.Width (135));
					ST.m_TriggerEvents[i].m_TriggerAfterEvents = EditorGUILayout.Toggle(ST.m_TriggerEvents[i].m_TriggerAfterEvents,GUILayout.Height (m_BoxHeight), GUILayout.Width (30));
					GUILayout.EndHorizontal();
				}

				if(ST.m_TriggerEvents[i].m_TriggerAfterEvents)
				{
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					ST.m_TriggerEvents[i].m_FoldOutAfterEvents = NGUIEditorTools.DrawHeader("Trigger after events",ST.m_TriggerEvents[i].m_FoldOutAfterEvents);
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
					EditorGUILayout.LabelField ("Trigger once", GUILayout.Height (m_TextHeight), GUILayout.Width (95));
					ST.m_TriggerEvents[i].m_TriggerOnce = EditorGUILayout.Toggle(ST.m_TriggerEvents[i].m_TriggerOnce,GUILayout.Height (m_BoxHeight), GUILayout.Width (30));
					GUILayout.EndHorizontal();

					if(ST.m_TriggerEvents[i].m_FoldOutAfterEvents)
					{	
						NGUIEditorTools.BeginContents(23);						
						GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
						EditorGUILayout.LabelField ("No. of IDs: " + ST.m_TriggerEvents[i].m_EventIDs.Count(), GUILayout.Height (m_TextHeight), GUILayout.Width (116));
						if (GUILayout.Button("Add Event ID", GUILayout.Height (m_BoxHeight), GUILayout.Width (150)))
						{
							ST.m_TriggerEvents[i].m_EventIDs.Add(0);
							ST.m_TriggerEvents[i].m_EventsTriggered.Add(false);
						}
						GUILayout.EndHorizontal();
						
						for(int j = 0; j < ST.m_TriggerEvents[i].m_EventIDs.Count(); j++)
						{
							GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
							
							EditorGUILayout.LabelField ("ID: "+(j+1), GUILayout.Height (m_TextHeight), GUILayout.Width (55));
							ST.m_TriggerEvents[i].m_EventIDs[j] = EditorGUILayout.IntField((int)Mathf.Clamp(ST.m_TriggerEvents[i].m_EventIDs[j],0,Mathf.Infinity) , GUILayout.Height (m_BoxHeight),GUILayout.Width (50));							
						
							GUI.skin = m_Skin;
							GUILayout.Space(20);
						
							if (GUILayout.Button("X",GUILayout.Height (m_CloseHeight), GUILayout.Width (20)))
							{
								ST.m_TriggerEvents[i].m_EventIDs.RemoveAt(j);
								ST.m_TriggerEvents[i].m_EventsTriggered.RemoveAt(j);
								GUILayout.EndHorizontal();
								break;
							}
							
							GUI.skin = null;
							
							GUILayout.EndHorizontal();
						}
						NGUIEditorTools.EndContents();
					}
				}
				NGUIEditorTools.EndContents();
			}
		}	
	}
	
	void CollaborateSelfLayout()
	{
		ST.m_CollaborateSelf = EditorGUILayout.Toggle ("CollaborateSelf: ", ST.m_CollaborateSelf, GUILayout.Height (m_BoxHeight));
		if(ST.m_CollaborateSelf)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			m_FoldoutCollabor = NGUIEditorTools.DrawHeader("Collaborate Self",m_FoldoutCollabor);
			GUILayout.EndHorizontal();
			if(m_FoldoutCollabor)
			{
				EditorGUI.indentLevel++;
				
				NGUIEditorTools.BeginContents(20);
				
				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				EditorGUILayout.LabelField ("Input", GUILayout.Height (m_TextHeight), GUILayout.Width (88));
				ST.m_CollaborateInput = EditorGUILayout.TextField(ST.m_CollaborateInput,GUILayout.Height (m_BoxHeight),GUILayout.Width (80));
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				EditorGUILayout.LabelField ("No. of IDs: " + ST.m_IDsCollaborate.Count(), GUILayout.Height (m_TextHeight), GUILayout.Width (116));
				if (GUILayout.Button("Add Valid ID", GUILayout.Height (m_BoxHeight), GUILayout.Width (150)))
				{
					ST.m_IDsCollaborate.Add(0);
				}
				GUILayout.EndHorizontal();
				
				for(int i = 0; i < ST.m_IDsCollaborate.Count(); i++)
				{
					GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
				
					EditorGUILayout.LabelField ("ID: "+(i+1), GUILayout.Height (m_TextHeight), GUILayout.Width (95));
					ST.m_IDsCollaborate[i] = EditorGUILayout.IntField((int)Mathf.Clamp(ST.m_IDsCollaborate[i],0,Mathf.Infinity) , GUILayout.Height (m_BoxHeight),GUILayout.Width (50));							
				
					GUI.skin = m_Skin;
					GUILayout.Space(20);
				
					if (GUILayout.Button("X",GUILayout.Height (m_CloseHeight), GUILayout.Width (20)))
					{
						ST.m_IDsCollaborate.RemoveAt(i);
						GUILayout.EndHorizontal();
						break;
					}
				
					GUI.skin = null;
				
					GUILayout.EndHorizontal();
				}
				NGUIEditorTools.EndContents();
				EditorGUI.indentLevel--;
			}
		}
	}
	void ZoneTimerLayout()
	{
		ST.m_ZoneTimer = EditorGUILayout.Toggle ("ZoneTimer: ", ST.m_ZoneTimer, GUILayout.Height (15));
		if(ST.m_ZoneTimer)
		{
			EditorGUI.indentLevel++;
			GUILayout.BeginHorizontal(GUILayout.Height(m_RowHeight));
			EditorGUILayout.LabelField("Timer Value: ",GUILayout.Height(m_TextHeight),GUILayout.Width(120));
			GUILayout.Space(-30);
			ST.m_TimerValue = EditorGUILayout.FloatField (ST.m_TimerValue, GUILayout.Height(m_BoxHeight),GUILayout.Width(60));
			GUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}
	}

	void OnClickLayout()
	{
		ST.m_OnClick = EditorGUILayout.Toggle ("OnClick: ", ST.m_OnClick, GUILayout.Height (15));
	}

	void CollaborateGetLayout()
	{
		ST.m_CollaborateGet = EditorGUILayout.Toggle ("CollaborateGet: ", ST.m_CollaborateGet, GUILayout.Height (15));
	}

	void CollisionLayout()
	{
		ST.m_Collision = EditorGUILayout.Toggle ("Collision: ", ST.m_Collision, GUILayout.Height (15));
	}
	void HoverTrigger()
	{
		ST.m_Hover = EditorGUILayout.Toggle ("Hover: ", ST.m_Hover, GUILayout.Height (15));
	}

}