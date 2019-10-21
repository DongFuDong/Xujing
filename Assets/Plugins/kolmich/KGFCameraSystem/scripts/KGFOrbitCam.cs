using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// The KGFOrbitCam class. This class implements an orbiter that can be used to control a unity3d camera.
/// </summary>
public class KGFOrbitCam : KGFObject, KGFIValidator
{
	#region data classes
	[System.Serializable]
	public class camera_root_settings
	{
		public GameObject itsRoot;
		public bool itsLinkTargetPosition = true;
		public float itsLinkTargetPositionSpeed = 2.0f;
		public bool itsLinkTargetRotation = true;
		public float itsLinkTargetRotationSpeed = 2.0f;
	}
	
	[System.Serializable]
	public class camera_zoom_settings
	{
		public bool itsEnable = false;
		public bool itsDisableZoomLimits = true;
		public float itsStartZoom = 200f;
		public float itsMinZoom = 130f;
		public float itsMaxZoom = 300f;
		public float itsZoomSpeed = 150f;
		
	}
	
	[System.Serializable]
	public class camera_rotation_settings
	{
		[System.Serializable]
		public class up_down_settings
		{
			public bool itsEnable = true;
			public float itsStartValue = 0.0f;
			public bool itsUseLimits = true;
			public float itsUpLimit = 30.0f;
			public float itsDownLimit = 30.0f;
		}

		[System.Serializable]
		public class left_right_settings
		{
			public bool itsEnable = true;
			public float itsStartValue = 0.0f;
			public bool itsUseLimits = true;
			public float itsLeftLimit = 45.0f;
			public float itsRightLimit = 45.0f;
		}

		public left_right_settings itsLeftRight = new left_right_settings();
		public up_down_settings itsUpDown = new up_down_settings();
		public bool itsUseQuaternions;
	}
	
	[System.Serializable]
	public class camera_terrain_settings
	{
		public bool itsFollowGround;
		public bool itsTestCollisions;
		public LayerMask itsCollisionLayer;
		public float itsCollisionOffset = 0.1f;
		//private bool itsUseRayCollision = true;
		//public bool itsUseBoxCollision = true;
	}
	
	[System.Serializable]
	public class camera_control_settings
	{
		public bool itsEnable;
		public bool itsInvertYAxis;
		public string itsXAxisName = "Mouse X";
		public float itsXAxisSensitivity = 2.0f;
		public string itsYAxisName = "Mouse Y";
		public float itsYAxisSensitivity = 2.0f;
		public string itsZoomAxisName = "Mouse ScrollWheel";
		public float itsZoomAxisSensitivity = 2.0f;
		
		public float itsXAxisSensitivityTouch = 2.0f;
		public float itsYAxisSensitivityTouch = 2.0f;
		public bool itsTwoFingerRotateTouch = false;

	}
	
	[System.Serializable]
	public class camera_damping_settings
	{
		public bool itsEnableInertia = false;
		public float itsIntertiaLeftRight = 0.3f;
		public float itsIntertiaUpDown = 0.3f;
		public bool itsEnableOverShoot = false;
		public float itsOverShootCorrectionSpeed = 1.0f;
	}
	
	[System.Serializable]
	public class camera_lookat_settings
	{
		public bool itsEnable = false;
		public GameObject itsLookatTarget;
		public GameObject itsUpVectorSource;
		public float itsLookatSpeed = 2.0f;
		public bool itsHardLinkToTarget = false;
	}
	
	[System.Serializable]
	public class camera_settings
	{
		[HideInInspector]
		public Camera itsCamera = null;
		public float itsFieldOfView = 60.0f;
	}
	#endregion

	#region public events
	/// <summary>
	/// This event will get triggered immediately
	/// </summary>
	public KGFDelegate EventRootChanged = new KGFDelegate();
	
	/// <summary>
	/// This event will get triggered if the root changed and the orbit cam reached the new root
	/// If the new root is moving and the orbit cam is not fast enough to follow its new root it may never reach it. So it
	/// is possible that this event will never be triggered while trying to reach the root.
	/// </summary>
	public KGFDelegate EventRootReached = new KGFDelegate();
	
	/// <summary>
	/// This event gets triggered if an objects starts or stops intersecting the line of sight between orbiter root and the camera.
	/// It can be used for example to make objects transparant that will block the line of sight.
	/// </summary>
	public KGFDelegate EventIntersectorsChanged = new KGFDelegate();
	#endregion
	
	#region public members
	public camera_root_settings itsRoot = new camera_root_settings();
	public camera_zoom_settings itsZoom = new camera_zoom_settings();
	public camera_rotation_settings itsRotation = new camera_rotation_settings();
	public camera_terrain_settings itsEnvironment = new camera_terrain_settings();
	public camera_control_settings itsInput = new camera_control_settings();
	public camera_damping_settings itsInputDamping = new camera_damping_settings();
	public camera_lookat_settings itsLookat = new camera_lookat_settings();
	public camera_settings itsCamera = new camera_settings();
	#endregion

	#region private members

	//hidden root object to look at during transitions
	private Vector3 itsCurrentLookatPosition;
	
	//camera values
	private float itsFieldOfViewVelocity;

	//rotation & zoom influenced by input
	private float itsCurrentRotationLeftRight;
	private float itsTargetRotationLeftRight;
	
	private float itsInputRotationLeftRight;
	private float itsInputRotationLeftRightVelocity;
	
	private float itsInputRotationUpDown;
	private float itsInputRotationUpDownVelocity;
	
	
	private float itsCurrentRotationUpDown;
	private float itsTargetRotationUpDown;
	private float itsCurrentZoom;
	private float itsTargetZoom;
	private float itsCollisionZoom;

	//mouse rotation speed
	private float itsLeftRightRotationSpeed = 1.0f;
	private float itsUpDownRotationSpeed = 1.0f;

	//gyroscope
	private Vector3 itsTargetGroundNormal;
	private Vector3 itsCurrentGroundNormal;

	//link position
	private Vector3 itsCurrentLinkPosition = new Vector3();
	private Vector3 itsLinkPositionVelocity = new Vector3();
	private Vector3 itsLinkLookatVelocity = new Vector3();
	//link rotation
	private Vector3 itsCurrentLinkRotation= new Vector3();
	private Vector3 itsLinkRotationVelocity = new Vector3();
	
	//speed for input damping
	private float itsTargetRotationLeftRightVelocityPos;
	private float itsTargetRotationLeftRightVelocityNeg;
	private float itsTargetRotationUpDownVelocityPos;
	private float itsTargetRotationUpDownVelocityNeg;
	private float itsTargetZoomVelocity;

	//Quaternions to calculate rotation from euler angles
	private Quaternion itsRotationQuaternion;
	private Quaternion itsRotationHorizontal;
	private Quaternion itsRotationVertical;
	private Quaternion itsTargetRotationHorizontal;
	private Quaternion itsTargetRotationVertical;

	//input rotation
	private float itsRotationLeftRight;
	private float itsRotationUpDown;

	//target link position
	private Vector3 itsLinkTargetStartPosition;
	//target rotation
	private Quaternion itsLinkRotationQuaternion;


	//collider
	private GameObject itsColliderObject;
	private bool itsCollision = false;
	//private BoxCollider itsInnerBoxCollider;
	//private BoxCollider itsOuterBoxCollider;
	//private Rigidbody itsRigidBody;
	//private bool itsBoxCollision = false;
	//private Vector3 itsBoxCollisionNormal = Vector3.zero;
	//private float itsBoxCollisionZoom = 0;
	//private float itsCurrentBoxCollisionZoom = 0;
	//private Vector3 itsCurrentBoxCollisionNormal = Vector3.zero;
	//private bool itsInnerBoxCollision= false;
	//private bool itsOuterBoxCollision= false;
	//private Transform itsTransform = null;
	private Transform itsRootTranform = null;
	
	//this member always stores the old root when it gets changed to a new one.
	private GameObject itsRootOld;
	private Transform itsRootOldTranform = null;
	
	/// <summary>
	/// This vector is used to interpolate the up vector when changing between roots.
	/// </summary>
	private Vector3 itsRootUp;
	
	/// <summary>
	/// Used for lerping the up vector while transiting
	/// </summary>
	private Vector3 itsitsLinkUpVelocity;
	
	
	private Transform itsLookatTransform = null;
	
	private float itsLastCollisionZoom;


	//check if in Editor or game mode for gizmo drawing
	private bool itsEditor = true;

	//normal vectors to add to collision
	private Vector3 itsNormalVector = Vector3.zero;
//	private Vector3 itsFinalNormalVector = Vector3.zero;
	private Vector3 itsOriginalPosition;


	private GameObject itsTransformTarget = null;

	private Quaternion itsCurrentLookatRotation;
	private Quaternion itsTargetLookatRotation;
	
	/// <summary>
	/// This member is used to store gameobjects that are blocking the line of sight
	/// </summary>
	private RaycastHit[] itsObjectInLineOfSight = null;

	#endregion

