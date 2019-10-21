using UnityEngine;
using System.Collections;

public class save_load_camera_settings : MonoBehaviour{
	
	public KGFOrbitCam theOrbitCam;
	private KGFOrbitCamData theOrbitCamData = null;
	
	public void Update ()
	{
		if(theOrbitCam != null)
		{
			if(Input.GetKeyUp(KeyCode.K))
			{
				theOrbitCamData = theOrbitCam.SaveSettings();
			}
			
			if(Input.GetKeyUp(KeyCode.L) && theOrbitCamData != null)
			{
				theOrbitCam.LoadSettings(theOrbitCamData);
			}
		}
	}
}
