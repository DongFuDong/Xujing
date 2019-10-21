/***
*   Title:"旭景清园"项目开发
*   [副标题] 户型场景核心代码
*   Description：
*   [描述]
*   Date:2019/01/29
*   Version:0.1
*   Developer:lixin
*   ModifyRecoder:
*
*
*
*
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Global
{
    public class HouseSceneCtrl : MonoBehaviour
    {
        private int _mHoues;
        [SerializeField]
        private GameObject _HouseAMod;
        [SerializeField]
        private GameObject _HouseBMod;
        [SerializeField]
        private GameObject _HouseCMod;
        [SerializeField]
        private GameObject _HouseDMod;

        [SerializeField]
        private GameObject _HouseTypeA;
        [SerializeField]
        private GameObject _HouseTypeB;
        [SerializeField]
        private GameObject _HouseTypeC;
        [SerializeField]
        private GameObject _HouseTypeD;

        [SerializeField]
        private Toggle _HouseATog;
        [SerializeField]
        private Toggle _HouseBTog;
        [SerializeField]
        private Toggle _HouseCTog;
        [SerializeField]
        private Toggle _HouseDTog;

        [SerializeField]
        private Button _UICadBtn;
        [SerializeField]
        private Button _LightBtn;
        [SerializeField]
        private Button _ReturnBtn;

        [SerializeField]
        private GameObject _HouseACad;
        [SerializeField]
        private GameObject _HouseBCad;
        [SerializeField]
        private GameObject _HouseCCad;
        [SerializeField]
        private GameObject _HouseDCad;

        private bool _HouseACadBool = true;
        private bool _HouseBCadBool = true;
        private bool _HouseCCadBool = true;
        private bool _HouseDCadBool = true;
        [SerializeField]
        private GameObject _LightingUI;
        private bool _LightUIBool = true;

        [SerializeField]
        private Transform m_LightPos;
        private Transform m_LightMinValue;
        private Transform m_LightMaxValue;
        [SerializeField]
        private Slider m_lightSlider;
        [SerializeField]
        private KGFOrbitCam HouseCameraStart;
        
        [SerializeField]
        private Camera _camera;


        private void Start()
        {
            #region 初始化
            _mHoues = PlayerPrefs.GetInt("_houseInt");

            //if (mHouesA == 1)
            //{
            //    HouseAMod.SetActive(true);
            //}
            //m_LightMinValue.transform.localPosition= new Vector3(5, 330, 0);
            //m_LightMaxValue.transform.localPosition = new Vector3(85, 330, 0);
            switch (_mHoues)
            {
                case 1:
                    _HouseAMod.SetActive(true);
                    _HouseBMod.SetActive(false);
                    _HouseCMod.SetActive(false);
                    _HouseDMod.SetActive(false);

                    _HouseTypeA.SetActive(true);
                    _HouseTypeB.SetActive(false);
                    _HouseTypeC.SetActive(false);
                    _HouseTypeD.SetActive(false);

                    _HouseATog.isOn = true;
                    _HouseBTog.isOn = false;
                    _HouseCTog.isOn = false;
                    _HouseDTog.isOn = false;
                    break;
                case 2:
                    _HouseAMod.SetActive(false);
                    _HouseBMod.SetActive(true);
                    _HouseCMod.SetActive(false);
                    _HouseDMod.SetActive(false);

                    _HouseTypeA.SetActive(false);
                    _HouseTypeB.SetActive(true);
                    _HouseTypeC.SetActive(false);
                    _HouseTypeD.SetActive(false);

                    _HouseATog.isOn = false;
                    _HouseBTog.isOn = true;
                    _HouseCTog.isOn = false;
                    _HouseDTog.isOn = false;
                    break;
                case 3:
                    _HouseAMod.SetActive(false);
                    _HouseBMod.SetActive(false);
                    _HouseCMod.SetActive(true);
                    _HouseDMod.SetActive(false);

                    _HouseTypeA.SetActive(false);
                    _HouseTypeB.SetActive(false);
                    _HouseTypeC.SetActive(true);
                    _HouseTypeD.SetActive(false);

                    _HouseATog.isOn = false;
                    _HouseBTog.isOn = false;
                    _HouseCTog.isOn = true;
                    _HouseDTog.isOn = false;
                    break;
                case 4:
                    _HouseAMod.SetActive(false);
                    _HouseBMod.SetActive(false);
                    _HouseCMod.SetActive(false);
                    _HouseDMod.SetActive(true);

                    _HouseTypeA.SetActive(false);
                    _HouseTypeB.SetActive(false);
                    _HouseTypeC.SetActive(false);
                    _HouseTypeD.SetActive(true);


                    _HouseATog.isOn = false;
                    _HouseBTog.isOn = false;
                    _HouseCTog.isOn = false;
                    _HouseDTog.isOn = true;
                    break;
                default:
                    break;
            }
            #endregion 初始化
        }


        #region 一级菜单按钮
        #region 点击户型按钮
        /// <summary>
        /// 点击户型A的按钮
        /// </summary>
        public void OnClickHouseATog()
        {
            _HouseAMod.SetActive(true);
            _HouseBMod.SetActive(false);
            _HouseCMod.SetActive(false);
            _HouseDMod.SetActive(false);

            _HouseTypeA.SetActive(true);
            _HouseTypeB.SetActive(false);
            _HouseTypeC.SetActive(false);
            _HouseTypeD.SetActive(false);
        }
        /// <summary>
        /// 点击户型B的按钮
        /// </summary>
        public void OnClickHouseBTog()
        {
            _HouseAMod.SetActive(false);
            _HouseBMod.SetActive(true);
            _HouseCMod.SetActive(false);
            _HouseDMod.SetActive(false);

            _HouseTypeA.SetActive(false);
            _HouseTypeB.SetActive(true);
            _HouseTypeC.SetActive(false);
            _HouseTypeD.SetActive(false);
        }
        /// <summary>
        /// 点击户型C的按钮
        /// </summary>
        public void OnClickHouseCTog()
        {
            _HouseAMod.SetActive(false);
            _HouseBMod.SetActive(false);
            _HouseCMod.SetActive(true);
            _HouseDMod.SetActive(false);

            _HouseTypeA.SetActive(false);
            _HouseTypeB.SetActive(false);
            _HouseTypeC.SetActive(true);
            _HouseTypeD.SetActive(false);
        }
        /// <summary>
        /// 点击户型D按钮
        /// </summary>
        public void OnClickHouseDTog()
        {
            _HouseAMod.SetActive(false);
            _HouseBMod.SetActive(false);
            _HouseCMod.SetActive(false);
            _HouseDMod.SetActive(true);

            _HouseTypeA.SetActive(false);
            _HouseTypeB.SetActive(false);
            _HouseTypeC.SetActive(false);
            _HouseTypeD.SetActive(true);
        }
        /// <summary>
        /// 点击平面图按钮
        /// </summary>
        public void OnClickUICadBtn()
        {
            if (_HouseATog.isOn == true)
            {
                if (_HouseACadBool == true)
                {
                    _HouseACad.SetActive(true);
                    _HouseBCad.SetActive(false);
                    _HouseCCad.SetActive(false);
                    _HouseDCad.SetActive(false);
                    _HouseACadBool = false;
                }
                else
                {
                    _HouseACad.SetActive(false);
                    _HouseACadBool = true;
                }

            }
            if (_HouseBTog.isOn == true)
            {
                if (_HouseBCadBool == true)
                {
                    _HouseACad.SetActive(false);
                    _HouseBCad.SetActive(true);
                    _HouseCCad.SetActive(false);
                    _HouseDCad.SetActive(false);
                    _HouseBCadBool = false;
                }
                else
                {
                    _HouseBCad.SetActive(false);
                    _HouseBCadBool = true;
                }

            }
            if (_HouseCTog.isOn == true)
            {
                if (_HouseCCadBool == true)
                {
                    _HouseACad.SetActive(false);
                    _HouseBCad.SetActive(false);
                    _HouseCCad.SetActive(true);
                    _HouseDCad.SetActive(false);
                    _HouseCCadBool = false;
                }
                else
                {
                    _HouseCCad.SetActive(false);
                    _HouseCCadBool = true;
                }

            }
            if (_HouseDTog.isOn == true)
            {
                if (_HouseDCadBool == true)
                {
                    _HouseACad.SetActive(false);
                    _HouseBCad.SetActive(false);
                    _HouseCCad.SetActive(false);
                    _HouseDCad.SetActive(true);
                    _HouseDCadBool = false;
                }
                else
                {
                    _HouseDCad.SetActive(false);
                    _HouseDCadBool = true;
                }

            }

        }
        #endregion
        /// <summary>
        /// 点击采光按钮
        /// </summary>
        public void OnClickLightingBtn()
        {
            if (_LightUIBool == true)
            {
                _LightingUI.SetActive(true);
                _LightUIBool = false;
            }
            else
            {
                _LightingUI.SetActive(false);
                _LightUIBool = true;
            }

        }
        /// <summary>
        /// 点击返回按钮 加载到最初场景
        /// </summary>
        public void OnClickReturnBtn()
        {
            GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.MainScenes;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景

        }
        #endregion

        /// <summary>
        /// 滑动slider 让灯光能随着滑动x轴移动
        /// </summary>
        public void OnClickSlider()
        {
            m_LightPos.transform.localRotation = Quaternion.Euler(new Vector3(m_lightSlider.value, 330f, 0f));
        }


        /// <summary>
        /// 射线检测控制相机上下移动（itsUpDown.itsEnable = false）
        /// </summary>
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))//按下鼠标左键
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == _LightingUI)
                    {
                        HouseCameraStart.itsRotation.itsUpDown.itsEnable = false;
                    }
                }
                else
                {
                    HouseCameraStart.itsRotation.itsUpDown.itsEnable = true;
                }
            }
        }

        public void OnClickShowHomeBtn()
        {
            if (_HouseATog.isOn == true)
            {
                GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.ShowHomesAScenes;
                SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
            }

            if (_HouseBTog.isOn == true)
            {
                GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.ShowHomesBScenes;
                SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
            }
            //todo
            //if (_HouseCTog.isOn == true)
            //{
            //    GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.ShowHomesCScenes;
            //    SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
            //}
            //if (_HouseDTog.isOn == true)
            //{
            //    GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.ShowHomesDScenes;
            //    SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
            //}
        }
    }
}


