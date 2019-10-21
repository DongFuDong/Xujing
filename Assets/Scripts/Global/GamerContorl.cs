using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Global
{
    public class GamerContorl : MonoBehaviour
    {



        public static GamerContorl Instance;

        public KGFOrbitCam orbitCam;
        public KGFOrbitCamSettings itsEventSwitchToMain;
        public KGFOrbitCamSettings itsEventSwitchToMetro;
        public KGFOrbitCamSettings itsEventSwitchToPark;
        public KGFOrbitCamSettings itsEventSwitchToBusiness;
        public KGFOrbitCamSettings itsEventSwitchToSchool;
        public KGFOrbitCamSettings itsEventSwitchToHospital;
        public KGFOrbitCamSettings itsEventSwitchToProjectSigns;
        public KGFOrbitCamSettings itsEventSwitchToPlanSigns;

        public KGFOrbitCamSettings itsEventSwitchToHouseBtn;
        public KGFOrbitCamSettings itsEventSwitchToHouseEast;
        public KGFOrbitCamSettings itsEventSwitchToHouseSouth;
        public KGFOrbitCamSettings itsEventSwitchToHouseWest;
        public KGFOrbitCamSettings itsEventSwitchToHouseNorth;



        private eCameraRoot itsCurrentCameraRoot = eCameraRoot.MainScenes;
        public enum eCameraRoot
        {
            MainScenes,
            MetroScenes,
            ParkScenes,
            BusinessScenes,
            SchoolScenes,
            HospitalScenes,
            ProjectSigns,
            PlanSigns,
            HouseBtn,
            HouseEast,
            HouseSouth,
            HouseWest,
            HouseNorth
         

            //eUpSideDown,
            //eFishEye
        }


        private void Awake()
        {
            Instance = this;
        }

        
        void Start()
        {
            itsEventSwitchToMain.Apply();
            itsCurrentCameraRoot = eCameraRoot.MainScenes;
            //orbitCam.EnableInput(true);
        }



        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0) || Input.touchCount == 1)
            {
                orbitCam.EnableInput(true);

            }
            else
            {
                orbitCam.EnableInput(false);

            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                orbitCam.SetTargetZoom(orbitCam.GetCurrentZoom() - Input.GetAxis("Mouse ScrollWheel") * orbitCam.GetZoomAxisSensitivity() * orbitCam.GetZoomSpeed());
            }
        }

        /// <summary>
        /// 镜头转换的地铁
        /// </summary>
        public void MetroCameras()
        {
            itsEventSwitchToMetro.Apply();
            itsCurrentCameraRoot = eCameraRoot.MetroScenes;

        }
        /// <summary>
        /// 镜头转换的公园
        /// </summary>
        public void ParkCameras()
        {
            itsEventSwitchToPark.Apply();
            itsCurrentCameraRoot = eCameraRoot.ParkScenes;
        }
        /// <summary>
        /// 镜头转到商业区
        /// </summary>
        public void BusinessCameras()
        {
            itsEventSwitchToBusiness.Apply();
            itsCurrentCameraRoot = eCameraRoot.BusinessScenes;
        }
        /// <summary>
        /// 镜头旋转到学校
        /// </summary>
        public void SchoolCameras()
        {
            itsEventSwitchToSchool.Apply();
            itsCurrentCameraRoot = eCameraRoot.SchoolScenes;
        }
        /// <summary>
        /// 镜头旋转到医院
        /// </summary>
        public void HospitalCameras()
        {
            itsEventSwitchToHospital.Apply();
            itsCurrentCameraRoot = eCameraRoot.HospitalScenes;

        }

        /// <summary>
        /// 镜头旋转到全景鸟瞰
        /// </summary>
        public void PanoramaCamaera()
        {
            itsEventSwitchToMain.Apply();
            itsCurrentCameraRoot = eCameraRoot.MainScenes;
        }
        /// <summary>
        /// 镜头旋转到项目简介
        /// </summary>
        public void ProjectSigns()
        {
            itsEventSwitchToProjectSigns.Apply();
            itsCurrentCameraRoot = eCameraRoot.ProjectSigns;
        }
        /// <summary>
        /// 镜头旋转到道路规划
        /// </summary>
        public void PlanSigns()
        {
            itsEventSwitchToPlanSigns.Apply();
            itsCurrentCameraRoot = eCameraRoot.PlanSigns;
        }

        /// <summary>
        /// 镜头转到户型按钮
        /// </summary>
        public void HouseBtn()
        {
            itsEventSwitchToHouseBtn.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseBtn;
        }
        /// <summary>
        /// 镜头旋转到户型东
        /// </summary>
        public void HouseEast()
        {
            itsEventSwitchToHouseEast.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseEast;
        }
        /// <summary>
        /// 镜头旋转到户型南
        /// </summary>
        public void HouseSouth()
        {
            itsEventSwitchToHouseSouth.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseSouth;
        }
        /// <summary>
        /// 镜头旋转到户型西
        /// </summary>
        public void HouseWest()
        {
            itsEventSwitchToHouseWest.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseWest;
        }
        /// <summary>
        /// 镜头旋转到户型北
        /// </summary>
        public void HouseNorth()
        {
            itsEventSwitchToHouseNorth.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseNorth;
        }

      
     
    }

}

