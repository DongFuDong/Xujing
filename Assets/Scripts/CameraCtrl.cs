//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraCtrl : MonoBehaviour {

//    public static CameraCtrl Instance;
//    //[SerializeField]
//    public float cameraRotateSpeed = 30f;
//    //[SerializeField]
//    public float camerUpAndDownSpeed =20f;
//    //[SerializeField]
//    public float cameraZoomSpeed = 50f; 

//    //[SerializeField]
//    //private GameObject m_BigTraget;
//    //[SerializeField]
//    //private GameObject m_ATraget;
//    //[SerializeField]
//    //private GameObject m_BTraget;
//    //[SerializeField]
//    //private GameObject m_CTraget;

  
//    /// <summary>
//    /// 摄像机上下控制
//    /// </summary>
//    [SerializeField]
//    private Transform m_cameraUpAndDown;


//    /// <summary>
//    /// 摄像机缩放父物体
//    /// </summary>
//    [SerializeField]
//    private Transform m_camerZoomContainer;

//    /// <summary>
//    /// 摄像机容器
//    /// </summary>
//    [SerializeField]
//    private Transform m_cameraContainer;
//    private void Awake()
//    {
//        Instance = this;
//    }
//    void Start ()
//    {
//        //监听手指滑动方向
//        FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
//        //监听缩放
//        FingerEvent.Instance.OnZoom += OnZoom;
//	}


//    /// <summary>
//    /// 手指方向的判断
//    /// </summary>
//    /// <param name="obj"></param>
//    private void OnFingerDrag(FingerEvent.FingerDir obj)
//    {
//        switch (obj)
//        {
//            case FingerEvent.FingerDir.Up:
//                SetCamerUpAndDown(1);
//                break;
//            case FingerEvent.FingerDir.Down:
//                SetCamerUpAndDown(0);
//                break;
//            case FingerEvent.FingerDir.Right:
//                SetCamerRotate(1);
//                break;
//            case FingerEvent.FingerDir.Left:
//                SetCamerRotate(0);
//                break;
//            default:
//                break;
//        }
//    }
//    /// <summary>
//    /// 缩放的判断
//    /// </summary>
//    /// <param name="obj"></param>
//    private void OnZoom(FingerEvent.ZoomType obj)
//    {
//        switch (obj)
//        {
//            case FingerEvent.ZoomType.In:
//                SetCameraZoom(0);
//                break;
//            case FingerEvent.ZoomType.Out:
//                SetCameraZoom(1);
//                break;
//            default:
//                break;
//        }
//    }

//    /// <summary>
//    /// 取消监听
//    /// </summary>
//    private void OnDestroy()
//    {
//        //
//        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
//        //
//        FingerEvent.Instance.OnZoom -= OnZoom;
//    }
//    void Update ()
//    {
       

//    }


//    /// <summary>
//    /// 设置摄像机旋转
//    /// </summary>
//    /// <param name="type"></param>
//    void SetCamerRotate(int type)
//    {
//        transform.Rotate(0, cameraRotateSpeed * Time.deltaTime * (type == 0 ? -1 : 1), 0);
//    }
//    /// <summary>
//    /// 设置摄像机上下
//    /// </summary>
//    /// <param name="type"></param>
//    void SetCamerUpAndDown(int type)
//    {
//        m_cameraUpAndDown.transform.Rotate(camerUpAndDownSpeed * Time.deltaTime * (type == 0 ? -1 : 1), 0, 0);
//        m_cameraUpAndDown.transform.localEulerAngles = new Vector3(Mathf.Clamp(m_cameraUpAndDown.transform.localEulerAngles.x, 300f, 350f), 0, 0);
//    }
//        /// <summary>
//        /// 设置摄像机缩放
//        /// </summary>
//        /// 
//        /// <param name="type"></param>
//        void SetCameraZoom(int type)
//        {
//        m_cameraContainer.Translate(Vector3.forward * cameraZoomSpeed * Time.deltaTime * (type == 0 ? -1 : 1));
//        m_cameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_cameraContainer.localPosition.z, -300f, 200f));
//        }

//        /// <summary>
//        /// 摄像机跟随cube的位置
//        /// </summary>
//        public void CameraFollow(GameObject obj)
//        {

//        this.transform.position = obj.transform.position;
//        m_camerZoomContainer.LookAt(obj.transform.position);
     
//        }
//    }
    

