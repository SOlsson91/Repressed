using UnityEngine;
using System.Collections;

/*This class handle the input from the player. Setting gui to idle position when not active
 * Also holds the "SelectBox" behind items in inventory
 * 
Created by: Rasmus
 */

public class Select : MonoBehaviour 
{
	#region PublicMemberVariables
	public GameObject m_Messure;
	public GameObject m_Manager;
	public GameObject m_Flashlight;
	public GameObject m_Inventory;

	public Texture    m_TextureMatches;
	public Texture    m_TextureFlashlight;
	//public int m_Offset = 90;
	//public int m_Offset2 = 90;
	#endregion
	
	#region PrivateMemberVariables
	private int      m_Max;
	private Vector3  m_Vec;
	private int      m_Selected;
	private GameObject m_LastObject;
	private float    m_Up;
	private float    m_Up2;
	//private bool    m_Flash;
	private Rect 	 m_OriginalPosition;
	private Rect 	 m_IdlePosition;
	private Rect 	 m_OriginalPosition2;
	private Rect 	 m_IdlePosition2;
	private bool     m_Init;
	private bool     m_PressedOnce;
	private TextMesh m_Text;
	#endregion

	// Use this for initialization
	void Start () 
	{
		m_Text = GetComponentInChildren<TextMesh>();
		m_PressedOnce = false;
		//m_Flash = false;
		m_Init = true;
		m_Selected = 0;
		//m_Move = m_Messure.collider.bounds.size.x + 0.2f;
	}

	void FixedUpdate()
	{
		//Update the green box which indicate the selected Item in inventoryt
		m_Max = m_Manager.GetComponent<Manager> ().GetObjectVector ().Length - 1;
		if(m_Manager.GetComponent<Manager> ().GetObjectVector().Length > 0)
		{
			ChangeSelectBox();
		}
	}
	// Update is called once per frame
	void Update () 
	{
		//Save the two position for the GUI, when idle and active
		if(m_Init)
		{
			m_OriginalPosition = m_Inventory.GetComponent<GUITexture>().pixelInset;
			m_IdlePosition = m_OriginalPosition;
			//m_IdlePosition.y -= m_Offset;
			m_IdlePosition.y -= m_Inventory.GetComponent<GUITexture>().pixelInset.height;
			m_OriginalPosition2 = m_Flashlight.GetComponent<GUITexture>().pixelInset;
			m_IdlePosition2 = m_OriginalPosition2;
			//m_IdlePosition2.y -= m_Offset2;
			m_IdlePosition2.y -= m_Flashlight.GetComponent<GUITexture>().pixelInset.height;
			m_Init=false;
			//m_Flashlight.GetComponent<GUITexture>().pixelInset = m_IdlePosition2;
			m_Flashlight.GetComponent<GUITexture>().pixelInset = m_OriginalPosition2;
		}

		UpdateSelected ();

		//Set the inventory to idle position after a while
		if(m_Up2 > 0)
		{
			m_Up2 -= Time.deltaTime;
		}
		else
		{
			//m_Flashlight.GetComponent<GUITexture>().pixelInset = m_IdlePosition2;
			m_Flashlight.GetComponent<GUITexture>().color = new Color(1f, 1f, 1f, 0f);
		}

		if(m_Up > 0)
		{
			m_Up -= Time.deltaTime;
		}
		else
		{
			//m_Inventory.GetComponent<GUITexture>().pixelInset = m_IdlePosition;
			m_Inventory.GetComponent<GUITexture>().color = new Color(1f, 1f, 1f, 0f);
			ShowText(false);
		}
	}

