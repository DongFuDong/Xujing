/***
*   Title:"旭景清园"项目开发
*   [副标题]
*   Description：
*   [描述]  视图层：负责开始场景的显示部分
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
using Control;//视图层调用控制层

namespace View
{
    public class View_StartScenes : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
           // Ctrl_StartScenes.Instance.Start();
        }

        #region 区位规划部分
        /// <summary>
        /// 点击区位规划按钮
        /// </summary>
        public void ClickLcoationBtn()
        {
            //调用控制层脚本的点击“区位规划”方法
            Ctrl_StartScenes.Instance.ClickLcoationBtn();
        }
        /// <summary>
        /// 点击返回按钮
        /// </summary>
        public void ClickReturnBtn()
        {
            Ctrl_StartScenes.Instance.ClickReturnBtn();
        }
        /// <summary>
        /// 点击交通按钮
        /// </summary>
        public void ClickTrafficBtn()
        {
            Ctrl_StartScenes.Instance.ClickTrafficBtn();
        }

        public void ClickAroundBtn()
        {
            Ctrl_StartScenes.Instance.ClickAroundBtn();

        }

        /// <summary>
        /// 点击区为规划里面的地铁按钮
        /// </summary>
        public void ClickMetroBtn()
        {
            Ctrl_StartScenes.Instance.ClickMetroBtn();
        }

        public void ClickBusBtn()
        {
            Ctrl_StartScenes.Instance.ClickBusBtn();
        }

        #endregion
        
        /// <summary>
        /// 点击项目概况按钮
        /// </summary>
        public void ClickInformationBtn()
        {

            Ctrl_StartScenes.Instance.ClickInformationBtn();
        }

        /// <summary>
        /// 点击全景鸟瞰按钮
        /// </summary>
        public void ClickPanoramaBtn()
        {
            Ctrl_StartScenes.Instance.PanoramaBtn();
        }
        /// <summary>
        /// 点击项目简介按钮
        /// </summary>
        public void ClickProjectSignsBtn()
        {
            Ctrl_StartScenes.Instance.ProjectSignsBtn();
        }

        /// <summary>
        /// 点击周边环境按钮
        /// </summary>
        public void ClickEnvironmentalSignsBtn()
        {
            Ctrl_StartScenes.Instance.EnvironmentalSignsBtn();  
        }

        /// <summary>
        /// 点击道路规划按钮
        /// </summary>
        public void ClickPlanSignsBtn()
        {
            Ctrl_StartScenes.Instance.PlanSignsBtn();
        }

        /// <summary>
        /// 点击右上角地铁按钮
        /// </summary>
        public void ClickOneLevelTopMenuMetro()
        {
            Ctrl_StartScenes.Instance.OneLevelTopMenuMetroBtn();
        }
        /// <summary>
        /// 点击右上角公园按钮
        /// </summary>
        public void ClickOneLevelTopMenuPark()
        {
            Ctrl_StartScenes.Instance.OneLevelTopMenuParkBtn();
        }


        /// <summary>
        /// 点击右上角商业按钮
        /// </summary>
        public void ClickOneLevelTopMenuBusiness()
        {
            Ctrl_StartScenes.Instance.OneLevelTopMenuBusinessBtn();
        }
        /// <summary>
        /// 点击右上角学校按钮
        /// </summary>
        public void ClickOneLevelTopMenuSchool()
        {
            Ctrl_StartScenes.Instance.OneLevelTopMenuSchoolBtn();
        }
        /// <summary>
        /// 点击右上角医院按钮
        /// </summary>
        public void ClickOneLevelTopMenuHospital()
        {
            Ctrl_StartScenes.Instance.OneLevelTopMenuHospitalBtn();
            
        }

        /// <summary>
        /// 点击户型体验
        /// </summary>
        public void ClickHouseType()
        {
            Ctrl_StartScenes.Instance.HouseTypeBtn();
        }

        /// <summary>
        /// 点击按钮A
        /// </summary>
        public void ClickHouseA()
        {
            Ctrl_StartScenes.Instance.HouseABtn();
        }
        /// <summary>
        /// 点击按钮b
        /// </summary>
        public void ClickHouseB()
        {
            Ctrl_StartScenes.Instance.HouseBBtn();
        }
        /// <summary>
        /// 点击按钮c
        /// </summary>
        public void ClickHouseC()
        {
            Ctrl_StartScenes.Instance.HouseCBtn();
        }
        /// <summary>
        /// 点击按钮d
        /// </summary>
        public void ClickHouseD()
        {
            Ctrl_StartScenes.Instance.HouseDBtn();
        }
        /// <summary>
        /// 点击按钮East
        /// </summary>
        public void ClickHouseEast()
        {
            Ctrl_StartScenes.Instance.HouseEastBtn();
        }
        /// <summary>
        /// 点击按钮South
        /// </summary>
        public void ClickHouseSouth()
        {
            Ctrl_StartScenes.Instance.HouseSouthBtn();
        }
        /// <summary>
        /// 点击按钮West
        /// </summary>
        public void ClickHouseWest()
        {
            Ctrl_StartScenes.Instance.HouseWestBtn();
        }
        /// <summary>
        /// 点击按钮North
        /// </summary>
        public void ClickHouseNorth()
        {
            Ctrl_StartScenes.Instance.HouseNorthBtn();
        }

        /// <summary>
        /// 点击园林景观按钮
        /// </summary>
        public void ClickLandscape()
        {
            Ctrl_StartScenes.Instance.LandscapeBtn();
        }
        public void ClickGameContinue()
        {
            
        }
        /// <summary>
        /// 点击离开游戏按钮
        /// </summary>
        public void ClickExitBtn()
        {
            Ctrl_StartScenes.Instance.ClickExit();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClickFixBtn()
        {
            Ctrl_StartScenes.Instance.ClickFix();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClickCancelBtn()
        {
            Ctrl_StartScenes.Instance.ClickCancel();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            Ctrl_StartScenes.Instance.HouseBoxBtn();
        }

    }
}

