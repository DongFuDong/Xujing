using UnityEngine;
using System.Collections;

public class enable_floors : MonoBehaviour {
	
	public GameObject itsFirstFloor;
	public GameObject itsSecondFloor;
	public GameObject itsThirdFloor;
	public KGFCharacterController3rdPerson itsController;
	
	public KGFOrbitCamSettings itsFirstFloorSettings;
	public KGFOrbitCamSettings itsSecondFloorSettings;
	public KGFOrbitCamSettings itsThirdFloorSettings;
	public KGFOrbitCamSettings itsCharacterSettings;
	
	// Use this for initialization
	void Start () {
		itsFirstFloor.GetComponent<Renderer>().enabled = true;
		itsSecondFloor.GetComponent<Renderer>().enabled = true;
		itsThirdFloor.GetComponent<Renderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea( new Rect (0f,0f,Screen.width, Screen.height/10f) );
		{
			GUILayout.BeginHorizontal();
			{
				if(GUILayout.Button("first floor"))
				{
					itsFirstFloorSettings.Apply();
					enableFloorRenderer(1);
					itsController.SetUseClickTarget(false);
				}
				GUILayout.Space(32);
				if(GUILayout.Button("second floor"))
				{
					itsSecondFloorSettings.Apply();
					enableFloorRenderer(2);
					itsController.SetUseClickTarget(false);
				}
				GUILayout.Space(32);
				if(GUILayout.Button("third floor"))
				{
					itsThirdFloorSettings.Apply();
					enableFloorRenderer(3);
					itsController.SetUseClickTarget(false);
				}
				GUILayout.Space(32);
				if(GUILayout.Button("character"))
				{
					itsCharacterSettings.Apply();
					enableFloorRenderer(3);
					itsController.SetUseClickTarget(true);
				}
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	}
	
	
	void enableFloorRenderer(int theFloor)
	{
		if(theFloor >= 1)
		{
			itsFirstFloor.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			itsFirstFloor.GetComponent<Renderer>().enabled = false;	
		}
		
		if(theFloor >= 2)
		{
			itsSecondFloor.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			itsSecondFloor.GetComponent<Renderer>().enabled = false;	
		}
		
		if(theFloor >= 3)
		{
			itsThirdFloor.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			itsThirdFloor.GetComponent<Renderer>().enabled = false;	
		}
	}
}
