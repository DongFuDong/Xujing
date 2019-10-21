using UnityEngine;
using System.Collections;




public class KGFOrbitCamSettings : KGFObject, KGFIValidator
{
	[HideInInspector]
	public Camera itsPreviewCamera = null; //preview camera used in editor time to preview settings
	
	[System.Serializable]
	public class camera_event_orbiter_settings
	{
		public KGFOrbitCam itsOrbitCam = null;
	}

	[System.Serializable]
	public class camera_event_transform_target_settings
	{
		public GameObject itsTransformTarget = null;
	}



	public camera_event_orbiter_settings itsOrbitCam = new camera_event_orbiter_settings();
	public camera_event_transform_target_settings itsTransformTarget = new camera_event_transform_target_settings();

	public KGFOrbitCam.camera_root_settings itsRoot = new KGFOrbitCam.camera_root_settings();
	public KGFOrbitCam.camera_zoom_settings itsZoom = new KGFOrbitCam.camera_zoom_settings();
	public KGFOrbitCam.camera_rotation_settings itsRotation = new KGFOrbitCam.camera_rotation_settings();
	public KGFOrbitCam.camera_terrain_settings itsEnvironment = new KGFOrbitCam.camera_terrain_settings();
	public KGFOrbitCam.camera_control_settings itsInput = new KGFOrbitCam.camera_control_settings();
	public KGFOrbitCam.camera_lookat_settings itsLookat = new KGFOrbitCam.camera_lookat_settings();
	public KGFOrbitCam.camera_settings itsCamera = new KGFOrbitCam.camera_settings();


	[HideInInspector]
	public bool itsGameStarted = false;

	protected override void Awake()
	{
		base.Awake();
		if(itsPreviewCamera != null)
			Destroy(itsPreviewCamera);
	}
	
	public void Start ()
	{
		itsGameStarted = true;
	}

	public void Update ()
	{
	}


