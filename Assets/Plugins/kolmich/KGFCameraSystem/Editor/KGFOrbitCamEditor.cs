using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(KGFOrbitCam))]
public class KGFOrbitCamEditor : KGFEditor
{
	public static KGFMessageList ValidateKGFOrbitCamEditor(UnityEngine.Object theTarget)
	{
		KGFMessageList aMessageList = KGFEditor.ValidateKGFEditor(theTarget);
		return aMessageList;
	}
	
	protected override void CustomGui()
	{
		base.CustomGui();
		if(!Application.isPlaying)
		{
			KGFOrbitCam anOrbitCam = (KGFOrbitCam)target;
			anOrbitCam.Start();
		}
	}
}

