using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class CodeNumber : MonoBehaviour 
{
	#region PublicMemberVariables
	public int 		m_MyNumber;
	#endregion
	
	#region PrivateMemberVariables
	private GUITexture  m_Parent;
	private GUIText     m_Text;
	private Vector2 	m_WidthHeight;
	#endregion

	// Use this for initialization
	void Start () 
	{
		m_Parent = transform.parent.GetComponent<GUITexture> ();
		m_Text   = GetComponent<GUIText> ();
		SetPosition ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_Parent.enabled != m_Text.enabled)
		{
			m_Text.enabled = m_Parent.enabled;
		}
	}

	private void SetPosition()
	{
		float TempHeightOffset = 0;
		float TempWidthOffset  = -m_Parent.GetScreenRect ().width / 2;
		TempWidthOffset += 100 + (m_MyNumber * 100);
		Vector2 TempVec = new Vector2 (TempWidthOffset, TempHeightOffset);
		m_Text.pixelOffset = TempVec;
	}

	public void ChangeNumber(int who, int number)
	{
		if(who == m_MyNumber)
		{
			m_Text.text = number.ToString();
		}
	}
}
