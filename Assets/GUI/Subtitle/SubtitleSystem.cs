using UnityEngine;
using System.Collections;

public class SubtitleSystem : MonoBehaviour 
{
	public GameObject[] m_Subtitles;

	private bool    m_Active = false;
	private float   m_Time;
	private float 	m_Timer 		 	= 0;
	private float   m_DelayTime 		= 0;
	private float   m_DelayTimer 	 	= 0;
	private bool  	m_End 			 	= false;
	private int 	m_SelectedChar 		= 0;
	private int   	m_SelectedSubtitle	= 0;
	private bool    m_EndSubtitle		= false;
	private bool    m_ClearField		= false;
	private GUIText m_Text;
	private char[] 	m_Chars;

	// Use this for initialization
	void Start ()
	{
		m_Text = GameObject.FindGameObjectWithTag ("SubtitleGUI").GetComponent<GUIText>();
		m_Chars = new char[0];
		m_Chars = m_Subtitles [m_SelectedSubtitle].GetComponent<Subtitle> ().m_Subtitle.ToCharArray ();
		m_Time = m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_DelayLetter;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_Active && m_Text.enabled == false)
		{
			StartSubtitle();
		}
		if(m_Active && m_DelayTimer >= m_DelayTime)
		{
			if(m_ClearField)
			{
				m_Text.text = "";
				m_ClearField = false;
			}
			m_Timer += Time.deltaTime;
			if(m_Timer > m_Time)
			{
				if(m_EndSubtitle)
				{
					EndSubtitle();
				}
				if(!m_End)
				{
					if(m_Chars[m_SelectedChar].ToString() == "#")
					{
						m_Text.text += "\n";
					}
					else
					{
						m_Text.text += m_Chars[m_SelectedChar].ToString();
					}
					m_Timer = 0;
					if(m_SelectedChar < m_Chars.Length - 1)
					{
						m_SelectedChar++;
					}
					else
					{
						m_End = true;
					}
				}
				else
				{
					m_End = false;
					NextSubtitleChars();
				}
			}
		}
		else
		{
			m_DelayTimer += Time.deltaTime;
		}
	}

	private void NextSubtitleChars()
	{
		if(m_SelectedSubtitle < m_Subtitles.Length - 1)
		{
			m_DelayTime = m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_DelayNextSubtitle;
			m_SelectedSubtitle++;
			if(m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_ClearField)
			{
				m_ClearField = true;
			}
			m_Time = m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_DelayLetter;
			m_SelectedChar = 0;
			m_Chars = m_Subtitles [m_SelectedSubtitle].GetComponent<Subtitle> ().m_Subtitle.ToCharArray ();
			m_DelayTimer = 0;
		}
		else
		{
			m_DelayTime = m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_DelayNextSubtitle;
			m_EndSubtitle = true;
			m_DelayTimer = 0;
		}
	}

	private void EndSubtitle()
	{
		m_EndSubtitle = false;
		m_Text.text = "";
		m_Active = false;
		m_Text.enabled = false;
		m_Text.gameObject.transform.parent.GetComponent<GUITexture>().enabled = false;
		gameObject.SetActive(false);
	}
	
	public void StartSubtitle()
	{
		m_End = false;
		m_Text.text = "";
		m_Text.enabled = true;
		m_Text.gameObject.transform.parent.GetComponent<GUITexture>().enabled = true;
		m_SelectedSubtitle = 0;
		m_SelectedChar     = 0;
		m_Chars = m_Subtitles [m_SelectedSubtitle].GetComponent<Subtitle> ().m_Subtitle.ToCharArray ();
		m_Time = m_Subtitles[m_SelectedSubtitle].GetComponent<Subtitle>().m_DelayLetter;
		m_Active = true;
	}
}
