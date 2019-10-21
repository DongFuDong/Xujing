using UnityEngine;
using System.Collections;

public class event_trigger_button : MonoBehaviour
{
	public Texture2D itsKeys;
	public Texture2D itsMouseRotate;
	public Texture2D itsMouseZoom;
	public GameObject itsCharacter;
	public KGFOrbitCam itsOrbiter;
	
	public bool drawControls = false;
	
	
	public KGFOrbitCamSettings itsEventSwitchToCapsule;
	public KGFOrbitCamSettings itsEventSwitchToLeftTower;
	public KGFOrbitCamSettings itsEventSwitchToRightTower;
	public KGFOrbitCamSettings itsEventSwitchToPlaza;
	public KGFOrbitCamSettings itsEventSwitchToInnerCity;
	public KGFOrbitCamSettings itsEventSwitchToUpSideDown;
	public KGFOrbitCamSettings itsEventSwitchToFishEye;
	public Texture2D itsButtonTexture;
	
	private Rect aButton1Rect;
	private Rect aButton2Rect;
	private Rect aButton3Rect;
	private Rect aButton4Rect;
	private Rect aButton5Rect;
	private Rect aButton6Rect;
	private Rect aButton7Rect;
	
	private GUIStyle itsGUIStyle;

	
	/// <summary>
	/// The possible camera roots
	/// </summary>
	public enum eCameraRoot
	{
		eCharacter,
		eLeftTower,
		eLake,
		eTower,
		eSign,
		eUpSideDown,
		eFishEye
	}
	
	/// <summary>
	/// The current
	/// </summary>
	private eCameraRoot itsCurrentCameraRoot = eCameraRoot.eCharacter;
	
	// Use this for initialization
	void Start ()
	{
		//Application.targetFrameRate = 30;
		float aPadding = 10.0f;
		float aButtonWidth = (Screen.width - aPadding*7)/7.0f;
		float aButtonHeight = 32.0f;
		
		float anXOffset = 10;
		float aTotalWidth = aButtonWidth + aPadding;
		
		aButton1Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton2Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton3Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton4Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton5Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton6Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		anXOffset += aTotalWidth;
		aButton7Rect = new Rect(anXOffset,aPadding,aButtonWidth,aButtonHeight);
		
		itsGUIStyle = new GUIStyle();
		itsGUIStyle.normal.background = itsButtonTexture;
		itsGUIStyle.normal.textColor = new Color(1,1,1);
		itsGUIStyle.alignment = TextAnchor.MiddleCenter;
	}

	/// <summary>
	/// Check for rmb
	/// </summary>
	void Update()
	{
		if(Input.GetMouseButton(1))
		{
			itsOrbiter.EnableInput(true);
		}
		else
		{
			itsOrbiter.EnableInput(false);
			if(itsCurrentCameraRoot == eCameraRoot.eCharacter)
			{
				itsOrbiter.ResetRotationToStart();
			}
		}
	}
	
	/// <summary>
	/// Draw the top buttons
	/// </summary>
	void OnGUI ()
	{
		if(GUI.Button (aButton1Rect,"fish eye",itsGUIStyle))
		{
			itsEventSwitchToFishEye.Apply();
			itsCurrentCameraRoot = eCameraRoot.eFishEye;
		}		
		else if(GUI.Button (aButton2Rect,"switch to LeftTower",itsGUIStyle))
		{
			itsEventSwitchToLeftTower.Apply();
			itsCurrentCameraRoot = eCameraRoot.eLeftTower;
		}
		else if(GUI.Button (aButton3Rect,"switch to Lake",itsGUIStyle))
		{
			itsEventSwitchToRightTower.Apply();
			itsCurrentCameraRoot = eCameraRoot.eLake;
		}
		if(GUI.Button (aButton4Rect,"up side down",itsGUIStyle))
		{
			itsEventSwitchToUpSideDown.Apply();
			itsCurrentCameraRoot = eCameraRoot.eUpSideDown;
		}
		else if(GUI.Button (aButton5Rect,"switch to Tower",itsGUIStyle))
		{
			itsEventSwitchToPlaza.Apply();
			itsCurrentCameraRoot = eCameraRoot.eTower;
		}
		else if(GUI.Button (aButton6Rect,"switch to Sign",itsGUIStyle))
		{
			itsEventSwitchToInnerCity.Apply();
			itsCurrentCameraRoot = eCameraRoot.eSign;
		}		
		else if(GUI.Button (aButton7Rect,"character",itsGUIStyle))
		{
			itsEventSwitchToCapsule.Apply();
			itsCurrentCameraRoot = eCameraRoot.eCharacter;
		}
		
		if(drawControls)
			DrawInput();
	}
	
	/// <summary>
	/// Draw the input information (bottom right)
	/// </summary>
	public void DrawInput()
	{
		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		{
			KGFGUIUtility.BeginVerticalBox(KGFGUIUtility.eStyleBox.eBoxInvisible);
			{
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					
					if(itsOrbiter.itsRoot.itsRoot == itsCharacter)
					{
						KGFGUIUtility.Box(itsKeys,KGFGUIUtility.eStyleBox.eBoxInvisible, GUILayout.Height(Screen.height/10f), GUILayout.Width(Screen.width/10f));
						KGFGUIUtility.Box(itsMouseRotate,KGFGUIUtility.eStyleBox.eBoxInvisible, GUILayout.Height(Screen.height/10f), GUILayout.Width(Screen.width/10f));
						KGFGUIUtility.Box(itsMouseZoom,KGFGUIUtility.eStyleBox.eBoxInvisible, GUILayout.Height(Screen.height/10f), GUILayout.Width(Screen.width/10f));
					}
					else
					{
						KGFGUIUtility.Box(itsMouseRotate,KGFGUIUtility.eStyleBox.eBoxInvisible, GUILayout.Height(Screen.height/10f), GUILayout.Width(Screen.width/10f));
						KGFGUIUtility.Box(itsMouseZoom,KGFGUIUtility.eStyleBox.eBoxInvisible, GUILayout.Height(Screen.height/10f), GUILayout.Width(Screen.width/10f));
					}
				}
				GUILayout.EndHorizontal();
				GUILayout.Space(32);
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
	}
	
}
