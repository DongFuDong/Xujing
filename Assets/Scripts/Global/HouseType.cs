/***
*   Title:"旭景清园"项目开发
*   [副标题]户型体验功能开发
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
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Global
{
public class HouseType : MonoBehaviour {

        public static HouseType Instance;//单例模式
        [SerializeField]
        private GameObject RightPanel;//定义屏幕右边的界面

        [SerializeField]
        private GameObject[] HouseABox;//定义户型a的box
        [SerializeField]
        private GameObject[] HouseBBox;
        [SerializeField]
        private GameObject[] HouseCBox;
        [SerializeField]
        private GameObject HouseDBox;

        private MeshRenderer HouseBoxMat;

        [SerializeField]
        private Toggle HouseABool;//定义toggle为后面做判断
        [SerializeField]
        private Toggle HouseBBool;
        [SerializeField]
        private Toggle HouseCBool;
        [SerializeField]
        private Toggle HouseDBool;
        [SerializeField]
        private ToggleGroup HouseTypeImage;//定义toggle的组以便后面重置toggle按钮
        [SerializeField]
        private ToggleGroup DirectionType;//同上
        [SerializeField]
        private Toggle HouseTypeBtnBool;
        [SerializeField]
        private Toggle EastToggleBool;
        [SerializeField]
        private Toggle SouthToggleBool;
        [SerializeField]
        private Toggle WestToggleBool;
        [SerializeField]
        private Toggle NorthToggleBool;


        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private GameObject[] _houseA;
        [SerializeField]
        private GameObject[] _houseB;
        [SerializeField]
        private GameObject[] _houseC;
        [SerializeField]
        private GameObject _houseD;

        private GameObject _HouseSceneHouseA;

        private int _houseInt = 0;//创建整数为了后面保存数据显示哪一个模型使用
        
        /// <summary>
        /// 本类实例
        /// </summary>
        private void Awake()
        {

            Instance = this;
        }
        /// <summary>
        /// 显示右边界面方法
        /// </summary>
        public void DisPlayRightPanel()
        {
            RightPanel.transform.DOLocalMoveX(765,0.5f);
            GamerContorl.Instance.HouseBtn();
        }
        /// <summary>
        /// 重置右边界面显示方法
        /// </summary>
        public void ResetRightPanelDisPlay()
        {
            RightPanel.transform.DOLocalMoveX(1200, 0.5f);
        }

        /// <summary>
        /// 点击户型a
        /// </summary>
        public void HouseAToggle()
        {
            if (HouseABool.isOn == true)
            {
                for (int i = 0; i < HouseABox.Length; i++)
                {
                    HouseABox[i].SetActive(true);
                    HouseBoxMat = HouseABox[i].GetComponent<MeshRenderer>();
                    HouseBoxMat.material.DOFade(0.4f, 1.5f).SetLoops(-1, LoopType.Yoyo);
                }
            }
            else
            {
                for (int i = 0; i < HouseABox.Length; i++)
                {
                    HouseABox[i].SetActive(false);
                }
            }

        }
        public void HouseBToggle()
        {
            if (HouseBBool.isOn == true)
            {
                for (int i = 0; i < HouseBBox.Length; i++)
                {
                    HouseBBox[i].SetActive(true);
                    HouseBoxMat = HouseBBox[i].GetComponent<MeshRenderer>();
                    HouseBoxMat.material.DOFade(0.4f, 1.5f).SetLoops(-1, LoopType.Yoyo);
                }
            }
            else
            {
                for (int i = 0; i < HouseBBox.Length; i++)
                {
                    HouseBBox[i].SetActive(false);
                }
            }

        }
        /// <summary>
        /// 点击户型c
        /// </summary>
        public void HouseCToggle()
        {
            if (HouseCBool.isOn == true)
            {
                for (int i = 0; i < HouseCBox.Length; i++)
                {
                    HouseCBox[i].SetActive(true);
                    HouseBoxMat = HouseCBox[i].GetComponent<MeshRenderer>();
                    HouseBoxMat.material.DOFade(0.4f, 1.5f).SetLoops(-1, LoopType.Yoyo);
                }
            }
            else
            {
                for (int i = 0; i < HouseCBox.Length; i++)
                {
                    HouseCBox[i].SetActive(false);
                }
            }

        }
        /// <summary>
        /// 点击户型d
        /// </summary>
        public void HouseDToggle()
        {
            if (HouseDBool.isOn == true)
            {
                HouseDBox.SetActive(true);
                HouseBoxMat = HouseDBox.GetComponent<MeshRenderer>();
                HouseBoxMat.material.DOFade(0.4f, 1.5f).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                HouseDBox.SetActive(false);
            }

        }

        /// <summary>
        /// 点击朝向东
        /// </summary>
        public void HouseEastToggle()
        {
            GamerContorl.Instance.HouseEast();
        }
        /// <summary>
        /// 点击朝向南
        /// </summary>
        public void HouseSouthToggle()
        {
            GamerContorl.Instance.HouseSouth();
        }
        /// <summary>
        /// 点击朝向西
        /// </summary>
        public void HouseWestToggle()
        {
            GamerContorl.Instance.HouseWest();
        }
        /// <summary>
        /// 点击朝向北
        /// </summary>
        public void HouseNorthToggle()
        {
            GamerContorl.Instance.HouseNorth();
        }

        public void ResetHouseToggle()
        {
            if (HouseTypeBtnBool.isOn == false)
            {
                HouseTypeImage.allowSwitchOff = true;
                HouseABool.isOn = false;
                HouseBBool.isOn = false;
                HouseCBool.isOn = false;
                HouseDBool.isOn = false;


                DirectionType.allowSwitchOff =true;
                EastToggleBool.isOn = false;
                SouthToggleBool.isOn = false;
                WestToggleBool.isOn = false;
                NorthToggleBool.isOn = false;
            }
        }
        /// <summary>
        /// 点击户型功能下Box来切换场景
        /// </summary>
        public void ClickHouseBoxBtn()
        {
            if (Input.GetMouseButtonDown(0))//按下鼠标左键
            {
                //摄像机视角范围内发射一条射线
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                //储存射线的信息
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //如果射线碰撞到物体（碰到的是A户型的Box）
                    for (int i = 0; i < _houseA.Length; i++)
                    {
                        if (hit.collider.gameObject == _houseA[i])
                        {
                            //加载方法
                            LoadingHouseScenes();
                            _houseInt = 1;
                            PlayerPrefs.SetInt("_houseInt", _houseInt);
                        }
                    }
                    //如果射线碰撞到物体（碰到的是B户型的Box）
                    for (int i = 0; i < _houseB.Length; i++)
                    {
                        if (hit.collider.gameObject == _houseB[i])
                        {
                            LoadingHouseScenes();
                            _houseInt = 2;
                            PlayerPrefs.SetInt("_houseInt", _houseInt);
                        }
                    }
                    //如果射线碰撞到物体（碰到的是C户型的Box）
                    for (int i = 0; i < _houseC.Length; i++)
                    {
                        if (hit.collider.gameObject == _houseC[i])
                        {
                            LoadingHouseScenes();
                            _houseInt = 3;
                            PlayerPrefs.SetInt("_houseInt", _houseInt);
                        }
                    }
                    // 如果射线碰撞到物体（碰到的是D户型的Box）
                    if (hit.collider.gameObject == _houseD)
                    {
                        LoadingHouseScenes();
                        _houseInt = 4;
                        PlayerPrefs.SetInt("_houseInt", _houseInt);
                    }
                }
            }

        }
        /// <summary>
        /// 加载方法
        /// </summary>
        public void LoadingHouseScenes()
        {
            GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.HouseScenes;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
        }

    }
}

