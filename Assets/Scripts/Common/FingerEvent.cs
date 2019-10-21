//using UnityEngine;
//using System.Collections;

//public class FingerEvent :  MonoBehaviour {

//    public static FingerEvent Instance;

//    /// <summary>
//    /// 定义方向的枚举
//    /// </summary>
//    public enum FingerDir
//    {
//        Up,
//        Down,
//        Right,
//        Left

//    }
//    /// <summary>
//    /// 定义委托 滑动委托
//    /// </summary>
//    public System.Action<FingerDir> OnFingerDrag;



//    /// <summary>
//    /// 定义缩放枚举
//    /// </summary>
//    public enum ZoomType
//    {
//        In,
//        Out
//    }
//    /// <summary>
//    /// 定义缩放的委托
//    /// </summary>
//    public System.Action<ZoomType> OnZoom;


//    /// <summary>
//    /// 手指上一次的位置
//    /// </summary>
//    private Vector2 m_OldFingerDrag;

//    /// <summary>
//    /// 手指滑动方向
//    /// </summary>
//    private Vector2 m_Dir;
//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Update()
//    {
//#if UNITY_EDITOR || UNITY_STANDALONE_WIN
//        if (Input.GetAxis("Mouse ScrollWheel") < 0)
//        {
//            if (OnZoom != null)
//            {
//                OnZoom(ZoomType.In);
//            }
//        }
//        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
//        {
//            if (OnZoom != null)
//            {
//                OnZoom(ZoomType.Out);
//            }
//        }

//#elif UNITY_ANDROID || UNITY_IPHONE

//#endif
//    }
//    void OnEnable()
//    {
//    	//启动时调用，这里开始注册手势操作的事件。
    	
//    	//按下事件： OnFingerDown就是按下事件监听的方法，这个名子可以由你来自定义。方法只能在本类中监听。下面所有的事件都一样！！！
//        FingerGestures.OnFingerDown += OnFingerDown;
//        //抬起事件
//		FingerGestures.OnFingerUp += OnFingerUp;
//	    //开始拖动事件
//	    FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
//        //拖动中事件...
//        FingerGestures.OnFingerDragMove += OnFingerDragMove;
//        //拖动结束事件
//        FingerGestures.OnFingerDragEnd += OnFingerDragEnd; 
		
//		FingerGestures.OnFingerStationaryBegin += OnFingerStationaryBegin;
		
		
//    }

//    void OnDisable()
//    {
//    	//关闭时调用，这里销毁手势操作的事件
//    	//和上面一样
//        FingerGestures.OnFingerDown -= OnFingerDown;
//		FingerGestures.OnFingerUp -= OnFingerUp;
//		FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
//        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
//        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd; 
		
//		FingerGestures.OnFingerStationaryBegin -= OnFingerStationaryBegin;
		
//    }

//    //按下时调用
//    void OnFingerDown( int fingerIndex, Vector2 fingerPos )
//    {
//		//int fingerIndex 是手指的ID 第一按下的手指就是 0 第二个按下的手指就是1。。。一次类推。
//		//Vector2 fingerPos 手指按下屏幕中的2D坐标
		
//		//将2D坐标转换成3D坐标
       
//    }
	
//	//抬起时调用
//	void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
//	{
		
		
//	}
	
//	//开始滑动
//	void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
//    {
//        m_OldFingerDrag = fingerPos;
//    }
//	//滑动结束
//	void OnFingerDragEnd( int fingerIndex, Vector2 fingerPos )
//	{
//        //m_OldFingerDrag = fingerPos;
	 	
//	}
//	//滑动中
//    void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
//    {
//        m_Dir = fingerPos - m_OldFingerDrag;
//        if (m_Dir.y < m_Dir.x && m_Dir.y > -m_Dir.x)
//        {
//            //向右
//            OnFingerDrag(FingerDir.Right);
           
//        }
//        else if (m_Dir.y > m_Dir.x && m_Dir.y < -m_Dir.x)
//        {
//            //向左
//            OnFingerDrag(FingerDir.Left);
            
//        }
//        else if (m_Dir.y > m_Dir.x && m_Dir.y > -m_Dir.x)
//        {
//            //向上
//            OnFingerDrag(FingerDir.Up);
//        }
//        else
//        {
//            //向下
//            OnFingerDrag(FingerDir.Down);
//        }

//    }

	
//	//按下事件开始后调用，包括 开始 结束 持续中状态只到下次事件开始！
//	void OnFingerStationaryBegin( int fingerIndex, Vector2 fingerPos )
//	{
		
//		 Debug.Log("OnFingerStationaryBegin " + fingerPos + " times with finger " + fingerIndex);
//	}
	

//}
