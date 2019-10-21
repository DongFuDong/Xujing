/***
*   Title:"旭景清园"项目开发
*   控制层：开始场景
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
using Global;


namespace Control
{
    public class Ctrl_StartScenes : MonoBehaviour
    {
       
        /// <summary>
        /// 单例
        /// </summary>
        public static Ctrl_StartScenes Instance;

        private void Awake()
        {
            Instance = this;

        }

        internal void Start()
        {
          // FadeInAndOut.Instance.SetSceneToClear();//
        }

        #region 点击区位规划
        /// <summary>
        /// LcoationBtnScale()方法
        /// </summary>
        internal void ClickLcoationBtn()
        {
            // 区为规划界面的Y放大
            LcoationPanel.Instance.LcoationPanelEnlarge();
            //重置项目简介按钮下面的二级菜单
            InformationPanel.Instance.ResetInformationBtns();
            // 重置周边环境右上角的界面
            InformationPanel.Instance.ResetEnvironmentalSigns();
            //重置项目简介
            InformationPanel.Instance.ResetProjectSigns();
            //重置右上角菜单
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            // 重置道路规划
            InformationPanel.Instance.ResetPlanSigns();
            //重置户型体验重置右边界面显示方法
            HouseType.Instance.ResetRightPanelDisPlay();
            HouseType.Instance.ResetHouseToggle();


        }
        /// <summary>
        /// 在区位规划页面点击返回按钮
        /// </summary>
        internal void ClickReturnBtn()
        {

            LcoationPanel.Instance.ReturnBtn();
            //PanoramaPanel.Instance.PanoramaSigns();
            
        }

        /// <summary>
        /// 在区位规划页面点击交通按钮
        /// </summary>
        internal void ClickTrafficBtn()
        {
            LcoationPanel.Instance.TrafficBtn();


        }

        /// <summary>
        /// 在区位规划页面点击周边按钮
        /// </summary>
        internal void ClickAroundBtn()
        {
            LcoationPanel.Instance.AroundBtn();

        }


        /// <summary>
        /// 点击区为规划交通环境的地铁按钮
        /// </summary>
        internal void ClickMetroBtn()
        {
            LcoationPanel.Instance.MetroBtn();
        }
        /// <summary>
        /// 点击公交按钮
        /// </summary>
        internal void ClickBusBtn()
        {
            LcoationPanel.Instance.BusBtn();
        }

        #endregion

        #region 点击项目概况
        /// <summary>
        /// 点击项目概况按钮
        /// </summary>
        internal void ClickInformationBtn()
        {
            LcoationPanel.Instance.LcoationPanelNarrow();
            InformationPanel.Instance.InformationBtn();
            HouseType.Instance.ResetRightPanelDisPlay();
            HouseType.Instance.ResetHouseToggle();

        }
        #endregion

        #region 点击项目概况二级菜单

        /// <summary>
        /// 点击项目简介 
        /// </summary>
        internal void ProjectSignsBtn()
        {
            InformationPanel.Instance.ProjectSigns();

            InformationPanel.Instance.ResetEnvironmentalSigns();
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            InformationPanel.Instance.ResetPlanSigns();
            //
            //InformationPanel.Instance.ResetOneLevelTopMenuToggle();
        }
        /// <summary>
        /// 点击周边环境
        /// </summary>
        internal void EnvironmentalSignsBtn()
        {

            InformationPanel.Instance.EnvironmentalSigns();
            InformationPanel.Instance.ResetProjectSigns();
            InformationPanel.Instance.ResetPlanSigns();
        }



        /// <summary>
        /// 点击道路规划 
        /// </summary>
        internal void PlanSignsBtn()
        {
            InformationPanel.Instance.PlanSigns();

            InformationPanel.Instance.ResetEnvironmentalSigns();
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            InformationPanel.Instance.ResetProjectSigns();

            InformationPanel.Instance.ResetOneLevelTopMenuToggle();
        }

        #endregion

        #region 点击项目概况二级按钮周边环境后的方法
        /// <summary>
        /// 点击右上角地铁按钮
        /// </summary>
        internal void OneLevelTopMenuMetroBtn()
        {
            //调用地铁方法
            InformationPanel.Instance.OneLevelTopMenuMetro();

            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            
        }
        /// <summary>
        /// 点击右上角公园按钮
        /// </summary>
        internal void OneLevelTopMenuParkBtn()
        {
            //调用公园方法
            InformationPanel.Instance.OneLevelTopMenuPark();

            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            
        }

        /// <summary>
        /// 点击右上角商业区按钮
        /// </summary>
        internal void OneLevelTopMenuBusinessBtn()
        {
            //调用商业区方法
            InformationPanel.Instance.OneLevelTopMenuBusiness();

            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            
        }

        /// <summary>
        /// 点击右上角学校按钮
        /// </summary>
        internal void OneLevelTopMenuSchoolBtn()
        {
            //调用学校方法
            InformationPanel.Instance.OneLevelTopMenuSchool();

            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            
        }
        /// <summary>
        /// 点击右上角医院按钮
        /// </summary>
        internal void OneLevelTopMenuHospitalBtn()
        {
            InformationPanel.Instance.OneLevelTopMenuHospital();

            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();

        }
        #endregion

        #region 点击户型体验
        /// <summary>
        /// 点击户型体验
        /// </summary>
        internal void HouseTypeBtn()
        {
            LcoationPanel.Instance.LcoationPanelNarrow();
            HouseType.Instance.DisPlayRightPanel();
            // 重置周边环境右上角的界面
            InformationPanel.Instance.ResetEnvironmentalSigns();
            //重置右上角菜单
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            // 重置项目简介
            InformationPanel.Instance.ResetProjectSigns();
            
            //重置道路规划
            InformationPanel.Instance.ResetPlanSigns();
          

        }
        /// <summary>
        /// 点击按钮a
        /// </summary>
        internal void HouseABtn()
        {
            HouseType.Instance.HouseAToggle();


        }
        /// <summary>
        /// 点击按钮b
        /// </summary>
        internal void HouseBBtn()
        {
            HouseType.Instance.HouseBToggle();
        }
        /// <summary>
        /// 点击按钮c
        /// </summary>
        internal void HouseCBtn()
        {
            HouseType.Instance.HouseCToggle();
        }
        /// <summary>
        /// 点击按钮d
        /// </summary>
        internal void HouseDBtn()
        {
            HouseType.Instance.HouseDToggle();
        }
        /// <summary>
        /// 点击按钮East
        /// </summary>
        internal void HouseEastBtn()
        {
            HouseType.Instance.HouseEastToggle();
        }
        /// <summary>
        /// 点击按钮South
        /// </summary>
        internal void HouseSouthBtn()
        {
            HouseType.Instance.HouseSouthToggle();
        }
        /// <summary>
        /// 点击按钮West
        /// </summary>
        internal void HouseWestBtn()
        {
            HouseType.Instance.HouseWestToggle();
        }
        /// <summary>
        /// 点击按钮North
        /// </summary>
        internal void HouseNorthBtn()
        {
            HouseType.Instance.HouseNorthToggle();
        }

        internal void HouseBoxBtn()
        {
            HouseType.Instance.ClickHouseBoxBtn();
        }

        #endregion

        #region 点击全景鸟瞰 
        /// <summary>
        /// 点击全景鸟瞰按钮
        /// </summary>
        internal void PanoramaBtn()
        {
            LcoationPanel.Instance.LcoationPanelNarrow();
            PanoramaPanel.Instance.PanoramaSigns();
            // 重置周边环境右上角的界面
            InformationPanel.Instance.ResetEnvironmentalSigns();
            //重置右上角菜单
            InformationPanel.Instance.ResetOneLevelTopMenuMetro();
            InformationPanel.Instance.ResetOneLevelTopMenuPark();
            InformationPanel.Instance.ResetOneLevelTopMenuBusiness();
            InformationPanel.Instance.ResetOneLevelTopMenuSchool();
            InformationPanel.Instance.ResetOneLevelTopMenuHospital();
            // 重置项目简介
            InformationPanel.Instance.ResetProjectSigns();
            //重置项目简介按钮下面的二级菜单
            InformationPanel.Instance.ResetInformationBtns();
            //重置道路规划
            InformationPanel.Instance.ResetPlanSigns();
            //重置户型体验重置右边界面显示方法
            HouseType.Instance.ResetRightPanelDisPlay();
        }
        #endregion

        internal void LandscapeBtn()
        {
            LandscapePanel.Instance.OnLandscape();
        }

        /// <summary>
        /// 点击离开按钮
        /// </summary>
        internal void ClickExit()
        {
            LcoationPanel.Instance.LcoationPanelNarrow();

            ExitBtnToggle.Instance.ExitBtn();

        }
        /// <summary>
        ///点击退出按钮下面的确认按钮
        /// </summary>
        internal void ClickFix()
        {
            ExitBtnToggle.Instance.FixBtn();
        }
        /// <summary>
        /// 点击退出按钮下面的取消按钮
        /// </summary>
        internal void ClickCancel()
        {
            ExitBtnToggle.Instance.CancelBtn();
        }
    }
}