	protected override void KGFAwake()
	{
		base.KGFAwake();
		//itsTransform = transform;
		if(itsLookat.itsLookatTarget != null)
		{
			itsLookatTransform = itsLookat.itsLookatTarget.transform;
		}
		if(itsCamera.itsCamera == null)
		{
			itsCamera.itsCamera = GetComponentInChildren<Camera>();
			itsCamera.itsFieldOfView = itsCamera.itsCamera.fieldOfView;
		}
		
		itsRootTranform = itsRoot.itsRoot.transform;
	}

	/// <summary>
	/// Default unity3d start method. Used for initialisation
	/// </summary>
	public void Start ()
	{
		itsEditor = false;
		ApplyStartValues();
		if (itsRoot.itsRoot != null)
		{
			SetRoot(itsRoot.itsRoot);
		}
		else
		{
			itsLinkTargetStartPosition = new Vector3 (0, 0, 0);
		}
		if (itsLookat.itsEnable)
		{
			if(itsLookatTransform != null)
			{
				itsCurrentLookatPosition = itsLookatTransform.position;
			}
			if(itsLookat.itsUpVectorSource != null)
			{
				itsTargetLookatRotation = Quaternion.LookRotation ((itsCurrentLookatPosition - transform.position).normalized, itsLookat.itsUpVectorSource.transform.up);
			}
		}
		else
		{
			if(itsRootTranform != null)
			{
				itsCurrentLookatPosition = itsRootTranform.position;
				itsTargetLookatRotation = Quaternion.LookRotation ((itsCurrentLookatPosition - transform.position).normalized, itsRootTranform.up);
			}
		}
		itsCurrentLookatRotation = itsTargetLookatRotation;
		LateUpdate();
		LookAtTarget(true);
	}
	
	private void ApplyStartValues()
	{
		//initialize rotation and zoom, set target to start values
		itsRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		itsTargetRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		itsInputRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		itsRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		itsTargetRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		itsInputRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		itsCurrentZoom = itsZoom.itsStartZoom;
		itsTargetZoom = itsZoom.itsStartZoom;
		itsCollisionZoom = itsZoom.itsStartZoom;
		itsTargetRotationHorizontal = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
		itsRotationHorizontal = itsTargetRotationHorizontal;
		itsTargetRotationVertical = Quaternion.Euler (itsTargetRotationUpDown, 0f, 0f);
		itsRotationVertical = itsTargetRotationVertical;
		if(itsRoot.itsRoot != null)
		{
			itsCurrentLinkPosition = itsRoot.itsRoot.transform.position;
			itsCurrentLinkRotation = itsRoot.itsRoot.transform.eulerAngles;
		}
		
		if(itsCamera.itsCamera != null)
		{
			if(Application.isPlaying)
				itsCamera.itsFieldOfView = itsCamera.itsCamera.fieldOfView;
			else
				itsCamera.itsCamera.fieldOfView = itsCamera.itsFieldOfView;	//in editor mode show target fov immediately.
		}
	}

	/// <summary>
	/// 
	/// </summary>
//	void FixedUpdate ()
//	{
//		itsBoxCollision = false;
//		itsInnerBoxCollision = false;
//		itsOuterBoxCollision = false;
//	}
	
	/// <summary>
	/// This method is checking if the new root target is reached by the orbiter.
	/// If so the old root is set to the current root and the event for reaching the new root is triggered.
	/// </summary>
	private void CheckDistanceToRoot()
	{
		if(itsRoot.itsRoot == null)
			return;
		if(Vector3.Distance(itsCurrentLinkPosition,itsRoot.itsRoot.transform.position) < 0.01f && itsRootOld != itsRoot.itsRoot)
		{
			itsRootOld = itsRoot.itsRoot;
			itsRootOldTranform = itsRootOld.transform;
			if(EventRootReached != null)
			{
				EventRootReached.Trigger(this);
			}
		}
	}
	
	public bool GetIsRootChanging()
	{
		return (itsRootOld != itsRoot.itsRoot);
	}
	
	/// <summary>
	/// This methods checks if the input is not overshooting the given limits.
	/// If so there is a smoothdamping enabled that will correct the overshoot again.
	/// </summary>
	private void HandleLimits()
	{
		//prevent overshoot
		if (itsRotation.itsLeftRight.itsUseLimits)
		{
			if (itsInputRotationLeftRight > itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit)
			{
				if(itsInputDamping.itsEnableOverShoot)
				{
					float aDifference = Math.Abs(itsInputRotationLeftRight - (itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit));
					itsInputRotationLeftRight = Mathf.SmoothDamp(itsInputRotationLeftRight, itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit, ref itsTargetRotationLeftRightVelocityPos,1.0f/(itsInputDamping.itsOverShootCorrectionSpeed*aDifference));
				}
				else
				{
					itsInputRotationLeftRight = itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit;
				}
			}
			if (itsInputRotationLeftRight < itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit)
			{
				if(itsInputDamping.itsEnableOverShoot)
				{
					float aDifference = Math.Abs(itsInputRotationLeftRight - (itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit));
					itsInputRotationLeftRight = Mathf.SmoothDamp(itsInputRotationLeftRight, itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit, ref itsTargetRotationLeftRightVelocityNeg,1.0f/(itsInputDamping.itsOverShootCorrectionSpeed*aDifference));
				}
				else
				{
					itsInputRotationLeftRight = itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit;
				}
			}
		}
		if (itsRotation.itsUpDown.itsUseLimits)
		{
			if (itsInputRotationUpDown > itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit)
			{
				if(itsInputDamping.itsEnableOverShoot)
				{
					float aDifference = Math.Abs(itsInputRotationUpDown - (itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit));
					itsInputRotationUpDown = Mathf.SmoothDamp(itsInputRotationUpDown, itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit, ref itsTargetRotationUpDownVelocityPos,1.0f/(itsInputDamping.itsOverShootCorrectionSpeed*aDifference));
				}
				else
				{
					itsInputRotationUpDown = itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit;
				}
			}
			if (itsInputRotationUpDown < itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit)
			{
				if(itsInputDamping.itsEnableOverShoot)
				{
					float aDifference = Math.Abs(itsInputRotationUpDown - (itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit));
					itsInputRotationUpDown = Mathf.SmoothDamp(itsInputRotationUpDown, itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit, ref itsTargetRotationUpDownVelocityNeg,1.0f/(itsInputDamping.itsOverShootCorrectionSpeed*aDifference));
				}
				else
				{
					itsInputRotationUpDown = itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit;
				}
			}
		}
		if (!itsZoom.itsDisableZoomLimits)
		{
			if (itsTargetZoom > itsZoom.itsMaxZoom)
			{
				float aDifference = Math.Abs(itsTargetZoom - itsZoom.itsMaxZoom);
				itsTargetZoom = Mathf.SmoothDamp(itsTargetZoom, itsZoom.itsMaxZoom, ref itsTargetZoomVelocity,0.1f/aDifference);
			}
			if (itsTargetZoom < itsZoom.itsMinZoom)
			{
				float aDifference = Math.Abs(itsTargetZoom - itsZoom.itsMinZoom);
				itsTargetZoom = Mathf.SmoothDamp(itsTargetZoom, itsZoom.itsMinZoom, ref itsTargetZoomVelocity,0.1f/aDifference);
			}
		}
	}
	