	public void Apply ()
	{
		Vector3 aTransformTargetVector;
		Quaternion aTransformTargetRotation;
		float aTransformTargetAngleX = 0;
		float aTransformTargetAngleY = 0;
		float aZoom = 0;

		if(itsTransformTarget.itsTransformTarget != null && itsRoot.itsRoot != null)
		{
			aTransformTargetVector = -1 * (itsTransformTarget.itsTransformTarget.transform.position - itsRoot.itsRoot.transform.position).normalized;
			aTransformTargetRotation = Quaternion.LookRotation (aTransformTargetVector);
			aTransformTargetAngleX = aTransformTargetRotation.eulerAngles.x;
			aTransformTargetAngleY = -aTransformTargetRotation.eulerAngles.y;
			aZoom = Vector3.Distance(itsTransformTarget.itsTransformTarget.transform.position, itsRoot.itsRoot.transform.position);
		}

		if (itsOrbitCam.itsOrbitCam != null)
		{
			itsOrbitCam.itsOrbitCam.SetCameraFieldOfView(itsCamera.itsFieldOfView);
			
			itsOrbitCam.itsOrbitCam.SetLinkTargetPosition (itsRoot.itsLinkTargetPosition);
			itsOrbitCam.itsOrbitCam.SetLinkTargetRotation (itsRoot.itsLinkTargetRotation);
			itsOrbitCam.itsOrbitCam.SetLinkTargetPositionSpeed (itsRoot.itsLinkTargetPositionSpeed);
			itsOrbitCam.itsOrbitCam.SetLinkTargetRotationSpeed (itsRoot.itsLinkTargetRotationSpeed);
			
			itsOrbitCam.itsOrbitCam.SetMinZoom (itsZoom.itsMinZoom);
			itsOrbitCam.itsOrbitCam.SetMaxZoom (itsZoom.itsMaxZoom);
			if(aZoom != 0)
			{
				itsOrbitCam.itsOrbitCam.SetStartZoom(aZoom);
				itsOrbitCam.itsOrbitCam.SetTargetZoom (aZoom);
			}
			else
			{
				itsOrbitCam.itsOrbitCam.SetStartZoom(itsZoom.itsStartZoom);
				itsOrbitCam.itsOrbitCam.SetTargetZoom (itsZoom.itsStartZoom);
			}
			itsOrbitCam.itsOrbitCam.EnableZoom (itsZoom.itsEnable);
			itsOrbitCam.itsOrbitCam.DisableZoomLimits (itsZoom.itsDisableZoomLimits);
			itsOrbitCam.itsOrbitCam.SetZoomSpeed (itsZoom.itsZoomSpeed);
			

			itsOrbitCam.itsOrbitCam.SetMinRotationLeftRight (itsRotation.itsLeftRight.itsLeftLimit);
			itsOrbitCam.itsOrbitCam.SetMaxRotationLeftRight (itsRotation.itsLeftRight.itsRightLimit);
			itsOrbitCam.itsOrbitCam.SetStartRotationLeftRight(itsRotation.itsLeftRight.itsStartValue - aTransformTargetAngleY);
			itsOrbitCam.itsOrbitCam.SetTargetRotationLeftRight (itsRotation.itsLeftRight.itsStartValue - aTransformTargetAngleY);
			itsOrbitCam.itsOrbitCam.DisableRotationLeftRight (itsRotation.itsLeftRight.itsEnable);
			itsOrbitCam.itsOrbitCam.DisableRotationLeftRightLimits (itsRotation.itsLeftRight.itsUseLimits);

			itsOrbitCam.itsOrbitCam.SetMinRotationUpDown (itsRotation.itsUpDown.itsDownLimit);
			itsOrbitCam.itsOrbitCam.SetMaxRotationUpDown (itsRotation.itsUpDown.itsUpLimit);
			itsOrbitCam.itsOrbitCam.SetStartRotationUpDown(itsRotation.itsUpDown.itsStartValue + aTransformTargetAngleX);
			itsOrbitCam.itsOrbitCam.SetTargetRotationUpDown (itsRotation.itsUpDown.itsStartValue + aTransformTargetAngleX);
			itsOrbitCam.itsOrbitCam.DisableRotationUpDown (itsRotation.itsUpDown.itsEnable);
			itsOrbitCam.itsOrbitCam.DisableRotationUpDownLimits (itsRotation.itsUpDown.itsUseLimits);
			
			
			itsOrbitCam.itsOrbitCam.SetUseQuaternions (itsRotation.itsUseQuaternions);

			itsOrbitCam.itsOrbitCam.TestCollisions (itsEnvironment.itsTestCollisions);
			itsOrbitCam.itsOrbitCam.SetCollisionLayer (itsEnvironment.itsCollisionLayer);
			itsOrbitCam.itsOrbitCam.SetCollisionOffset (itsEnvironment.itsCollisionOffset);
			itsOrbitCam.itsOrbitCam.EnableGyroscope (itsEnvironment.itsFollowGround);
//			itsOrbitCam.itsOrbitCam.UseBoxCollision(itsEnvironment.itsUseBoxCollision);
//			itsOrbitCam.itsOrbitCam.UseRayCollision(itsEnvironment.itsUseRayCollision);

			itsOrbitCam.itsOrbitCam.EnableInput (itsInput.itsEnable);
			itsOrbitCam.itsOrbitCam.InvertYAxis (itsInput.itsInvertYAxis);
			itsOrbitCam.itsOrbitCam.SetXAxisName(itsInput.itsXAxisName);
			itsOrbitCam.itsOrbitCam.SetYAxisName(itsInput.itsYAxisName);
			itsOrbitCam.itsOrbitCam.SetZoomAxisName(itsInput.itsZoomAxisName);
			itsOrbitCam.itsOrbitCam.SetXAxisSensitivity(itsInput.itsXAxisSensitivity);
			itsOrbitCam.itsOrbitCam.SetYAxisSensitivity(itsInput.itsYAxisSensitivity);
			itsOrbitCam.itsOrbitCam.SetXAxisSensitivityTouch(itsInput.itsXAxisSensitivityTouch);
			itsOrbitCam.itsOrbitCam.SetYAxisSensitivityTouch(itsInput.itsYAxisSensitivityTouch);
			itsOrbitCam.itsOrbitCam.SetTwoFingerRotateTouch(itsInput.itsTwoFingerRotateTouch);
			itsOrbitCam.itsOrbitCam.SetZoomAxisSensitivity(itsInput.itsZoomAxisSensitivity);

			itsOrbitCam.itsOrbitCam.EnableLookat(itsLookat.itsEnable);
			itsOrbitCam.itsOrbitCam.SetLookatTarget (itsLookat.itsLookatTarget);
			itsOrbitCam.itsOrbitCam.SetLookatSpeed (itsLookat.itsLookatSpeed);
			itsOrbitCam.itsOrbitCam.SetUpVectorSource (itsLookat.itsUpVectorSource);
			itsOrbitCam.itsOrbitCam.SetHardLinkToTarget(itsLookat.itsHardLinkToTarget);
			
			itsOrbitCam.itsOrbitCam.SetRoot(itsRoot.itsRoot);
		}
	}

