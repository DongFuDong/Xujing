
//please uncomment the following line if you own the PlayMaker package
//#define PLAYMAKER


using UnityEngine;
using System.Collections;
using System;




#if PLAYMAKER

using HutongGames.PlayMaker;




[ActionCategory("KGFCameraSystem")]

public class KGFOrbitCamSettingsApply : FsmStateAction
{
	public KGFOrbitCamSettings itsOrbitCamSettings;
	public override void Reset ()
	{
		itsOrbitCamSettings = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCamSettings == null)
		{
			LogError("OrbitCamSettings is null");
		}
		else
		{
			itsOrbitCamSettings.Apply();
		}
		Finish();
	}
}








#region root
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRoot : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsRoot;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRoot = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else if(itsRoot.GameObject.Value == null)
		{
			LogError("itsRoot is null");
		}
		else
		{
			itsOrbitCam.SetRoot(itsRoot.GameObject.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRoot : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsRoot;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRoot = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRoot.GameObject.Value = itsOrbitCam.GetTarget();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLinkTargetPosition : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsEnableLinkTargetPosition = false;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsEnableLinkTargetPosition = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLinkTargetPosition(itsEnableLinkTargetPosition.Value);
		}
		Finish ();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLinkTargetPosition : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsEnableLinkTargetPosition = false;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsEnableLinkTargetPosition = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsEnableLinkTargetPosition.Value = itsOrbitCam.GetLinkTargetPosition();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLinkTargetPositionSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLinkTargetPositionSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLinkTargetPositionSpeed = 1;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLinkTargetPositionSpeed(itsLinkTargetPositionSpeed.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLinkTargetPositionSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLinkTargetPositionSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLinkTargetPositionSpeed = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsLinkTargetPositionSpeed.Value = itsOrbitCam.GetLinkTargetPositionSpeed();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLinkTargetRotation : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsEnableLinkTargetRotation = false;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsEnableLinkTargetRotation = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLinkTargetRotation(itsEnableLinkTargetRotation.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLinkTargetRotation : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsEnableLinkTargetRotation = false;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsEnableLinkTargetRotation = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsEnableLinkTargetRotation.Value = itsOrbitCam.GetLinkTargetRotation();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLinkTargetRotationSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLinkTargetRotationSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLinkTargetRotationSpeed = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLinkTargetRotationSpeed(itsLinkTargetRotationSpeed.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLinkTargetRotationSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLinkTargetRotationSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLinkTargetRotationSpeed = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsLinkTargetRotationSpeed.Value = itsOrbitCam.GetLinkTargetRotationSpeed();
		}
		Finish();
	}
}
#endregion




#region rotation

#region rotation up down
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMaxRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMaxRotationUpDown.Value = itsOrbitCam.GetMaxRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMaxRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMaxRotationUpDown(itsMaxRotationUpDown.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMinRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMinRotationUpDown.Value = itsOrbitCam.GetMinRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMinRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMinRotationUpDown(itsMinRotationUpDown.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetTargetRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsTargetRotationUpDown.Value = itsOrbitCam.GetTargetRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetTargetRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetTargetRotationUpDown(itsTargetRotationUpDown.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationUpDown.Value = itsOrbitCam.GetRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetRotationUpDown(itsRotationUpDown.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetStartRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsStartRotationUpDown.Value = itsOrbitCam.GetStartRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetStartRotationUpDown : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartRotationUpDown;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartRotationUpDown = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetStartRotationUpDown(itsStartRotationUpDown.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationUpDownDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationUpDownDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationUpDownDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationUpDownDisabled.Value = itsOrbitCam.GetDisableRotationUpDown();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationUpDownDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationUpDownDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationUpDownDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.DisableRotationUpDown(itsRotationUpDownDisabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationLimitsUpDownDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLimitsUpDownDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLimitsUpDownDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationLimitsUpDownDisabled.Value = itsOrbitCam.GetDisableRotationUpDownLimits();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationLimitsUpDownDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLimitsUpDownDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLimitsUpDownDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.DisableRotationUpDownLimits(itsRotationLimitsUpDownDisabled.Value);
		}
		Finish();
	}
}
#endregion




#region rotation left right
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMaxRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMaxRotationLeftRight.Value = itsOrbitCam.GetMaxRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMaxRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMaxRotationLeftRight(itsMaxRotationLeftRight.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMinRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMinRotationLeftRight.Value = itsOrbitCam.GetMinRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMinRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMinRotationLeftRight(itsMinRotationLeftRight.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetTargetRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsTargetRotationLeftRight.Value = itsOrbitCam.GetTargetRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetTargetRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetTargetRotationLeftRight(itsTargetRotationLeftRight.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationLeftRight.Value = itsOrbitCam.GetRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetRotationLeftRight(itsRotationLeftRight.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetStartRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsStartRotationLeftRight.Value = itsOrbitCam.GetStartRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetStartRotationLeftRight : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartRotationLeftRight;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartRotationLeftRight = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetStartRotationLeftRight(itsStartRotationLeftRight.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationLeftRightDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLeftRightDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLeftRightDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationLeftRightDisabled.Value = itsOrbitCam.GetDisableRotationLeftRight();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationLeftRightDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLeftRightDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLeftRightDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.DisableRotationLeftRight(itsRotationLeftRightDisabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetRotationLimitsLeftRightDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLimitsLeftRightDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLimitsLeftRightDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsRotationLimitsLeftRightDisabled.Value = itsOrbitCam.GetDisableRotationLeftRightLimits();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetRotationLimitsLeftRightDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsRotationLimitsLeftRightDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsRotationLimitsLeftRightDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.DisableRotationLeftRightLimits(itsRotationLimitsLeftRightDisabled.Value);
		}
		Finish();
	}
}
#endregion


[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetUseQuaternions : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsUseQuaternions;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsUseQuaternions = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsUseQuaternions.Value = itsOrbitCam.GetUseQuaternions();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetUseQuaternions : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsUseQuaternions;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsUseQuaternions = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetUseQuaternions(itsUseQuaternions.Value);
		}
		Finish();
	}
}
#endregion




#region zoom
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMaxZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMaxZoom.Value = itsOrbitCam.GetMaxZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMaxZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMaxZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMaxZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMaxZoom(itsMaxZoom.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetMinZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsMinZoom.Value = itsOrbitCam.GetMinZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetMinZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsMinZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsMinZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetMinZoom(itsMinZoom.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetTargetZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsTargetZoom.Value = itsOrbitCam.GetTargetZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetTargetZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsTargetZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsTargetZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetTargetZoom(itsTargetZoom.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsZoom.Value = itsOrbitCam.GetCurrentZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetCurrentZoom(itsZoom.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetStartZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsStartZoom.Value = itsOrbitCam.GetStartZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetStartZoom : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsStartZoom;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsStartZoom = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetStartZoom(itsStartZoom.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetZoomDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsZoomDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsZoomDisabled.Value = itsOrbitCam.GetZoom();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetZoomDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsZoomDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.EnableZoom(itsZoomDisabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetZoomLimitsDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsZoomLimitsDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomLimitsDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsZoomLimitsDisabled.Value = itsOrbitCam.GetDisableZoomLimits();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetZoomLimitsDisabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsZoomLimitsDisabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomLimitsDisabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.DisableZoomLimits(itsZoomLimitsDisabled.Value);
		}
		Finish();
	}
}
#endregion




#region input
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetInputEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsInpuEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsInpuEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsInpuEnabled.Value = itsOrbitCam.GetEnableInput();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetInputEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsInputEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsInputEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.EnableInput(itsInputEnabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetYAxisInverted : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsYAxisInverted;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisInverted = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsYAxisInverted.Value = itsOrbitCam.GetInvertYAxis();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetYAxisInverted: FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsYAxisInverted;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisInverted = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.InvertYAxis(itsYAxisInverted.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetXAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsXAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsXAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsXAxisSensitivity.Value = itsOrbitCam.GetXAxisSensitivity();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetXAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsXAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsXAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetXAxisSensitivity(itsXAxisSensitivity.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetYAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsYAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsYAxisSensitivity.Value = itsOrbitCam.GetYAxisSensitivity();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetYAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsYAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetYAxisSensitivity(itsYAxisSensitivity.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetZoomAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsZoomAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsZoomAxisSensitivity.Value = itsOrbitCam.GetZoomAxisSensitivity();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetZoomAxisSensitivity : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsZoomAxisSensitivity;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomAxisSensitivity = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetZoomAxisSensitivity(itsZoomAxisSensitivity.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetXAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsXAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsXAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsXAxisName.Value = itsOrbitCam.GetXAxisName();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetXAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsXAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsXAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetXAxisName(itsXAxisName.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetYAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsYAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsYAxisName.Value = itsOrbitCam.GetYAxisName();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetYAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsYAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsYAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetYAxisName(itsYAxisName.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetZoomAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsZoomAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsZoomAxisName.Value = itsOrbitCam.GetZoomAxisName();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetZoomAxisName : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmString itsZoomAxisName;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsZoomAxisName = string.Empty;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetZoomAxisName(itsZoomAxisName.Value);
		}
		Finish();
	}
}
#endregion





#region collision
[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetGyroscopeEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsGyroscopeEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsGyroscopeEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsGyroscopeEnabled.Value = itsOrbitCam.GetEnableGyroscope();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetGyroscopeEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsGyroscopeEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsGyroscopeEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.EnableGyroscope(itsGyroscopeEnabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetCollisionEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsCollisionEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsCollisionEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsCollisionEnabled.Value = itsOrbitCam.GetTestCollisions();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetCollisionEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsCollisionEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsCollisionEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.TestCollisions(itsCollisionEnabled.Value);
		}
		Finish();
	}
}
#endregion


#region look at

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLookatEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsLookatEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsLookatEnabled.Value = itsOrbitCam.GetLookatEnabled();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLookatEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsLookatEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.EnableLookat(itsLookatEnabled.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLookatTarget : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsLookatTarget;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatTarget = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLookatTarget(itsLookatTarget.GameObject.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLookatTarget : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsLookatTarget;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatTarget = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else if(itsLookatTarget.GameObject.Value == null)
		{
			LogError("itsLookatTarget is null");
		}
		else
		{
			itsLookatTarget.GameObject.Value = itsOrbitCam.GetLookatTarget();
		}
		Finish ();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetUpVectorSource : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsUpVectorSource;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsUpVectorSource = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else if(itsUpVectorSource.GameObject.Value == null)
		{
			LogError("itsUpVectorSource is null");
		}
		else
		{
			itsOrbitCam.SetUpVectorSource(itsUpVectorSource.GameObject.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetUpVectorSource : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmOwnerDefault itsUpVectorSource;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsUpVectorSource = null;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsUpVectorSource.GameObject.Value = itsOrbitCam.GetUpVectorSource();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLookatSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLookatSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatSpeed = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsLookatSpeed.Value = itsOrbitCam.GetLookatSpeed();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLookatSpeed : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmFloat itsLookatSpeed;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatSpeed = 0;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetLookatSpeed(itsLookatSpeed.Value);
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamGetLookatHardlinkEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsLookatHardlinkEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatHardlinkEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsLookatHardlinkEnabled.Value = itsOrbitCam.GetHardLinkToTarget();
		}
		Finish();
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFOrbitCamSetLookatHardlinkEnabled : FsmStateAction
{
	public KGFOrbitCam itsOrbitCam;
	public FsmBool itsLookatHardlinkEnabled;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
		itsLookatHardlinkEnabled = false;
	}
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{
			itsOrbitCam.SetHardLinkToTarget(itsLookatHardlinkEnabled.Value);
		}
		Finish();
	}
}

#endregion

#region events
[ActionCategory("KGFCameraSystem")]
public class KGFCameraSystemEventRootReached : FsmStateAction
{
	[RequiredField]
	public KGFOrbitCam itsOrbitCam;
	public FsmEvent EventOnRootReached;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
	}
	
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{			
			itsOrbitCam.EventRootReached += OnRootReached;
		}
	}
	
	public override void OnExit()
	{
		if (itsOrbitCam != null)
		{
			itsOrbitCam.EventRootReached -= OnRootReached;
		}
	}
	
	void OnRootReached(object theSender, EventArgs theArgs)
	{		
		Fsm.Event(EventOnRootReached);
		Finish();
	}
	
	public override string ErrorCheck()
	{
		if (itsOrbitCam == null)
		{
			return "itsOrbitCam has to be filled in";
		}
		return null;
	}
}

[ActionCategory("KGFCameraSystem")]
public class KGFCameraSystemEventRootChanged : FsmStateAction
{
	[RequiredField]
	public KGFOrbitCam itsOrbitCam;
	public FsmEvent EventOnRootChanged;
	
	public override void Reset ()
	{
		itsOrbitCam = null;
	}
	
	public override void OnEnter ()
	{
		if(itsOrbitCam == null)
		{
			LogError("itsOrbitCam is null");
		}
		else
		{			
			itsOrbitCam.EventRootChanged += OnRootChanged;
		}
	}
	
	public override void OnExit()
	{
		if (itsOrbitCam != null)
		{
			itsOrbitCam.EventRootChanged -= OnRootChanged;
		}
	}
	
	void OnRootChanged(object theSender, EventArgs theArgs)
	{		
		Fsm.Event(EventOnRootChanged);
		Finish();
	}
	
	public override string ErrorCheck()
	{
		if (itsOrbitCam == null)
		{
			return "itsOrbitCam has to be filled in";
		}
		return null;
	}
}
#endregion

#endif