	/// <summary>
	/// This method is doing all the transformation calculations
	/// </summary>
	public virtual void LateUpdate()
	{
		CheckDistanceToRoot();
		//#if (!UNITY_IPHONE && !UNITY_ANDROID) || UNITY_EDITOR
		itsLeftRightRotationSpeed = itsInput.itsXAxisSensitivity;
		itsUpDownRotationSpeed = itsInput.itsYAxisSensitivity;
		//#endif
		//#if (UNITY_IPHONE || UNITY_ANDROID) && ! UNITY_EDITOR
		itsLeftRightRotationSpeed = itsInput.itsXAxisSensitivityTouch;
		itsUpDownRotationSpeed = itsInput.itsYAxisSensitivityTouch;
		//#endif
		
		if (itsInput.itsEnable)
		{
			//#if (!UNITY_IPHONE && !UNITY_ANDROID) || UNITY_EDITOR
			ReactToInput();
			//#endif
			//#if (UNITY_IPHONE || UNITY_ANDROID) && ! UNITY_EDITOR
			ReactToInputTouch();
			//#endif
		}
		
		HandleLimits();
		
		if(itsInputDamping.itsEnableInertia)
		{
			itsTargetRotationLeftRight = Mathf.SmoothDamp(itsTargetRotationLeftRight,itsInputRotationLeftRight,ref itsInputRotationLeftRightVelocity,itsInputDamping.itsIntertiaLeftRight);
			itsTargetRotationUpDown = Mathf.SmoothDamp(itsTargetRotationUpDown,itsInputRotationUpDown,ref itsInputRotationUpDownVelocity,itsInputDamping.itsIntertiaUpDown);
		}
		else
		{
			itsTargetRotationLeftRight = itsInputRotationLeftRight;
			itsTargetRotationUpDown = itsInputRotationUpDown;
		}
		
		if (itsRoot.itsRoot != null)
		{
			//Get ground vector angle
			float anAngle = 0;
			if (itsEnvironment.itsFollowGround)
			{
				GetGroundVector ();
				Vector3 aForward = transform.forward;
				aForward.y = 0;
				anAngle = Vector3.Dot (aForward, itsCurrentGroundNormal) * 90.0f;
			}
			

			//follow target position
			if (itsRoot.itsLinkTargetPosition && itsRoot.itsRoot != null)
			{
				itsCurrentLinkPosition.x = Mathf.SmoothDamp (itsCurrentLinkPosition.x, itsRootTranform.position.x, ref itsLinkPositionVelocity.x, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLinkPosition.y = Mathf.SmoothDamp (itsCurrentLinkPosition.y, itsRootTranform.position.y, ref itsLinkPositionVelocity.y, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLinkPosition.z = Mathf.SmoothDamp (itsCurrentLinkPosition.z, itsRootTranform.position.z, ref itsLinkPositionVelocity.z, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
			}
			else
			{
				itsCurrentLinkPosition.x = Mathf.SmoothDamp (itsCurrentLinkPosition.x, itsLinkTargetStartPosition.x, ref itsLinkPositionVelocity.x, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLinkPosition.y = Mathf.SmoothDamp (itsCurrentLinkPosition.y, itsLinkTargetStartPosition.y, ref itsLinkPositionVelocity.y, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLinkPosition.z = Mathf.SmoothDamp (itsCurrentLinkPosition.z, itsLinkTargetStartPosition.z, ref itsLinkPositionVelocity.z, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
			}

			//follow target rotation
			if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
			{
				itsCurrentLinkRotation.x = Mathf.SmoothDampAngle (itsCurrentLinkRotation.x, itsRootTranform.eulerAngles.x, ref itsLinkRotationVelocity.x, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
				itsCurrentLinkRotation.y = Mathf.SmoothDampAngle (itsCurrentLinkRotation.y, itsRootTranform.eulerAngles.y, ref itsLinkRotationVelocity.y, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
				itsCurrentLinkRotation.z = Mathf.SmoothDampAngle (itsCurrentLinkRotation.z, itsRootTranform.eulerAngles.z, ref itsLinkRotationVelocity.z, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
			}
			else
			{
				itsCurrentLinkRotation.x = Mathf.SmoothDampAngle (itsCurrentLinkRotation.x, 0, ref itsLinkRotationVelocity.x, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
				itsCurrentLinkRotation.y = Mathf.SmoothDampAngle (itsCurrentLinkRotation.y, 0, ref itsLinkRotationVelocity.y, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
				itsCurrentLinkRotation.z = Mathf.SmoothDampAngle (itsCurrentLinkRotation.z, 0, ref itsLinkRotationVelocity.z, 1.0f / itsRoot.itsLinkTargetRotationSpeed);
			}
			//adapt field of view
			if(itsCamera.itsCamera != null)
				itsCamera.itsCamera.fieldOfView = Mathf.SmoothDamp(itsCamera.itsCamera.fieldOfView,itsCamera.itsFieldOfView, ref itsFieldOfViewVelocity, 1.0f /5.0f);
			
			if (itsRotationLeftRight != itsTargetRotationLeftRight)
			{
				if(itsTargetRotationLeftRight == itsRotation.itsLeftRight.itsStartValue)
				{
					itsRotationLeftRight = Mathf.Lerp (itsRotationLeftRight, itsTargetRotationLeftRight, itsLeftRightRotationSpeed * Time.deltaTime);
				}
				else
				{
					itsRotationLeftRight = itsTargetRotationLeftRight;
				}
			}
			
			if (itsRotationUpDown != itsTargetRotationUpDown - anAngle)
			{
				if(itsTargetRotationUpDown == itsRotation.itsUpDown.itsStartValue)
				{
					itsRotationUpDown = Mathf.Lerp (itsRotationUpDown, itsTargetRotationUpDown - anAngle, itsUpDownRotationSpeed * Time.deltaTime);
				}
				else
				{
					itsRotationUpDown = itsTargetRotationUpDown;
				}
			}

			if (itsRotation.itsUseQuaternions)
			{
				itsTargetRotationHorizontal = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
				itsTargetRotationVertical = Quaternion.Euler (itsTargetRotationUpDown - anAngle, 0f, 0f);

				if (itsRotationHorizontal != itsTargetRotationHorizontal)
				{
					if(itsTargetRotationLeftRight == itsRotation.itsLeftRight.itsStartValue)
					{
						itsRotationHorizontal = Quaternion.Slerp (itsRotationHorizontal, itsTargetRotationHorizontal, itsLeftRightRotationSpeed * Time.deltaTime);
					}
					else
					{
						itsRotationHorizontal = itsTargetRotationHorizontal;
					}
				}
				
				if (itsRotationVertical != itsTargetRotationVertical)
				{
					if(itsTargetRotationUpDown == itsRotation.itsUpDown.itsStartValue)
					{
						itsRotationVertical = Quaternion.Slerp (itsRotationVertical, itsTargetRotationVertical, itsUpDownRotationSpeed * Time.deltaTime);
					}
					else
					{
						itsRotationVertical = itsTargetRotationVertical;
					}
				}


				Quaternion anInputQuaternion = itsRotationHorizontal * itsRotationVertical;

				if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
				{
					itsLinkRotationQuaternion = Quaternion.Slerp (itsLinkRotationQuaternion, itsRootTranform.rotation, Time.deltaTime * itsRoot.itsLinkTargetRotationSpeed);
				}
				else
				{
					itsLinkRotationQuaternion = Quaternion.Slerp (itsLinkRotationQuaternion, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * itsRoot.itsLinkTargetRotationSpeed);
				}
				itsRotationQuaternion = itsLinkRotationQuaternion * anInputQuaternion;
				//itsRotationQuaternion = anInputQuaternion;
			}
			else
			{

				Quaternion anInputYawQuaternion = Quaternion.Euler (0f, itsRotationLeftRight, 0f);
				Quaternion anInputPitchQuaternion = Quaternion.Euler (itsRotationUpDown, 0f, 0f);

				Quaternion anInputQuaternion = anInputYawQuaternion * anInputPitchQuaternion;

				if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
				{
					itsLinkRotationQuaternion = Quaternion.Slerp (itsLinkRotationQuaternion, itsRootTranform.rotation, Time.deltaTime * itsRoot.itsLinkTargetRotationSpeed);
				}
				else
				{
					itsLinkRotationQuaternion = Quaternion.Slerp (itsLinkRotationQuaternion, Quaternion.Euler (new Vector3 (0, 0, 0)), Time.deltaTime * itsRoot.itsLinkTargetRotationSpeed);
				}

				itsRotationQuaternion = itsLinkRotationQuaternion * anInputQuaternion;
				//itsRotationQuaternion = anInputQuaternion;
			}

			/// detect collision & calculate new position
			itsCollisionZoom = itsTargetZoom;

			//Check for Ray Collision
			Vector3 aNormalVector = Vector3.zero;
			if (itsRoot.itsRoot != null && itsEnvironment.itsTestCollisions)
			{
				RaycastHit aCollisionHit;
				
				//Collision detected
				if (Physics.Linecast (itsRootTranform.position, itsOriginalPosition, out aCollisionHit, itsEnvironment.itsCollisionLayer))
				{
					Debug.DrawLine (itsRootTranform.position, itsOriginalPosition, new Color (1, 0.0f, 0.0f));

					//get distance to collision object + a little overhead
					float aDistance = Vector3.Distance (itsRootTranform.position, aCollisionHit.point + itsEnvironment.itsCollisionOffset*transform.forward);
					
					if (!itsZoom.itsDisableZoomLimits)
					{
						if (aDistance < itsZoom.itsMinZoom)
							aDistance = itsZoom.itsMinZoom;
					}

					if (itsTargetZoom > aDistance)
					{
						//only set collision distance if smaller than current distance - prevent zooming away
						itsCollisionZoom = aDistance;
					}
					itsCollision = true;
					aNormalVector = aCollisionHit.normal * 1;

				}
				else
				{
					Debug.DrawLine (itsRootTranform.position, itsOriginalPosition, new Color (0.5f, 0.5f, 0.5f));
				}
			}
			
			float aCollisionZoomSpeed = 50.0f;
			//Ray collision found -> do following
			if (itsCollision)
			{
				if (aNormalVector != Vector3.zero)
				{
					//Lerp to ray cast normal
					itsNormalVector.x = Mathf.Lerp (itsNormalVector.x, aNormalVector.x, aCollisionZoomSpeed * Time.deltaTime);
					itsNormalVector.y = Mathf.Lerp (itsNormalVector.y, aNormalVector.y, aCollisionZoomSpeed * Time.deltaTime);
					itsNormalVector.z = Mathf.Lerp (itsNormalVector.z, aNormalVector.z, aCollisionZoomSpeed * Time.deltaTime);
				}
			}
			else
			{
				//Remove offset if no collision
				itsNormalVector.x = Mathf.Lerp (itsNormalVector.x, 0, aCollisionZoomSpeed * Time.deltaTime);
				itsNormalVector.y = Mathf.Lerp (itsNormalVector.y, 0, aCollisionZoomSpeed * Time.deltaTime);
				itsNormalVector.z = Mathf.Lerp (itsNormalVector.z, 0, aCollisionZoomSpeed * Time.deltaTime);

			}
			if(itsCollisionZoom < itsTargetZoom)
				itsCurrentZoom = Mathf.Lerp (itsCurrentZoom, itsCollisionZoom, 50* Time.deltaTime);//itsCollisionZoom;
			else
				itsCurrentZoom = Mathf.Lerp (itsCurrentZoom, itsCollisionZoom, itsZoom.itsZoomSpeed * Time.deltaTime);
			
			//Debug.Log (itsCollisionZoom + " - " + itsTargetZoom + " - " + itsRotationLeftRight + "Collision " + itsCollision + " OP: " + itsOriginalPosition);
			
			//Original Position without offset -> for ray cast
			//itsOriginalPosition = itsCurrentLinkPosition + itsRotationQuaternion * (new Vector3 (0f, 0f, -itsCurrentZoom) + itsCurrentBoxCollisionNormal);
			itsOriginalPosition = itsCurrentLinkPosition + itsRotationQuaternion * (new Vector3 (0f, 0f, -itsTargetZoom));
			//
			//final position of orbiter
			//transform.position = itsCurrentLinkPosition + itsRotationQuaternion * (new Vector3 (0f, 0f, -itsCurrentZoom) + itsCurrentBoxCollisionNormal);
			transform.position = itsCurrentLinkPosition + itsRotationQuaternion * (new Vector3 (0f, 0f, -itsCurrentZoom));

			//itsLastCollisionZoom = itsCurrentBoxCollisionZoom;

			//move box collider to original collision position
			//itsInnerBoxCollider.center = new Vector3 (0, 0, Camera.main.nearClipPlane - itsCurrentBoxCollisionZoom + itsLastCollisionZoom) - transform.InverseTransformDirection (itsFinalNormalVector);
			itsCollision = false;
		}
		LookAtTarget (false);
		HandleObjectsInLineOfSight();
	}
	
	/// <summary>
	/// This method collects all objects in the line of sight between the root and the camera
	/// </summary>
	private void HandleObjectsInLineOfSight()
	{
		if(itsRootTranform == null)
			return;
		
		Vector3 aDirection = itsOriginalPosition-itsRootTranform.position;
		aDirection.Normalize();
		float aDistance = Vector3.Distance(itsRootTranform.position,itsOriginalPosition);
		int aNumberOfHits = 0;
		if(itsObjectInLineOfSight != null)
			aNumberOfHits = itsObjectInLineOfSight.Length;
		itsObjectInLineOfSight = Physics.RaycastAll(itsRootTranform.position,aDirection,aDistance,itsEnvironment.itsCollisionLayer);
		
		if(itsObjectInLineOfSight != null)
		{
			if(itsObjectInLineOfSight.Length != aNumberOfHits)
			{
				if(EventIntersectorsChanged != null)
					EventIntersectorsChanged.Trigger(this);
			}
		}
	}
	
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="aCollision"></param>
//	void OnCollisionEnter (Collision aCollision)
//	{
//		if (itsEnvironment.itsUseBoxCollision && itsEnvironment.itsTestCollisions)
//		{
//			//Box collision found
//			itsBoxCollision = true;
//			int anIndex = 0;
//
//			//Check if zoomed collider found collision
//			for (int i = 0; i < aCollision.contacts.Length; i++)
//			{
//				ContactPoint aPoint = aCollision.contacts [i];
//				if (aPoint.thisCollider == itsInnerBoxCollider)
//				{
//					itsInnerBoxCollision= true;
//					anIndex = i;
//					break;
//				}
//			}
//			for (int i = 0; i < aCollision.contacts.Length; i++)
//			{
//				ContactPoint aPoint = aCollision.contacts [i];
//				if (aPoint.thisCollider == itsOuterBoxCollider)
//				{
//					itsOuterBoxCollision= true;
//					anIndex = i;
//					break;
//				}
//			}
//
//
//			/*//else set normal collider
//			if (itsCurrentCollider == null)
//			{
//				itsCurrentCollider = itsInnerBoxCollider;
//			}*/
//
//				if (itsInnerBoxCollision)
//			{
//				//if zoomed collider -> add to offset and reduce zoom
//				itsBoxCollisionNormal = aCollision.contacts [anIndex].normal * 2f;// * Time.deltaTime;
//				itsBoxCollisionZoom += 150f * Time.deltaTime;
//			}
//
//		}
//
//	}
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="aCollision"></param>
//	void OnCollisionStay (Collision aCollision)
//	{
//
//		if (itsEnvironment.itsUseBoxCollision && itsEnvironment.itsTestCollisions)
//		{
//			//Box collision found
//			itsBoxCollision = true;
//			int anIndex = 0;
//
//			//Check if zoomed collider found collision
//			for (int i = 0; i < aCollision.contacts.Length; i++)
//			{
//				ContactPoint aPoint = aCollision.contacts [i];
//				if (aPoint.thisCollider == itsInnerBoxCollider)
//				{
//					itsInnerBoxCollision= true;
//					anIndex = i;
//					break;
//				}
//			}
//			for (int i = 0; i < aCollision.contacts.Length; i++)
//			{
//				ContactPoint aPoint = aCollision.contacts [i];
//				if (aPoint.thisCollider == itsOuterBoxCollider)
//				{
//					itsOuterBoxCollision= true;
//					anIndex = i;
//					break;
//				}
//			}
//			/*//else set normal collider
//			if (itsCurrentCollider == null)
//			{
//				itsCurrentCollider = itsInnerBoxCollider;
//			}*/
//
//				if (itsInnerBoxCollision)
//			{
//				//if zoomed collider -> add to offset and reduce zoom
//				itsBoxCollisionNormal = aCollision.contacts [anIndex].normal * 2f;// * Time.deltaTime;
//				itsBoxCollisionZoom += 150f * Time.deltaTime;
//			}
//
//		}
//	}
//
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="aCollision"></param>
//	void OnCollisionExit(Collision aCollision)
//	{
//		//itsBoxCollision = false;
//		//itsCurrentCollider = null;
//		//itsBoxCollisionNormal = Vector3.zero;
//	}
//
	
//	void OnItween(Vector3 theNewVector)
//	{
//		Debug.Log("theNewVector"+theNewVector);
//		itsCurrentLookatPosition = theNewVector;
//	}

	private void LookAtTarget (bool theInit)
	{
		if(itsRootOldTranform == null || itsRootTranform == null)
			return;
		
		if(itsLookatTransform == null && itsLookat.itsLookatTarget != null)
			itsLookatTransform = itsLookat.itsLookatTarget.transform;
		
		float aDistanceFromOrigin = Vector3.Distance(itsCurrentLinkPosition,itsRootOldTranform.position);
		float aTotalDistance = Vector3.Distance(itsRootOldTranform.position,itsRootTranform.position);
		if(itsRootTranform == itsRootOldTranform)
			aTotalDistance = 1;
		
		if (!itsLookat.itsEnable)
		{
			if (itsRoot.itsRoot != null)
			{
				itsCurrentLookatPosition.x = Mathf.SmoothDamp (itsCurrentLookatPosition.x, itsRootTranform.position.x, ref itsLinkLookatVelocity.x, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLookatPosition.y = Mathf.SmoothDamp (itsCurrentLookatPosition.y, itsRootTranform.position.y, ref itsLinkLookatVelocity.y, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				itsCurrentLookatPosition.z = Mathf.SmoothDamp (itsCurrentLookatPosition.z, itsRootTranform.position.z, ref itsLinkLookatVelocity.z, 1.0f / itsRoot.itsLinkTargetPositionSpeed);
				
//				Hashtable aHashTable = iTween.Hash("from",itsCurrentLookatPosition,"to",itsRootTranform.position,"speed",100.0f,"onupdate","OnItween");
//				iTween.ValueTo(this.gameObject,aHashTable);
				
				itsRootUp = Vector3.Slerp(itsRootOldTranform.up,itsRootTranform.up,aDistanceFromOrigin/aTotalDistance);
				
				Vector3 aForwardVector = (itsCurrentLookatPosition - transform.position).normalized;
				Quaternion aLookRotation = Quaternion.LookRotation(aForwardVector,itsRootUp);
				transform.rotation = aLookRotation;
			}
		}
		else
		{
			if(theInit)
			{
				itsCurrentLookatPosition.x = itsLookatTransform.position.x;
				itsCurrentLookatPosition.y = itsLookatTransform.position.y;
				itsCurrentLookatPosition.z = itsLookatTransform.position.z;

				if(itsLookat.itsUpVectorSource != null)
				{
					itsTargetLookatRotation = Quaternion.LookRotation((itsCurrentLookatPosition - transform.position).normalized, itsLookat.itsUpVectorSource.transform.up);
					itsCurrentLookatRotation = itsTargetLookatRotation;
				}
			}
			else
			{

				if(itsLookat.itsHardLinkToTarget)
				{
					itsCurrentLookatPosition = itsLookatTransform.position;
					itsCurrentLookatRotation = Quaternion.LookRotation((itsCurrentLookatPosition - transform.position).normalized, itsLookat.itsUpVectorSource.transform.up);
				}
				else
				{
					
					itsCurrentLookatPosition.x = Mathf.SmoothDamp (itsCurrentLookatPosition.x, itsLookatTransform.position.x, ref itsLinkLookatVelocity.x, 1.0f / itsLookat.itsLookatSpeed);
					itsCurrentLookatPosition.y = Mathf.SmoothDamp (itsCurrentLookatPosition.y, itsLookatTransform.position.y, ref itsLinkLookatVelocity.y, 1.0f / itsLookat.itsLookatSpeed);
					itsCurrentLookatPosition.z = Mathf.SmoothDamp (itsCurrentLookatPosition.z, itsLookatTransform.position.z, ref itsLinkLookatVelocity.z, 1.0f / itsLookat.itsLookatSpeed);

					itsRootUp = Vector3.Slerp(itsRootOldTranform.up,itsRootTranform.up,aDistanceFromOrigin/aTotalDistance);
					Vector3 aForwardVector = (itsCurrentLookatPosition - transform.position).normalized;
					itsTargetLookatRotation = Quaternion.LookRotation(aForwardVector,itsRootUp);
					
					
//					itsTargetLookatRotation = Quaternion.LookRotation((itsCurrentLookatPosition - transform.position).normalized, itsRootTranform.up);
					itsCurrentLookatRotation = itsTargetLookatRotation;//Quaternion.Slerp(itsCurrentLookatRotation, itsTargetLookatRotation, Time.deltaTime * itsLookat.itsLookatSpeed);
				}
				transform.rotation = itsCurrentLookatRotation;
			}
			transform.rotation = itsCurrentLookatRotation;
		}
	}
	
	
//	/// <summary>
//	/// Looks at target.
//	/// </summary>
//	/// <param name='theInit'>
//	/// The init.
//	/// </param>
//	private void LookAtTarget (bool theInit)
//	{
//		float aMultiplicator = 1.0f;
//		if(Mathf.Repeat(itsRotationUpDown+90.0f,360.0f) > 180.0f)
//			aMultiplicator = -1.0f;
//
//		if (!itsLookat.itsEnable)
//		{
//			transform.rotation = Quaternion.LookRotation((itsCurrentLinkPosition - transform.position).normalized, aMultiplicator*itsRootTranform.up);
//		}
//		else
//		{
//			if(theInit)
//			{
//				transform.rotation = Quaternion.LookRotation((itsLookatTransform.position - transform.position).normalized, itsLookat.itsUpVectorSource.transform.up);
//			}
//			else
//			{
//
//				if(itsLookat.itsHardLinkToTarget)
//				{
//					transform.rotation = Quaternion.LookRotation((itsLookatTransform.position - transform.position).normalized, itsLookat.itsUpVectorSource.transform.up);
//				}
//				else
//				{
//
//					itsCurrentLookatPosition.x = Mathf.SmoothDamp (itsCurrentLookatPosition.x, itsLookatTransform.position.x, ref itsLinkLookatVelocity.x, 1.0f / itsLookat.itsLookatSpeed);
//					itsCurrentLookatPosition.y = Mathf.SmoothDamp (itsCurrentLookatPosition.y, itsLookatTransform.position.y, ref itsLinkLookatVelocity.y, 1.0f / itsLookat.itsLookatSpeed);
//					itsCurrentLookatPosition.z = Mathf.SmoothDamp (itsCurrentLookatPosition.z, itsLookatTransform.position.z, ref itsLinkLookatVelocity.z, 1.0f / itsLookat.itsLookatSpeed);
//
//					transform.rotation = Quaternion.LookRotation((itsCurrentLookatPosition - transform.position).normalized, itsRootTranform.up);
//				}
//			}
//		}
//	}

	/// <summary>
	/// 
	/// </summary>
	private void GetGroundVector ()
	{
		if (itsRoot.itsRoot != null)
		{
			RaycastHit aCollisionHit;
			if (Physics.Linecast (itsRootTranform.position, itsRootTranform.position - Vector3.up * 1000.0f, out aCollisionHit))//, itsDataSpatialOrbiter.itsCollisionLayers.value))
			{
				Debug.DrawLine (itsRootTranform.position, aCollisionHit.point, Color.yellow);
				itsTargetGroundNormal = -aCollisionHit.normal;
				itsTargetGroundNormal.Normalize ();
				itsCurrentGroundNormal = Vector3.Lerp (itsCurrentGroundNormal, itsTargetGroundNormal, Time.deltaTime * 1.0f);
				itsCurrentGroundNormal.Normalize ();
			}
			else
			{
				Debug.DrawLine (transform.position, transform.position - Vector3.up * 1000.0f, Color.yellow);
			}
		}
	}

	//#if UNITY_IPHONE || UNITY_ANDROID
	private float itsLastDistanceBetweenTwoTouches = 0.0f;
	private Vector3 itsLastMedianPosition = Vector3.zero;
	//#endif
	
	/// <summary>
	/// 
	/// </summary>
	public void ReactToInput ()
	{
		float aSign = 1.0f;
		if (itsInput.itsInvertYAxis)
		{
			aSign = -1.0f;
		}
		
		float aDampedInputValue = 1.0f;
		
		float anInputLeftRight = Input.GetAxis (itsInput.itsXAxisName) * itsInput.itsXAxisSensitivity;
		if (itsRotation.itsLeftRight.itsEnable)
		{
			if (itsRotation.itsLeftRight.itsUseLimits)	//make input constant and slow when limits reached
			{
				
				if (itsInputRotationLeftRight > itsRotation.itsLeftRight.itsStartValue + itsRotation.itsLeftRight.itsLeftLimit && anInputLeftRight > 0.0f && anInputLeftRight > aDampedInputValue)
				{
					anInputLeftRight = aDampedInputValue;
				}
				else if (itsInputRotationLeftRight < itsRotation.itsLeftRight.itsStartValue - itsRotation.itsLeftRight.itsRightLimit && anInputLeftRight < 0.0f && anInputLeftRight < -aDampedInputValue)
				{
					anInputLeftRight = -aDampedInputValue;
				}
			}
			itsInputRotationLeftRight += anInputLeftRight;
		}
		float anInputUpDown = Input.GetAxis (itsInput.itsYAxisName) * -itsInput.itsYAxisSensitivity*aSign;
		if (itsRotation.itsUpDown.itsEnable)
		{
			if (itsRotation.itsUpDown.itsUseLimits)
			{
				if (itsInputRotationUpDown > itsRotation.itsUpDown.itsStartValue + itsRotation.itsUpDown.itsUpLimit && anInputUpDown > 0.0f && anInputUpDown > aDampedInputValue)
				{
					anInputUpDown = aDampedInputValue;
				}
				else if (itsInputRotationUpDown < itsRotation.itsUpDown.itsStartValue - itsRotation.itsUpDown.itsDownLimit && anInputUpDown < 0.0f && anInputUpDown < -aDampedInputValue)
				{
					anInputUpDown = -aDampedInputValue;
				}
			}
			itsInputRotationUpDown += anInputUpDown;
		}
		if (itsZoom.itsEnable)
		{
			itsTargetZoom += Input.GetAxis (itsInput.itsZoomAxisName) * -itsInput.itsZoomAxisSensitivity;
		}
	}
	
	//#if UNITY_IPHONE || UNITY_ANDROID
	/// <summary>
	/// 
	/// </summary>
	public void ReactToInputTouch ()
	{
		if(Input.touches.Length == 1 && !itsInput.itsTwoFingerRotateTouch)
		{
			if (itsRotation.itsLeftRight.itsEnable)
			{
				itsInputRotationLeftRight += Input.GetTouch(0).deltaPosition.x * itsInput.itsXAxisSensitivityTouch;
			}
			
			if (itsRotation.itsUpDown.itsEnable)
			{
				itsInputRotationUpDown += Input.GetTouch(0).deltaPosition.y * -itsInput.itsYAxisSensitivityTouch;
			}
		}
		if(Input.touches.Length == 2)
		{
			Touch aTouchLeft = Input.GetTouch(0);
			Touch aTouchRight = Input.GetTouch(1);
			
			if(aTouchLeft.phase == TouchPhase.Moved && aTouchRight.phase == TouchPhase.Moved)
			{
				float aDistanceBetweenTwoTouches = Vector2.Distance(aTouchLeft.position, aTouchRight.position);
				Vector3 aMedianPosition = (aTouchLeft.position + aTouchRight.position)/2.0f;
				
				if(itsLastDistanceBetweenTwoTouches == 0) //first time init
					itsLastDistanceBetweenTwoTouches = aDistanceBetweenTwoTouches;
				if(itsLastMedianPosition == Vector3.zero) //first time init
					itsLastMedianPosition = aMedianPosition;
				
				float aDeltaDistance = itsLastDistanceBetweenTwoTouches - aDistanceBetweenTwoTouches;
				itsLastDistanceBetweenTwoTouches = aDistanceBetweenTwoTouches;
				
				Vector3 aDeltaMedianPosition = itsLastMedianPosition - aMedianPosition;
				itsLastMedianPosition = aMedianPosition;
				if(itsZoom.itsEnable)
				{
					itsTargetZoom += aDeltaDistance * GetZoomAxisSensitivity ();
				}
				if (itsRotation.itsLeftRight.itsEnable && itsInput.itsTwoFingerRotateTouch)
				{
					itsInputRotationLeftRight += aDeltaMedianPosition.x * itsInput.itsXAxisSensitivityTouch;
				}
				
				if (itsRotation.itsUpDown.itsEnable && itsInput.itsTwoFingerRotateTouch)
				{
					itsInputRotationUpDown += aDeltaMedianPosition.y * -itsInput.itsYAxisSensitivityTouch;
				}
			}
		}
	}
	//#endif
	
	/// <summary>
	/// 
	/// </summary>
	void OnDrawGizmosSelected()
	{
		if(itsRoot.itsRoot == null)
			return;
		
		itsRootTranform =  itsRoot.itsRoot.transform;
		if(itsLookat.itsLookatTarget != null)
		{
			itsLookatTransform = itsLookat.itsLookatTarget.transform;
		}

		//draw target item to lookat target, or if not available to root target
		Vector3 aTargetGizmoPosition = Vector3.zero;
		if (itsLookat.itsLookatTarget != null /*&& itsLookat.itsLookatTarget != itsRoot.itsRoot  && itsLookat.itsEnable*/)
		{
			aTargetGizmoPosition = itsLookatTransform.position;
			Gizmos.DrawIcon (aTargetGizmoPosition, "eye.png", true);
		}
		if (itsRoot.itsRoot != null)
		{
			aTargetGizmoPosition = itsRootTranform.position;
			Gizmos.DrawIcon (aTargetGizmoPosition, "target_64.png", true);
		}


		//if game not running initialize variables
		if (itsEditor)
		{
			itsCurrentZoom = itsZoom.itsStartZoom;
			itsRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
			itsRotationUpDown = itsRotation.itsUpDown.itsStartValue;
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
		if (itsRoot.itsLinkTargetPosition && itsRoot.itsRoot != null)
		{
			aTargetPosition = itsRootTranform.position;
		}
		else
		{
			aTargetPosition = Vector3.zero;
		}

		if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
		{
			aTargetRotation = itsRootTranform.rotation;
		}

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
			

			Quaternion aTargetRotation2 = Quaternion.Euler (aVerticalAngle, 0f, 0f);
			Quaternion aFinalRotation = aTargetRotation;
			aPoint = aTargetPosition + aFinalRotation * aPoint;


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

			aTargetVector = (aPoint - itsLookatTransform.position).normalized;
			aStartPoint = itsLookatTransform.position;
			aEndPoint = aPoint;
			Gizmos.color = new Color(1,1,0);
			Gizmos.DrawLine (aStartPoint, aEndPoint);
		}
	}

	#region exposed methods
	

	#region transform target
	[KGFEventExpose]
	public void SetTransformTarget(GameObject theObject)
	{
		itsTransformTarget = theObject;
	}
	public GameObject GetTransformTarget ()
	{
		return itsTransformTarget;
	}
	#endregion

	#region target
	[KGFEventExpose]
	public void SetRoot(GameObject theObject)
	{
		itsRootOld = itsRoot.itsRoot;
		itsRootOldTranform = itsRootOld.transform;
		itsRoot.itsRoot = theObject;
		itsRootTranform = itsRoot.itsRoot.transform;
		if(EventRootChanged != null)
			EventRootChanged.Trigger(this);
		
		//set target link data
		//itsLinkRotationQuaternion = itsRootTranform.rotation;
		itsLinkTargetStartPosition = itsRootTranform.position;
		//itsCurrentLinkPosition = itsLinkTargetStartPosition;
		itsCurrentLinkRotation = itsRootTranform.eulerAngles;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public GameObject GetTarget()
	{
		return itsRoot.itsRoot;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetCameraFieldOfView(float theValue)
	{
		itsCamera.itsFieldOfView = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetLinkTargetRotation(bool theValue)
	{
		itsRoot.itsLinkTargetRotation = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetLinkTargetRotation()
	{
		return itsRoot.itsLinkTargetRotation;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetLinkTargetPosition(bool theValue)
	{
		itsRoot.itsLinkTargetPosition = theValue;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetLinkTargetPosition()
	{
		return itsRoot.itsLinkTargetPosition;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetLinkTargetRotationSpeed(float theValue)
	{
		itsRoot.itsLinkTargetRotationSpeed = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetLinkTargetRotationSpeed()
	{
		return itsRoot.itsLinkTargetRotationSpeed;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetLinkTargetPositionSpeed(float theValue)
	{
		itsRoot.itsLinkTargetPositionSpeed = theValue;
	}
	public float GetLinkTargetPositionSpeed()
	{
		return itsRoot.itsLinkTargetRotationSpeed;
	}
	#endregion

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	#region rotation
	[KGFEventExpose]
	public void SetMaxRotationUpDown(float theValue)
	{
		itsRotation.itsUpDown.itsUpLimit = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMaxRotationUpDown()
	{
		return itsRotation.itsUpDown.itsUpLimit;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetMinRotationUpDown(float theValue)
	{
		itsRotation.itsUpDown.itsDownLimit = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMinRotationUpDown()
	{
		return itsRotation.itsUpDown.itsDownLimit;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetTargetRotationUpDown(float theValue)
	{
		itsInputRotationUpDown = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetTargetRotationUpDown()
	{
		return itsTargetRotationUpDown;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void DisableRotationUpDown(bool theValue)
	{
		itsRotation.itsUpDown.itsEnable = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetDisableRotationUpDown()
	{
		return itsRotation.itsUpDown.itsEnable;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void DisableRotationUpDownLimits(bool theValue)
	{
		itsRotation.itsUpDown.itsUseLimits = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetDisableRotationUpDownLimits()
	{
		return itsRotation.itsUpDown.itsUseLimits;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetRotationUpDown(float theValue)
	{
		itsRotationUpDown = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetRotationUpDown()
	{
		return itsRotationUpDown;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	public void SetStartRotationUpDown(float theValue)
	{
		itsRotation.itsUpDown.itsStartValue = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetStartRotationUpDown()
	{
		return itsRotation.itsUpDown.itsStartValue;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetMaxRotationLeftRight(float theValue)
	{
		itsRotation.itsLeftRight.itsRightLimit = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMaxRotationLeftRight()
	{
		return itsRotation.itsLeftRight.itsRightLimit;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetMinRotationLeftRight(float theValue)
	{
		itsRotation.itsLeftRight.itsLeftLimit = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMinRotationLeftRight()
	{
		return itsRotation.itsLeftRight.itsLeftLimit;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetTargetRotationLeftRight(float theValue)
	{
		itsInputRotationLeftRight = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetTargetRotationLeftRight()
	{
		return itsTargetRotationLeftRight;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void DisableRotationLeftRight(bool theValue)
	{
		itsRotation.itsLeftRight.itsEnable = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetDisableRotationLeftRight()
	{
		return itsRotation.itsLeftRight.itsEnable;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void DisableRotationLeftRightLimits(bool theValue)
	{
		itsRotation.itsLeftRight.itsUseLimits = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetDisableRotationLeftRightLimits()
	{
		return itsRotation.itsLeftRight.itsUseLimits;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetRotationLeftRight(float theValue)
	{
		itsRotationLeftRight = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetRotationLeftRight()
	{
		return itsRotationLeftRight;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetStartRotationLeftRight(float theValue)
	{
		itsRotation.itsLeftRight.itsStartValue = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetStartRotationLeftRight()
	{
		return itsRotation.itsLeftRight.itsStartValue;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetUseQuaternions(bool theValue)
	{
		itsRotation.itsUseQuaternions = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetUseQuaternions()
	{
		return itsRotation.itsUseQuaternions;
	}
	#endregion

	#region zoom
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetMaxZoom(float theValue)
	{
		itsZoom.itsMaxZoom = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMaxZoom()
	{
		return itsZoom.itsMaxZoom;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetMinZoom(float theValue)
	{
		itsZoom.itsMinZoom = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetMinZoom()
	{
		return itsZoom.itsMinZoom;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetTargetZoom(float theValue)
	{
		itsTargetZoom = theValue;
		itsCollisionZoom = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetTargetZoom()
	{
		return itsTargetZoom;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetStartZoom(float theValue)
	{
		itsZoom.itsStartZoom = theValue;
	}
	public float GetStartZoom()
	{
		return itsZoom.itsStartZoom;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetCurrentZoom(float theValue)
	{
		itsCurrentZoom = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetCurrentZoom()
	{
		return itsCurrentZoom;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetZoomSpeed(float theValue)
	{
		itsZoom.itsZoomSpeed = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetZoomSpeed()
	{
		return itsZoom.itsZoomSpeed;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void EnableZoom(bool theValue)
	{
		itsZoom.itsEnable = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetZoom()
	{
		return itsZoom.itsEnable;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void DisableZoomLimits(bool theValue)
	{
		itsZoom.itsDisableZoomLimits = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetDisableZoomLimits()
	{
		return itsZoom.itsDisableZoomLimits;
	}
	#endregion

	#region control
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void EnableInput(bool theValue)
	{
		itsInput.itsEnable = theValue;
	}
	
	[KGFEventExpose]
	public void ResetToStart()
	{
		//smoothly reset to start values when disabling input
		itsInputRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		itsInputRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		itsTargetZoom = itsZoom.itsStartZoom;
		itsTargetRotationHorizontal = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
		itsTargetRotationVertical = Quaternion.Euler (itsTargetRotationUpDown, 0f, 0f);
	}
	
	[KGFEventExpose]
	public void ResetRotationToStart()
	{
		itsInputRotationLeftRight = itsRotation.itsLeftRight.itsStartValue;
		itsInputRotationUpDown = itsRotation.itsUpDown.itsStartValue;
		itsTargetRotationHorizontal = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
		itsTargetRotationVertical = Quaternion.Euler (itsTargetRotationUpDown, 0f, 0f);
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetEnableInput()
	{
		return itsInput.itsEnable;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void InvertYAxis(bool theValue)
	{
		itsInput.itsInvertYAxis = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetInvertYAxis()
	{
		return itsInput.itsInvertYAxis;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetXAxisSensitivity(float theValue)
	{
		itsInput.itsXAxisSensitivity = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetXAxisSensitivity()
	{
		return itsInput.itsXAxisSensitivity;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetYAxisSensitivity(float theValue)
	{
		itsInput.itsYAxisSensitivity = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetYAxisSensitivity()
	{
		return itsInput.itsYAxisSensitivity;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetXAxisSensitivityTouch(float theValue)
	{
		itsInput.itsXAxisSensitivityTouch = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetXAxisSensitivityTouch()
	{
		return itsInput.itsXAxisSensitivityTouch;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetTwoFingerRotateTouch(bool theValue)
	{
		itsInput.itsTwoFingerRotateTouch = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetTwoFingerRotateTouch()
	{
		return itsInput.itsTwoFingerRotateTouch;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetYAxisSensitivityTouch(float theValue)
	{
		itsInput.itsYAxisSensitivityTouch = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetYAxisSensitivityTouch()
	{
		return itsInput.itsYAxisSensitivityTouch;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetZoomAxisSensitivity(float theValue)
	{
		itsInput.itsZoomAxisSensitivity = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetZoomAxisSensitivity()
	{
		return itsInput.itsZoomAxisSensitivity;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theString"></param>
	[KGFEventExpose]
	public void SetXAxisName(string theString)
	{
		itsInput.itsXAxisName = theString;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public string GetXAxisName()
	{
		return itsInput.itsXAxisName;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theString"></param>
	[KGFEventExpose]
	public void SetYAxisName(string theString)
	{
		itsInput.itsYAxisName = theString;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public string GetYAxisName()
	{
		return itsInput.itsYAxisName;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theString"></param>
	[KGFEventExpose]
	public void SetZoomAxisName(string theString)
	{
		itsInput.itsZoomAxisName = theString;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public string GetZoomAxisName()
	{
		return itsInput.itsZoomAxisName;
	}
	#endregion

	#region terrain
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void EnableGyroscope(bool theValue)
	{
		itsEnvironment.itsFollowGround = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetEnableGyroscope()
	{
		return itsEnvironment.itsFollowGround;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void TestCollisions(bool theValue)
	{
		itsEnvironment.itsTestCollisions = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetTestCollisions()
	{
		return itsEnvironment.itsTestCollisions;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theLayer"></param>
	[KGFEventExpose]
	public void SetCollisionLayer(LayerMask theLayer)
	{
		itsEnvironment.itsCollisionLayer = theLayer;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public LayerMask GetCollisionLayer()
	{
		return itsEnvironment.itsCollisionLayer;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theLayer"></param>
	public void SetCollisionOffset(float theCollisionOffset)
	{
		itsEnvironment.itsCollisionOffset = theCollisionOffset;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetCollisionOffset()
	{
		return itsEnvironment.itsCollisionOffset;
	}

//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="theValue"></param>
//	[KGFEventExpose]
//	public void UseBoxCollision(bool theValue)
//	{
//		itsEnvironment.itsUseBoxCollision = theValue;
//	}
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <returns></returns>
//	public bool GetUseBoxCollision()
//	{
//		return itsEnvironment.itsUseBoxCollision;
//	}
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <param name="theValue"></param>
//	[KGFEventExpose]
//	public void UseRayCollision(bool theValue)
//	{
//		itsEnvironment.itsUseRayCollision = theValue;
//	}
//
//	/// <summary>
//	/// 
//	/// </summary>
//	/// <returns></returns>
//	public bool GetUseRayCollision()
//	{
//		return itsEnvironment.itsUseRayCollision;
//	}
	#endregion

	#region lookat target
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void EnableLookat(bool theValue)
	{
		itsLookat.itsEnable = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetLookatEnabled()
	{
		return itsLookat.itsEnable;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="theObject"></param>
	[KGFEventExpose]
	public void SetLookatTarget(GameObject theObject)
	{
		itsLookat.itsLookatTarget = theObject;
		if(itsLookat.itsLookatTarget != null)
		{
			itsLookatTransform = itsLookat.itsLookatTarget.transform;
		}
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public GameObject GetLookatTarget()
	{
		return itsLookat.itsLookatTarget;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theObject"></param>
	[KGFEventExpose]
	public void SetUpVectorSource(GameObject theObject)
	{
		itsLookat.itsUpVectorSource = theObject;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public GameObject GetUpVectorSource()
	{
		return itsLookat.itsUpVectorSource;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="theValue"></param>
	[KGFEventExpose]
	public void SetLookatSpeed(float theValue)
	{
		itsLookat.itsLookatSpeed = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetLookatSpeed()
	{
		return itsLookat.itsLookatSpeed;
	}
	
	[KGFEventExpose]
	public void SetHardLinkToTarget(bool theValue)
	{
		itsLookat.itsHardLinkToTarget = theValue;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public bool GetHardLinkToTarget()
	{
		return itsLookat.itsHardLinkToTarget;
	}
	
	#endregion

	#endregion
	
	
	
	
	public Vector3 CalculateTargetPosition()
	{
		if (itsRoot.itsRoot != null)
		{
			
			Vector3 aTargetPosition;
			if (itsRoot.itsLinkTargetPosition && itsRoot.itsRoot != null)
			{
				aTargetPosition = itsRootTranform.position;
			}
			else
			{
				aTargetPosition = itsLinkTargetStartPosition;
			}
			
			
			
			Quaternion aTargetRotation;
			Quaternion aLinkRotation;
			if (itsRotation.itsUseQuaternions)
			{
				//itsTargetRotationHorizontal = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
				//itsTargetRotationVertical = Quaternion.Euler (itsTargetRotationUpDown, 0f, 0f);
				
				Quaternion anInputQuaternion = itsTargetRotationHorizontal * itsTargetRotationVertical;

				if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
				{
					aLinkRotation = itsRootTranform.rotation;
				}
				else
				{
					aLinkRotation= Quaternion.Euler (new Vector3 (0, 0, 0));
				}
				aTargetRotation = aLinkRotation * anInputQuaternion;
			}
			else
			{

				Quaternion anInputYawQuaternion = Quaternion.Euler (0f, itsTargetRotationLeftRight, 0f);
				Quaternion anInputPitchQuaternion = Quaternion.Euler (itsTargetRotationUpDown, 0f, 0f);

				Quaternion anInputQuaternion = anInputYawQuaternion * anInputPitchQuaternion;

				if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
				{
					aLinkRotation = itsRootTranform.rotation;
				}
				else
				{
					aLinkRotation= Quaternion.Euler (new Vector3 (0, 0, 0));
				}
				aTargetRotation = aLinkRotation * anInputQuaternion;
			}


			Vector3 aFinalPosition = aTargetPosition + aTargetRotation * (new Vector3 (0f, 0f, -itsCollisionZoom));

			return aFinalPosition;
		}
		else
		{
			return Vector3.zero;
		}
	}
	
	
	public bool GetTargetRotationsReached(float minRotation)
	{
		bool aRotationReached = true;
		if (itsRoot.itsLinkTargetRotation && itsRoot.itsRoot != null)
		{
			if(Vector3.Distance(itsCurrentLinkRotation, itsRootTranform.eulerAngles) > minRotation && Vector3.Distance(itsCurrentLinkRotation, itsRootTranform.eulerAngles) < 360f-minRotation)
			{
				aRotationReached = false;
			}
		}
		else
		{
			if(Vector3.Distance(itsCurrentLinkRotation, new Vector3(0,0,0)) > minRotation && Vector3.Distance(itsCurrentLinkRotation, new Vector3(0,0,0)) < 360f-minRotation)
			{
				aRotationReached = false;
			}
		}
		return aRotationReached;
	}
	
	public bool GetDestinationReached(float minDistance, float minRotation)
	{
		Vector3 aTargetPosition = CalculateTargetPosition();
		if(Vector3.Distance(transform.position, aTargetPosition) < minDistance && GetTargetRotationsReached(minRotation))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public Vector3 GetCurrentLookAtPosition()
	{
		return itsCurrentLookatPosition;
	}
	
	public void SetCurrentLookAtPosition(Vector3 thePosition)
	{
		itsCurrentLookatPosition = thePosition;
	}
	
	
	#region save / load
	
	public KGFOrbitCamData SaveSettings()
	{
		KGFOrbitCamData aData = new KGFOrbitCamData();
		
		aData.itsRoot.itsRoot = itsRoot.itsRoot;
		aData.itsRoot.itsLinkTargetPosition = itsRoot.itsLinkTargetPosition;
		aData.itsRoot.itsLinkTargetPositionSpeed = itsRoot.itsLinkTargetPositionSpeed;
		aData.itsRoot.itsLinkTargetRotation = itsRoot.itsLinkTargetRotation;
		aData.itsRoot.itsLinkTargetRotationSpeed = itsRoot.itsLinkTargetRotationSpeed;
		
		aData.itsZoom.itsMinZoom = itsZoom.itsMinZoom;
		aData.itsZoom.itsMaxZoom = itsZoom.itsMaxZoom;
		aData.itsZoom.itsEnable = itsZoom.itsEnable;
		aData.itsZoom.itsStartZoom = itsZoom.itsStartZoom;
		aData.itsZoom.itsDisableZoomLimits = itsZoom.itsDisableZoomLimits;
		aData.itsZoom.itsZoomSpeed = itsZoom.itsZoomSpeed;
		
		aData.itsRotation.itsLeftRight.itsLeftLimit = itsRotation.itsLeftRight.itsLeftLimit;
		aData.itsRotation.itsLeftRight.itsRightLimit = itsRotation.itsLeftRight.itsRightLimit;
		aData.itsRotation.itsLeftRight.itsStartValue = itsRotation.itsLeftRight.itsStartValue;
		aData.itsRotation.itsLeftRight.itsEnable = itsRotation.itsLeftRight.itsEnable;
		aData.itsRotation.itsLeftRight.itsUseLimits = itsRotation.itsLeftRight.itsUseLimits;
		
		aData.itsRotation.itsUpDown.itsDownLimit = itsRotation.itsUpDown.itsDownLimit;
		aData.itsRotation.itsUpDown.itsUpLimit = itsRotation.itsUpDown.itsUpLimit;
		aData.itsRotation.itsUpDown.itsStartValue = itsRotation.itsUpDown.itsStartValue;
		aData.itsRotation.itsUpDown.itsEnable = itsRotation.itsUpDown.itsEnable;
		aData.itsRotation.itsUpDown.itsUseLimits = itsRotation.itsUpDown.itsUseLimits;
		
		aData.itsRotation.itsUseQuaternions = itsRotation.itsUseQuaternions;
		
		aData.itsEnvironment.itsTestCollisions = itsEnvironment.itsTestCollisions;
		aData.itsEnvironment.itsCollisionLayer = itsEnvironment.itsCollisionLayer;
		aData.itsEnvironment.itsCollisionOffset = itsEnvironment.itsCollisionOffset;
		aData.itsEnvironment.itsFollowGround = itsEnvironment.itsFollowGround;
		
		aData.itsInput.itsEnable = itsInput.itsEnable;
		aData.itsInput.itsInvertYAxis = itsInput.itsInvertYAxis;
		aData.itsInput.itsXAxisSensitivity = itsInput.itsXAxisSensitivity;
		aData.itsInput.itsYAxisSensitivity = itsInput.itsYAxisSensitivity;
		aData.itsInput.itsXAxisSensitivityTouch = itsInput.itsXAxisSensitivityTouch;
		aData.itsInput.itsYAxisSensitivityTouch = itsInput.itsYAxisSensitivityTouch;
		aData.itsInput.itsTwoFingerRotateTouch = itsInput.itsTwoFingerRotateTouch;
		aData.itsInput.itsZoomAxisSensitivity = itsInput.itsZoomAxisSensitivity;
		aData.itsInput.itsXAxisName = itsInput.itsXAxisName;
		aData.itsInput.itsYAxisName = itsInput.itsYAxisName;
		aData.itsInput.itsZoomAxisName = itsInput.itsZoomAxisName;
		
		aData.itsLookat.itsEnable = itsLookat.itsEnable;
		aData.itsLookat.itsLookatTarget = itsLookat.itsLookatTarget;
		aData.itsLookat.itsLookatSpeed = itsLookat.itsLookatSpeed;
		aData.itsLookat.itsUpVectorSource = itsLookat.itsUpVectorSource;
		aData.itsLookat.itsHardLinkToTarget = itsLookat.itsHardLinkToTarget;
		
		aData.itsCurrentZoom = GetCurrentZoom();
		aData.itsTargetZoom = GetTargetZoom();
		aData.itsCurrentRotationLeftRight = GetRotationLeftRight();
		aData.itsTargetRotationLeftRight = GetTargetRotationLeftRight();
		aData.itsCurrentRotationUpDown = GetRotationUpDown();
		aData.itsTargetRotationUpDown = GetTargetRotationUpDown();
		
		aData.itsCurrentLookatPosition = GetCurrentLookAtPosition();
		
		return aData;
	}
	
	public void LoadSettings(KGFOrbitCamData theData)
	{
		SetRoot(theData.itsRoot.itsRoot);
		SetLinkTargetPosition (theData.itsRoot.itsLinkTargetPosition);
		SetLinkTargetRotation (theData.itsRoot.itsLinkTargetRotation);
		SetLinkTargetPositionSpeed (theData.itsRoot.itsLinkTargetPositionSpeed);
		SetLinkTargetRotationSpeed (theData.itsRoot.itsLinkTargetRotationSpeed);
		
		SetMinZoom (theData.itsZoom.itsMinZoom);
		SetMaxZoom (theData.itsZoom.itsMaxZoom);
		SetTargetZoom (theData.itsTargetZoom);
		EnableZoom (theData.itsZoom.itsEnable);
		DisableZoomLimits (theData.itsZoom.itsDisableZoomLimits);
		SetZoomSpeed (theData.itsZoom.itsZoomSpeed);
		SetStartZoom(theData.itsZoom.itsStartZoom);

		SetMinRotationLeftRight (theData.itsRotation.itsLeftRight.itsLeftLimit);
		SetMaxRotationLeftRight (theData.itsRotation.itsLeftRight.itsRightLimit);
		SetTargetRotationLeftRight (theData.itsTargetRotationLeftRight);
		DisableRotationLeftRight (theData.itsRotation.itsLeftRight.itsEnable);
		DisableRotationLeftRightLimits (theData.itsRotation.itsLeftRight.itsUseLimits);
		SetStartRotationLeftRight(theData.itsRotation.itsLeftRight.itsStartValue);

		SetMinRotationUpDown (theData.itsRotation.itsUpDown.itsDownLimit);
		SetMaxRotationUpDown (theData.itsRotation.itsUpDown.itsUpLimit);
		SetTargetRotationUpDown (theData.itsTargetRotationUpDown);
		DisableRotationUpDown (theData.itsRotation.itsUpDown.itsEnable);
		DisableRotationUpDownLimits (theData.itsRotation.itsUpDown.itsUseLimits);
		SetStartRotationUpDown(theData.itsRotation.itsUpDown.itsStartValue);
		
		SetUseQuaternions (theData.itsRotation.itsUseQuaternions);

		TestCollisions (theData.itsEnvironment.itsTestCollisions);
		SetCollisionLayer (theData.itsEnvironment.itsCollisionLayer);
		SetCollisionOffset (theData.itsEnvironment.itsCollisionOffset);
		EnableGyroscope (theData.itsEnvironment.itsFollowGround);

		EnableInput (theData.itsInput.itsEnable);
		InvertYAxis (theData.itsInput.itsInvertYAxis);
		SetXAxisName(theData.itsInput.itsXAxisName);
		SetYAxisName(theData.itsInput.itsYAxisName);
		SetZoomAxisName(theData.itsInput.itsZoomAxisName);
		SetXAxisSensitivity(theData.itsInput.itsXAxisSensitivity);
		SetYAxisSensitivity(theData.itsInput.itsYAxisSensitivity);
		SetXAxisSensitivityTouch(theData.itsInput.itsXAxisSensitivityTouch);
		SetYAxisSensitivityTouch(theData.itsInput.itsYAxisSensitivityTouch);
		SetTwoFingerRotateTouch(theData.itsInput.itsTwoFingerRotateTouch);
		SetZoomAxisSensitivity(theData.itsInput.itsZoomAxisSensitivity);

		EnableLookat(theData.itsLookat.itsEnable);
		SetLookatTarget (theData.itsLookat.itsLookatTarget);
		SetLookatSpeed (theData.itsLookat.itsLookatSpeed);
		SetUpVectorSource (theData.itsLookat.itsUpVectorSource);
		SetHardLinkToTarget(theData.itsLookat.itsHardLinkToTarget);
	}
	
	#endregion
	
	
	
	

	#region validate

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public KGFMessageList Validate ()
	{
		KGFMessageList aMessageList = new KGFMessageList ();

		//Root
		if (itsRoot.itsRoot == null)
		{
			aMessageList.AddError ("itsRoot should not be empty");
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
			aMessageList.AddError ("itsMaxZoom should not be smaller than itsMinZoom");
		}
		if (itsZoom.itsMinZoom < 0)
		{
			aMessageList.AddError ("itsMinZoom should not be smaller than 0");
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