	public void CopyDataFromOrbiter()
	{
		/*
		itsRoot.itsRoot = itsOrbiter.itsOrbiter.GetTarget();
		itsRoot.itsLinkTargetPosition = itsOrbiter.itsOrbiter.GetLinkTargetPosition();
		itsRoot.itsLinkTargetPositionSpeed = itsOrbiter.itsOrbiter.GetLinkTargetPositionSpeed();
		itsRoot.itsLinkTargetRotation = itsOrbiter.itsOrbiter.GetLinkTargetRotation();
		itsRoot.itsLinkTargetRotationSpeed = itsOrbiter.itsOrbiter.GetLinkTargetRotationSpeed();

		itsZoom.itsMinZoom = itsOrbiter.itsOrbiter.GetMinZoom();
		itsZoom.itsMaxZoom = itsOrbiter.itsOrbiter.GetMaxZoom();
		itsZoom.itsTargetZoom = itsOrbiter.itsOrbiter.GetTargetZoom();
		itsZoom.itsDisableZoom = itsOrbiter.itsOrbiter.GetDisableZoom();
		itsZoom.itsDisableZoomLimits = itsOrbiter.itsOrbiter.GetDisableZoomLimits();
		itsZoom.itsZoomSpeed = itsOrbiter.itsOrbiter.GetZoomSpeed();

		itsRotation.itsLeftRight.itsLeftLimit = itsOrbiter.itsOrbiter.GetMinRotationLeftRight();
		itsRotation.itsLeftRight.itsRightLimit = itsOrbiter.itsOrbiter.GetMaxRotationLeftRight();
		itsRotation.itsLeftRight.itsTargetValue = itsOrbiter.itsOrbiter.GetTargetRotationLeftRight();
		itsRotation.itsLeftRight.itsEnable = itsOrbiter.itsOrbiter.GetDisableRotationLeftRight();
		itsRotation.itsLeftRight.itsUseLimits = itsOrbiter.itsOrbiter.GetDisableRotationUpDownLimits();

		itsRotation.itsUpDown.itsDownLimit = itsOrbiter.itsOrbiter.GetMinRotationUpDown();
		itsRotation.itsUpDown.itsUpLimit = itsOrbiter.itsOrbiter.GetMaxRotationUpDown();
		itsRotation.itsUpDown.itsTargetValue = itsOrbiter.itsOrbiter.GetTargetRotationUpDown();
		itsRotation.itsUpDown.itsEnable = itsOrbiter.itsOrbiter.GetDisableRotationUpDown();
		itsRotation.itsUpDown.itsUseLimits = itsOrbiter.itsOrbiter.GetDisableRotationUpDownLimits();

		itsRotation.itsUseQuaternions = itsOrbiter.itsOrbiter.GetUseQuaternions();

		itsEnvironment.itsTestCollisions = itsOrbiter.itsOrbiter.GetTestCollisions();
		itsEnvironment.itsCollisionLayer = itsOrbiter.itsOrbiter.GetCollisionLayer();
		itsEnvironment.itsFollowGround = itsOrbiter.itsOrbiter.GetEnableGyroscope();
		
		itsInput.itsEnable = itsOrbiter.itsOrbiter.GetReactToInput();
		itsInput.itsInvertYAxis = itsOrbiter.itsOrbiter.GetInvertYAxis();
		itsInput.itsXAxisSensitivity = itsOrbiter.itsOrbiter.GetXAxisSensitivity();
		itsInput.itsYAxisSensitivity = itsOrbiter.itsOrbiter.GetYAxisSensitivity();
		itsInput.itsZoomAxisSensitivity = itsOrbiter.itsOrbiter.GetZoomAxisSensitivity();
		itsInput.itsXAxisName = itsOrbiter.itsOrbiter.GetXAxisName();
		itsInput.itsYAxisName = itsOrbiter.itsOrbiter.GetYAxisName();
		itsInput.itsZoomAxisName = itsOrbiter.itsOrbiter.GetZoomAxisName();

		itsLookat.itsLookatTarget = itsOrbiter.itsOrbiter.GetLookatTarget();
		itsLookat.itsLookatSpeed = itsOrbiter.itsOrbiter.GetLookatSpeed();
		itsLookat.itsUpVectorSource = itsOrbiter.itsOrbiter.GetUpVectorSource();
		 */

		
	}


