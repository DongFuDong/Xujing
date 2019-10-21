/***
*   Title:"旭景清园"项目开发
*   [副标题]项目概况界面
*   Description：
*   [描述] 区位规划页面
*   Date:2019/01/29
*   Version:0.1
*   Developer:lixin
*   ModifyRecoder:
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Global
{
    public class InformationPanel : MonoBehaviour
    {
        public static InformationPanel Instance; //单例
        [SerializeField]
        public GameObject TowMenu;//定义点击周边环境按钮后显示的界面
        [SerializeField]
        private GameObject OneLevelTopMenu;//定义右上角的界面（一遍点击周边环境后控制）

        [SerializeField]
        private GameObject[] MetroBoxs;//定义地铁高亮显示的盒子的数组
        private MeshRenderer MetroBoxMat;//定义盒子的材质
        [SerializeField]
        private GameObject[] ParkBoxs;//
        private MeshRenderer ParkBoxsMat;//

        [SerializeField]
        private GameObject BusinessBox;//
        private MeshRenderer BusinessBoxMat;//

        [SerializeField]
        private GameObject SchoolBox;//
        private MeshRenderer SchoolBoxMat;//
        [SerializeField]
        private GameObject HospitalBox;//
        private MeshRenderer HospitalBoxMat;//

        [SerializeField]
        private float FadeColorValue = 0.4f;//
        [SerializeField]
        private float FadeColorSpeed = 1.5f;//

        [SerializeField]
        private GameObject MetroImage;//
        [SerializeField]
        private GameObject ParkImage;//
        [SerializeField]
        private GameObject BusinessImage;//
        [SerializeField]
        private GameObject SchoolImage;//
        [SerializeField]
        private GameObject HospitalImage;//

        [SerializeField]
        private GameObject ProjectPanel;//
        private Image ProjectPanelImage;//

        [SerializeField]
        //private GameObject PlanPanel;//
        private GameObject _road;
        //private Image PlanPanelImage;//
        [SerializeField]
        private GameObject CarLine;//

        //定义右上角Toggle
        [SerializeField]
        private Toggle MetroToggle;//
        [SerializeField]
        private Toggle ParkToggle;//
        [SerializeField]
        private Toggle BusinessToggle;//
        [SerializeField]
        private Toggle SchoolToggle;//
        [SerializeField]
        private Toggle HospitalToggle;//
        [SerializeField]
        private ToggleGroup OneLevelTopMenuGroup;
        //[SerializeField]
        //private Toggle _informationBtn; 


        [SerializeField]
        private Toggle EnvironmentalSignsToggle;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            //获取项目简介图片上面的Image组件
            ProjectPanelImage = ProjectPanel.GetComponent<Image>();
            //PlanPanelImage = PlanPanel.GetComponent<Image>();
        }
        /// <summary>
        /// 点击项目概况
        /// </summary>
        public void InformationBtn()
        {
            
                TowMenu.SetActive(true);
         
                
        }

        /// <summary>
        /// 重置项目概况
        /// </summary>
        public void ResetInformationBtns()
        {
            TowMenu.SetActive(false);
        }

        /// <summary>
        /// 项目简介 
        /// </summary>
        public void ProjectSigns()
        {
                if (ProjectPanelImage.color.a == 0)
                {
                    ProjectPanel.SetActive(true);
                    ProjectPanelImage.DOFade(1, 0.6f).SetDelay(2f);
                }
                GamerContorl.Instance.ProjectSigns();   
        }

        /// <summary>
        /// 重置项目简介
        /// </summary>
        public void ResetProjectSigns()
        {

            ProjectPanel.SetActive(false);
            ProjectPanelImage.color = new Color(1, 1, 1, 0);
        }

        #region 点击项目概况后周边环境的方法
        /// <summary>
        /// 周边环境显示右上角界面
        /// </summary>
        public void EnvironmentalSigns()
        {
            OneLevelTopMenu.transform.DOLocalMoveY(675, 0.3f);

        }

        /// <summary>
        /// 重置周边环境右上角的界面
        /// </summary>
        public void ResetEnvironmentalSigns()
        {
            OneLevelTopMenu.transform.DOLocalMoveY(1110, 0.3f);
        }
        #endregion
        /// <summary>
        /// 道路规划 
        /// </summary>
        public void PlanSigns()
        {
            //if (PlanPanelImage.color.a == 0)
            //{
            //    PlanPanel.SetActive(true);
            //    PlanPanelImage.DOFade(1, 0.6f).SetDelay(2f);
            //}
            _road.SetActive (true);
            GamerContorl.Instance.PlanSigns();
            CarLine.SetActive(true);

        }
        /// <summary>
        /// 重置道路规划
        /// </summary>
        public void ResetPlanSigns()
        {
            //PlanPanel.SetActive(false);
            _road.SetActive (false);
            CarLine.SetActive(false);
            //PlanPanelImage.color = new Color(1, 1, 1, 0);
        }
        #region 点击项目概况后再点击周边环境弹出的右上角界面的方法
        /// <summary>
        /// 右上角界面 地铁部分
        /// </summary>
        public void OneLevelTopMenuMetro()
        {
            if (MetroToggle.isOn == true)
            {
                //遍历盒子数组
                for (int i = 0; i < MetroBoxs.Length; i++)
                {
                    //显示所有的地铁盒子
                    MetroBoxs[i].SetActive(true);
                    //获取这些盒子的材质
                    MetroBoxMat = MetroBoxs[i].GetComponent<MeshRenderer>();
                    //使用DOTween让盒子做颜色渐变循环
                    MetroBoxMat.material.DOFade(FadeColorValue, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
                }
                //
                MetroImage.transform.DOLocalMoveX(-560, 0.5f).SetDelay(1.5f);
                //调用 GamerContorl脚本里面切换摄像机视角的方法
                GamerContorl.Instance.MetroCameras();
            }
        }
        /// <summary>
        /// 重置右上角界面地铁部分方法
        /// </summary>
        public void ResetOneLevelTopMenuMetro()
        {
            //遍历盒子数组
            for (int i = 0; i < MetroBoxs.Length; i++)
            {
                //显示所有的地铁盒子
                MetroBoxs[i].SetActive(false);
                MetroBoxMat = MetroBoxs[i].GetComponent<MeshRenderer>();
                MetroBoxMat.material.DOFade(0f, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
            }
            MetroImage.transform.DOLocalMoveX(-1200, 0.5f);
        }
        /// <summary>
        /// 右上角界面公园部分
        /// </summary>
        public void OneLevelTopMenuPark()
        {
            if (ParkToggle.isOn == true)
            {
                for (int i = 0; i < ParkBoxs.Length; i++)
                {
                    ParkBoxs[i].SetActive(true);
                    ParkBoxsMat = ParkBoxs[i].GetComponent<MeshRenderer>();
                    ParkBoxsMat.material.DOFade(FadeColorValue, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
                }
                ParkImage.transform.DOLocalMoveX(-560, 0.5f).SetDelay(1.5f);
                GamerContorl.Instance.ParkCameras();
            }

        }
        /// <summary>
        /// 重置右上角界面公园部分
        /// </summary>
        public void ResetOneLevelTopMenuPark()
        {
            for (int i = 0; i < ParkBoxs.Length; i++)
            {
                ParkBoxs[i].SetActive(false);
                ParkBoxsMat = ParkBoxs[i].GetComponent<MeshRenderer>();
                ParkBoxsMat.material.DOFade(0f, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
            }
            ParkImage.transform.DOLocalMoveX(-1200, 0.5f);
        }

        /// <summary>
        /// 右上角界面 商业部分
        /// </summary>
        public void OneLevelTopMenuBusiness()
        {
            if (BusinessToggle.isOn == true)
            {
                BusinessBox.SetActive(true);
                BusinessBoxMat = BusinessBox.GetComponent<MeshRenderer>();
                BusinessBoxMat.material.DOFade(FadeColorValue, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
                BusinessImage.transform.DOLocalMoveX(-560, 0.5f).SetDelay(1.5f);
                GamerContorl.Instance.BusinessCameras();
            }
        }
        /// <summary>
        /// 重置商业部分
        /// </summary>
        public void ResetOneLevelTopMenuBusiness()
        {
            BusinessBox.SetActive(false);
            BusinessBoxMat = BusinessBox.GetComponent<MeshRenderer>();
            BusinessBoxMat.material.DOFade(0f, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
            BusinessImage.transform.DOLocalMoveX(-1200, 0.5f);
        }
        /// <summary>
        /// 右上角界面 学校部分
        /// </summary>
        public void OneLevelTopMenuSchool()
        {
            if (SchoolToggle.isOn == true)
            {
                SchoolBox.SetActive(true);
                SchoolBoxMat = SchoolBox.GetComponent<MeshRenderer>();
                SchoolBoxMat.material.DOFade(FadeColorValue, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
                SchoolImage.transform.DOLocalMoveX(-560, 0.5f).SetDelay(1.5f);
                GamerContorl.Instance.SchoolCameras();
            }
        }
        /// <summary>
        /// 右上角界面 重置学校部分
        /// </summary>
        public void ResetOneLevelTopMenuSchool()
        {
            SchoolBox.SetActive(false);
            SchoolBoxMat = SchoolBox.GetComponent<MeshRenderer>();
            SchoolBoxMat.material.DOFade(0f, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
            SchoolImage.transform.DOLocalMoveX(-1200, 0.5f);
        }
        /// <summary>
        /// 右上角界面 医院部分
        /// </summary>
        public void OneLevelTopMenuHospital()
        {
            if (HospitalToggle.isOn == true)
            {
                HospitalBox.SetActive(true);
                HospitalBoxMat = HospitalBox.GetComponent<MeshRenderer>();
                HospitalBoxMat.material.DOFade(FadeColorValue, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
                HospitalImage.transform.DOLocalMoveX(-560, 0.5f).SetDelay(1.5f);
                GamerContorl.Instance.HospitalCameras();
            }

        }
        /// <summary>
        /// 重置右上角界面 医院部分
        /// </summary>
        public void ResetOneLevelTopMenuHospital()
        {
            HospitalBox.SetActive(false);
            HospitalBoxMat = HospitalBox.GetComponent<MeshRenderer>();
            HospitalBoxMat.material.DOFade(0f, FadeColorSpeed).SetLoops(-1, LoopType.Yoyo);
            HospitalImage.transform.DOLocalMoveX(-1200, 0.5f);
        }
        #endregion
        /// <summary>
        /// 重置右上角所有的Toggle
        /// </summary>
        public void ResetOneLevelTopMenuToggle()
        {
            if (EnvironmentalSignsToggle.isOn == false)
            {
                OneLevelTopMenuGroup.allowSwitchOff = true;
                MetroToggle.isOn = false;
                ParkToggle.isOn = false;
                BusinessToggle.isOn = false;
                SchoolToggle.isOn = false;
                HospitalToggle.isOn = false;
            }
        }
    }
}