	private void UpdateSelected()
	{
		if(Camera.main.GetComponent<HandScript>().m_Active == false)
		{
			if(Input.GetKeyDown("1"))
			{
				m_Up2 = 1f;
				m_Flashlight.GetComponent<GUITexture>().color = new Color(1f, 1f, 1f, 0.5f);
				//m_Flashlight.GetComponent<GUITexture>().pixelInset = m_OriginalPosition2;
				m_Flashlight.transform.parent.GetComponentInChildren<ChangeLightSource>().ChooseMatches();
				//m_Flashlight.GetComponent<GUITexture>().texture = m_TextureFlashlight;
			}
			if(Input.GetKeyDown("2"))
			{
				m_Up2 = 1f;
				m_Flashlight.GetComponent<GUITexture>().color = new Color(1f, 1f, 1f, 0.5f);
				//m_Flashlight.GetComponent<GUITexture>().pixelInset = m_OriginalPosition2;
				m_Flashlight.transform.parent.GetComponentInChildren<ChangeLightSource>().ChooseFlashLight();
				//m_Flashlight.GetComponent<GUITexture>().texture = m_TextureMatches;
			}
		}

		//Puts the gui for flashlight up/down each time f is pressed
		//if(Input.GetKeyDown("f"))
		//{
		//	m_Flash = !m_Flash;
		//	if(m_Flash)
		//	{
		//		m_Flashlight.GetComponent<GUITexture>().pixelInset = m_OriginalPosition2;
		//
		//	}
		//	else
		//	{
		//		m_Flashlight.GetComponent<GUITexture>().pixelInset = m_IdlePosition2;
		//	}
		//}
		//Put up the inventory first time, next will scroll throu objects
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(m_Manager.GetComponent<Manager> ().GetObjectVector().Length > 0)
			{
				if(m_Up > 0)
				{
					if(m_Selected < m_Max)
					{
						m_Selected++;
					}
					else
					{
						m_Selected = 0;
					}
					m_Manager.GetComponent<Manager>().SetSelected(m_Selected);
				}
			}
			SetInventoryFocus();
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0 )
		{
			if(m_Manager.GetComponent<Manager> ().GetObjectVector().Length > 0)
			{
				if(m_Up > 0)
				{
				
					if(m_Selected > 0)
					{
						m_Selected--;
					}
					else
					{
						m_Selected = m_Max;
					}
					m_Manager.GetComponent<Manager>().SetSelected(m_Selected);
				}
			}
			SetInventoryFocus();
		}
		if(Input.GetAxis("xBoxMouse Scroll") > 0.5 && m_PressedOnce == false)
		{
			if(m_Manager.GetComponent<Manager> ().GetObjectVector().Length > 0)
			{
				if(m_Up > 0)
				{
					if(m_Selected < m_Max)
					{
						m_Selected++;
					}
					else
					{
						m_Selected = 0;
					}
					m_Manager.GetComponent<Manager>().SetSelected(m_Selected);
				}
			}
			SetInventoryFocus();
			m_PressedOnce = true;
		}
		if(Input.GetAxis("xBoxMouse Scroll") < -0.5 && m_PressedOnce == false)
		{
			m_PressedOnce = true;
			if(m_Manager.GetComponent<Manager> ().GetObjectVector().Length > 0)
			{
				if(m_Up > 0)
				{
					
					if(m_Selected > 0)
					{
						m_Selected--;
					}
					else
					{
						m_Selected = m_Max;
					}
					m_Manager.GetComponent<Manager>().SetSelected(m_Selected);
				}
			}
			SetInventoryFocus();
		}
		if(Input.GetAxis("xBoxMouse Scroll") > -0.3 && Input.GetAxis("xBoxMouse Scroll") < 0.3)
		{
			m_PressedOnce = false;
		}
	}

	public float GetInventoryFocus()
	{
		return m_Up;
	}

	public void SetInventoryFocus()
	{
		m_Up = 1f;
		ShowText(true);
		m_Inventory.GetComponent<GUITexture>().color = new Color(1f, 1f, 1f, 0.5f);
		//m_Inventory.GetComponent<GUITexture>().pixelInset = m_OriginalPosition;
	}

	//If selected change in Manager and not from here
	public void UpdateSelected(int i)
	{
		m_Selected = i;
	}

	public void ChangeSelectBox()
	{
		Vector3 offset = new Vector3 (0, 0, -0.1f);
		if(m_Manager.GetComponent<Manager> ().SelectedGameObject () == null)
		{
			m_Selected--;
			m_Manager.GetComponent<Manager>().SetSelected(m_Selected);
		}
		else
		{
			transform.position = m_Manager.GetComponent<Manager> ().SelectedGameObject ().transform.position - offset;
		}
		//string tempString = m_Manager.GetComponent<Manager> ().SelectedGameObject ().GetComponent<InventoryItem>().GetGameObject()[0].GetComponent<ItemDiscription>().m_Discription;
		//m_Text.text = tempString;
	}

	public void ShowText(bool b)
	{
		//m_Text.gameObject.SetActive(b);
	}
}