	private void PositionPreviewCamera(float theZoom, float theHorizontalRotation, float theVerticalRotation, Vector3 theTargetPosition, Quaternion theTargetRotation)
	{
		if(itsRoot.itsRoot == null)
			return;
		
		float aTranformPointHorizontalAngle = (theHorizontalRotation) * Mathf.PI / 180f;
		float aTranformPointVerticalAngle = (theVerticalRotation) * Mathf.PI / 180f;
		
		Vector3 aTransformPoint = new Vector3 ();
		aTransformPoint.z = theZoom * Mathf.Sin (aTranformPointVerticalAngle) * Mathf.Cos (aTranformPointHorizontalAngle);
		aTransformPoint.x = theZoom * Mathf.Sin (aTranformPointVerticalAngle) * Mathf.Sin (aTranformPointHorizontalAngle);
		aTransformPoint.y = theZoom * Mathf.Cos (aTranformPointVerticalAngle);
		aTransformPoint = theTargetPosition + theTargetRotation * aTransformPoint;
		transform.position = aTransformPoint;
		
		Vector3 aForwardVector = Vector3.zero;
		if(itsLookat.itsEnable && itsLookat.itsLookatTarget != null && itsRoot.itsRoot != null)
		{
			aForwardVector = itsLookat.itsLookatTarget.transform.position - transform.position;
			transform.rotation = Quaternion.LookRotation(aForwardVector.normalized,itsRoot.itsRoot.transform.up);
		}
		else
		{
			aForwardVector = theTargetPosition - transform.position;
			transform.rotation = Quaternion.LookRotation(aForwardVector.normalized,itsRoot.itsRoot.transform.up);
		}
		if(itsPreviewCamera != null)
			itsPreviewCamera.fieldOfView = itsCamera.itsFieldOfView;
		
	}
	
	void OnDrawGizmosSelected ()//DrawDebugRotation ()
	{
		if(itsRoot.itsRoot == null)
			return;
		
		float itsCurrentZoom = itsZoom.itsStartZoom;
		float itsRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		float itsRotationUpDown = itsRotation.itsUpDown.itsStartValue;



		if (itsOrbitCam.itsOrbitCam != null)
		{
			itsCurrentZoom = itsZoom.itsStartZoom;
			itsRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
			itsRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		}

		
		//draw target item to lookat target, or if not available to root target
		Vector3 aTargetGizmoPosition = Vector3.zero;
		if (itsLookat.itsLookatTarget != null/* && itsLookat.itsLookatTarget != itsRoot.itsRoot  && itsLookat.itsEnable*/)
		{
			aTargetGizmoPosition = itsLookat.itsLookatTarget.transform.position;
			Gizmos.DrawIcon (aTargetGizmoPosition, "eye.png", true);
		}
		if (itsRoot.itsRoot != null)
		{
			aTargetGizmoPosition = itsRoot.itsRoot.transform.position;
			Gizmos.DrawIcon (aTargetGizmoPosition, "target_64.png", true);
		}


		
		//Calculate number of points per circle
		int aVerticalPointNumber = (int)(((itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit) - (itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit)) / 5f);
		int aHorizontalPointNumber = (int)(((itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit) - (itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit)) / 5f);
		
		if (!itsRotation.itsUpDown.itsUseLimits)
		{
			aVerticalPointNumber = 36;
		}
		
		if (!itsRotation.itsLeftRight.itsUseLimits)
		{
			aHorizontalPointNumber = 36;
		}
		
		Vector3[] aVerticalPointsArray = new Vector3[aVerticalPointNumber + 1];
		Vector3[] aHorizontalPointsArray = new Vector3[aHorizontalPointNumber + 1];
		
		
		
		float aTargetVerticalAngle = 0f;
		float aTargetHorizontalAngle = 0f;
		Vector3 aTargetPosition = new Vector3 ();
		Quaternion aTargetRotation = Quaternion.identity;
		float aVerticalAngle = 0;
		float aHorizontalAngle = 0;
		Vector3 aPoint = new Vector3 ();
		float aLineLength = 1f;
		Vector3 aTargetVector = Vector3.zero;
		Vector3 aTargetPoint = Vector3.zero;
		
		
		//if link target enabled, add rotation and position to calculation
		if (itsRoot.itsRoot != null)
		{
			aTargetPosition = itsRoot.itsRoot.transform.position;
		}
		else
		{
			aTargetPosition = Vector3.zero;
		}
		
		
		if (itsRoot.itsRoot != null)
		{
			aTargetRotation = itsRoot.itsRoot.transform.localRotation;
		}
		
		PositionPreviewCamera(itsCurrentZoom,itsRotationLeftRight + aTargetHorizontalAngle,(itsRotation.itsUpDown.itsStartValue) + 270f,aTargetPosition,aTargetRotation);
		
		//only draw circle if vertical rotation allowed
		if (itsRotation.itsUpDown.itsEnable)
		{
			for (int i = 0; i < aVerticalPointNumber+1; i++)
			{
				if (itsRotation.itsUpDown.itsUseLimits)
				{
					//calculate angles
					aVerticalAngle = ((itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit) + 270f + ((itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit) - (itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit)) / ((float)aVerticalPointNumber) * i) * Mathf.PI / 180f;
				}
				else
				{
					aVerticalAngle = (0 + 270f + (360) / ((float)aVerticalPointNumber) * i) * Mathf.PI / 180f;
				}
				//horizontal angle is current horizontal rotation
				aHorizontalAngle = (itsRotationLeftRight + aTargetHorizontalAngle) * Mathf.PI / 180f;
				
				//Calcualte point on sphere around target with zoom as radius
				aPoint = new Vector3 ();
				aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos (aHorizontalAngle);
				aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin (aHorizontalAngle);
				aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
				
				aPoint = aTargetPosition + aTargetRotation * aPoint;
				
				aVerticalPointsArray [i] = aPoint;
			}
			
			//Draw circle
			for (int i = 0; i < aVerticalPointNumber -0; i++)
			{
				Gizmos.color = new Color (1f, 0f, 0f);
				Gizmos.DrawLine (aVerticalPointsArray [i], aVerticalPointsArray [i + 1]);
				
			}
			
			//Draw small line from circle start to target
			aTargetVector = (aTargetPosition - aVerticalPointsArray [0]).normalized;
			aTargetPoint = aVerticalPointsArray [0] + aTargetVector * aLineLength;
			Gizmos.DrawLine (aVerticalPointsArray [0], aTargetPoint);
			
			//Draw small line from circle end to target
			aTargetVector = (aTargetPosition - aVerticalPointsArray [aVerticalPointNumber]).normalized;
			aTargetPoint = aVerticalPointsArray [aVerticalPointNumber] + aTargetVector * aLineLength;
			Gizmos.DrawLine (aVerticalPointsArray [aVerticalPointNumber], aTargetPoint);
			
			
			//Calculate point on the middle of the circle
			aHorizontalAngle = (itsRotationLeftRight + aTargetHorizontalAngle) * Mathf.PI / 180f;
			aPoint.z = itsCurrentZoom * Mathf.Sin ((itsRotation.itsUpDown.itsStartValue + 270) / 180f * Mathf.PI) * Mathf.Cos (aHorizontalAngle);
			aPoint.x = itsCurrentZoom * Mathf.Sin ((itsRotation.itsUpDown.itsStartValue + 270) / 180f * Mathf.PI) * Mathf.Sin (aHorizontalAngle);
			aPoint.y = itsCurrentZoom * Mathf.Cos ((itsRotation.itsUpDown.itsStartValue + 270) / 180f * Mathf.PI);
			
			aPoint = aTargetPosition + aTargetRotation * aPoint;
			
			//Draw small line from circle center to target
			aTargetVector = (aTargetPosition - aPoint).normalized;
			aTargetPoint = aPoint + aTargetVector * aLineLength;
			Gizmos.DrawLine (aPoint, aTargetPoint);
		}
		
		
		
		//only draw circle if horizontal rotation allowed
		if (itsRotation.itsLeftRight.itsEnable)
		{
			
			for (int i = 0; i < aHorizontalPointNumber+1; i++)
			{
				//Vertical angle is current vertical rotation
				aVerticalAngle = (90 - itsRotationUpDown + aTargetVerticalAngle) * Mathf.PI / 180f;
				
				if (itsRotation.itsLeftRight.itsUseLimits)
				{
					//calculate angles
					aHorizontalAngle = (270 - ((itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit) + ((itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit) - (itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit)) / ((float)aHorizontalPointNumber) * i)) * Mathf.PI / 180f;
				}
				else
				{
					aHorizontalAngle = (270 - (360) / ((float)aHorizontalPointNumber) * i) * Mathf.PI / 180f;
				}
				
				//Calculate point on sphere wieh current zoom as radius
				aPoint = new Vector3 ();
				
				aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos (aHorizontalAngle);
				aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin (aHorizontalAngle);
				aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
				
				aPoint = aTargetPosition + aTargetRotation * aPoint;
				
				aHorizontalPointsArray [i] = aPoint;
			}
			
			//Draw cirlce
			for (int i = 0; i < aHorizontalPointNumber - 0; i++)
			{
				Gizmos.color = new Color (0f, 1f, 0f);
				Gizmos.DrawLine (aHorizontalPointsArray [i], aHorizontalPointsArray [i + 1]);
			}
			
			//Draw Line from start point of circle to target
			aTargetVector = (aTargetPosition - aHorizontalPointsArray [0]).normalized;
			aTargetPoint = aHorizontalPointsArray [0] + aTargetVector * aLineLength;
			Gizmos.DrawLine (aHorizontalPointsArray [0], aTargetPoint);
			
			//Draw Line from end point of circle to target
			aTargetVector = (aTargetPosition - aHorizontalPointsArray [aHorizontalPointNumber]).normalized;
			aTargetPoint = aHorizontalPointsArray [aHorizontalPointNumber] + aTargetVector * aLineLength;
			Gizmos.DrawLine (aHorizontalPointsArray [aHorizontalPointNumber], aTargetPoint);
			
			//Calculate point on center of circle
			aVerticalAngle = (90 - itsRotationUpDown) * Mathf.PI / 180f;
			aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos ((270 - itsRotation.itsLeftRight.itsStartValue) / 180f * Mathf.PI);
			aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin ((270 - itsRotation.itsLeftRight.itsStartValue) / 180f * Mathf.PI);
			aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
			
			aPoint = aTargetPosition + aTargetRotation * aPoint;
			
			//Draw line from center of cirlce to target
			aTargetVector = (aTargetPosition - aPoint).normalized;
			aTargetPoint = aPoint + aTargetVector * aLineLength;
			Gizmos.DrawLine (aPoint, aTargetPoint);
			
		}
		
		
		//Only draw line if zoom enabled
		if (itsZoom.itsEnable)
		{
			aTargetVector = (itsRoot.itsRoot.transform.position - transform.position).normalized;
			Vector3 aStartPoint = aTargetPosition + itsZoom.itsMinZoom * aTargetVector;
			Vector3 aEndPoint = aTargetPosition + itsZoom.itsMaxZoom * aTargetVector;
			Gizmos.color = new Color (0, 0, 1);
			
			
			aVerticalAngle = (90f - itsRotationUpDown + aTargetVerticalAngle) * Mathf.PI / 180f;
			aHorizontalAngle = (itsRotationLeftRight + 180f + aTargetHorizontalAngle) * Mathf.PI / 180f;
			
			aPoint = new Vector3 ();
			aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos (aHorizontalAngle);
			aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin (aHorizontalAngle);
			aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
			
			
			Quaternion aTargetRotation2 = Quaternion.Euler (aVerticalAngle, 0f, 0f);
			Quaternion aFinalRotation = aTargetRotation;
			aPoint = aTargetPosition + aFinalRotation * aPoint;
			
			aTargetVector = (aPoint - aTargetPosition).normalized;
			aStartPoint = aTargetPosition + itsZoom.itsMinZoom * aTargetVector;
			aEndPoint = aTargetPosition + itsZoom.itsStartZoom * aTargetVector;
			//Gizmos.DrawIcon (aEndPoint, "camera_small.png", true);

			if (!itsZoom.itsDisableZoomLimits)
			{
				//if limits enabled draw line from zoom min to zoom max
				//aPoint = transform.position;
				aTargetVector = (aPoint - aTargetPosition).normalized;
				aStartPoint = aTargetPosition + itsZoom.itsMinZoom * aTargetVector;
				aEndPoint = aTargetPosition + itsZoom.itsMaxZoom * aTargetVector;
			}
			else
			{
				//Draw "infinite" line if limits disabled
				aTargetVector = (aPoint - aTargetPosition).normalized;
				aStartPoint = aTargetPosition;
				aEndPoint = aTargetPosition + 1000 * aTargetVector;
			}
			
			Gizmos.DrawLine (aStartPoint, aEndPoint);
			
			
			if (!itsZoom.itsDisableZoomLimits)
			{
				//Calculate normals for zoom line
				aVerticalAngle = (itsRotationUpDown + aTargetVerticalAngle) * Mathf.PI / 180f;
				aHorizontalAngle = (itsRotationLeftRight + aTargetHorizontalAngle) * Mathf.PI / 180f;
				
				aPoint = new Vector3 ();
				aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos (aHorizontalAngle);
				aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin (aHorizontalAngle);
				aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
				
				//rotate line around vertical angle
				aTargetRotation2 = Quaternion.Euler (aVerticalAngle, 0f, 0f);
				aFinalRotation = aTargetRotation * aTargetRotation2;
				aPoint = aTargetPosition + aFinalRotation * aPoint;
				
				//calculate final normal vector
				aTargetVector = (aPoint - aTargetPosition).normalized;
				
				//Draw normal line at min zoom point
				Vector3 aLineStartPoint = aStartPoint + aLineLength * aTargetVector * 0.5f;
				Vector3 aLineEndPoint = aStartPoint - aLineLength * aTargetVector * 0.5f;
				Gizmos.DrawLine (aLineStartPoint, aLineEndPoint);
				
				//Draw normal line at max zoom point
				aLineStartPoint = aEndPoint + aLineLength * aTargetVector * 0.5f;
				aLineEndPoint = aEndPoint - aLineLength * aTargetVector * 0.5f;
				Gizmos.DrawLine (aLineStartPoint, aLineEndPoint);
			}
			
		}
		
		
		//Draw line to lookat target
		if (itsLookat.itsLookatTarget != null/* && itsLookat.itsEnable*/)
		{
			aTargetVector = (transform.position - aTargetPosition).normalized;
			Vector3 aStartPoint = aTargetPosition + itsZoom.itsMinZoom * aTargetVector;
			Vector3 aEndPoint = aTargetPosition + itsZoom.itsMaxZoom * aTargetVector;
			Gizmos.color = new Color (0, 0, 1);
			
			
			aVerticalAngle = (90f - itsRotationUpDown + aTargetVerticalAngle) * Mathf.PI / 180f;
			aHorizontalAngle = (itsRotationLeftRight + 180f + aTargetHorizontalAngle) * Mathf.PI / 180f;
			
			aPoint = new Vector3 ();
			aPoint.z = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Cos (aHorizontalAngle);
			aPoint.x = itsCurrentZoom * Mathf.Sin (aVerticalAngle) * Mathf.Sin (aHorizontalAngle);
			aPoint.y = itsCurrentZoom * Mathf.Cos (aVerticalAngle);
			
			Quaternion aFinalRotation = aTargetRotation;
			aPoint = aTargetPosition + aFinalRotation * aPoint;
			
			aTargetVector = (aPoint - itsLookat.itsLookatTarget.transform.position).normalized;
			aStartPoint = itsLookat.itsLookatTarget.transform.position;
			aEndPoint = aPoint;
			Gizmos.color = new Color(1,1,0);
			Gizmos.DrawLine (aStartPoint, aEndPoint);
		}
	}

	#region validate
	
	public KGFMessageList Validate ()
	{
		KGFMessageList aMessageList = new KGFMessageList ();

		//Orbiter
		if (itsOrbitCam.itsOrbitCam == null)
		{
			aMessageList.AddError ("itsOrbitCam should not be empty");
		}

		//Root
		if (itsRoot.itsRoot == null)
		{
			aMessageList.AddError ("itsRoot should not be empty");
		}
		if (itsRoot.itsRoot == this.gameObject)
		{
			aMessageList.AddError ("itsRoot cannot be th KGFOrbitCamSetting itself");
		}
		if (itsRoot.itsLinkTargetPositionSpeed <= 0)
		{
			aMessageList.AddError ("itsLinkedTargetPositionSpeed has invalid value, has to be > 0");
		}
		if (itsRoot.itsLinkTargetRotationSpeed <= 0)
		{
			aMessageList.AddError ("itsLinkedTargetRotationSpeed has invalid value, has to be > 0");
		}
		
		//Lookat
		if (itsLookat.itsLookatSpeed <= 0)
		{
			aMessageList.AddError ("itsLookatSpeed has invalid value, has to be > 0");
		}
		if (itsLookat.itsEnable)
		{
			if(itsLookat.itsLookatTarget == null)
			{
				aMessageList.AddError ("itsLookat is Enabled but Lookat target has not been assigned");
			}
			if(itsLookat.itsUpVectorSource == null)
			{
				aMessageList.AddError ("itsLookat is Enabled but up vector source has not been assigned");
			}
		}

		
		//Zoom
		if (itsZoom.itsZoomSpeed <= 0)
		{
			aMessageList.AddError ("itsZoomSpeed has invalid value, has to be > 0");
		}
		if (itsZoom.itsStartZoom < itsZoom.itsMinZoom)
		{
			aMessageList.AddError ("itsStartZoom should not be smaller than itsMinZoom");
		}
		if (itsZoom.itsStartZoom > itsZoom.itsMaxZoom)
		{
			aMessageList.AddError ("itsStartZoom should not be bigger than itsMaxZoom");
		}
		if (itsZoom.itsMinZoom > itsZoom.itsMaxZoom)
		{
			aMessageList.AddError ("itsMinZoom should not be bigger than itsMaxZoom");
		}
		if (itsZoom.itsMaxZoom < itsZoom.itsMinZoom)
		{
			aMessageList.AddError ("itsMaxZoon should not be smaller than itsMinZoom");
		}
		if (itsZoom.itsMinZoom < 0)
		{
			aMessageList.AddError ("itsMinZoon should not be smaller than 0");
		}
		
		//Rotation Up Down
		if (itsRotation.itsUpDown.itsDownLimit < 0)
		{
			aMessageList.AddError ("itsRotation itsUpDown itsDownLimit should not be smaller than 0");
		}
		if (itsRotation.itsUpDown.itsUpLimit < 0)
		{
			aMessageList.AddError ("itsRotation itsUpDown itsUpLimit should not be smaller than 0");
		}
		
		//Rotation Left Right
		if (itsRotation.itsLeftRight.itsLeftLimit < 0)
		{
			aMessageList.AddError ("itsRotation itsLeftRight itsLeftLimit should not be smaller than 0");
		}
		if (itsRotation.itsLeftRight.itsRightLimit < 0)
		{
			aMessageList.AddError ("itsRotation itsLeftRight itsRightLimit should not be smaller than 0");
		}
		
		//Control
		if (itsInput.itsXAxisSensitivity < 0)
		{
			aMessageList.AddError ("itsInput itsXAxisSensitivity should not be smaller than 0");
		}
		if (itsInput.itsYAxisSensitivity < 0)
		{
			aMessageList.AddError ("itsInput itsYAxisSensitivity should not be smaller than 0");
		}
		if (itsInput.itsXAxisSensitivityTouch < 0)
		{
			aMessageList.AddError ("itsInput itsXAxisSensitivityTouch should not be smaller than 0");
		}
		if (itsInput.itsYAxisSensitivityTouch < 0)
		{
			aMessageList.AddError ("itsInput itsYAxisSensitivityTouch should not be smaller than 0");
		}
		if (itsInput.itsZoomAxisSensitivity < 0)
		{
			aMessageList.AddError ("itsInput itsZoomAxisSensitivity should not be smaller than 0");
		}
		

		return aMessageList;
	}
	
	#endregion
}
