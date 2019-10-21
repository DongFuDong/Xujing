///***
//*   Title:"旭景清园"项目开发
//*   [副标题]
//*   Description：
//*   [描述]
//*   Date:2019/01/29
//*   Version:0.1
//*   Developer:lixin
//*   ModifyRecoder:
//*
//*
//*
//*
//*
//*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Global
//{
//public class ToggleCtrl : MonoBehaviour {
//        [SerializeField]
//        private Toggle LcoationBtn;
//        [SerializeField]
//        private Toggle InformationBtn;
//        [SerializeField]
//        private Toggle HouseTypeBtn;
//        [SerializeField]
//        private Toggle PanoramaBtn;
//        [SerializeField]
//        private Toggle ThreeDBtn;
//        [SerializeField]
//        private Toggle LandscapeBtn;
//        //[SerializeField]
//        //private Toggle ExitBtn;
//        [SerializeField]
//        private Toggle ProjectSignsToggle;
//        [SerializeField]
//        private Toggle EnvironmentalSignsToggle;
//        [SerializeField]
//        private Toggle PlanSignsToggle;
//        [SerializeField]
//        private Toggle TrafficBtn;
//        [SerializeField]
//        private Toggle AroundBtn;
//        [SerializeField]
//        private Toggle BusToggle;
//        [SerializeField]
//        private Toggle MetroToggle;
//        [SerializeField]
//        private Toggle TopMetroToggle;
//        [SerializeField]
//        private Toggle TopParkToggle;
//        [SerializeField]
//        private Toggle TopBusinessToggle;
//        [SerializeField]
//        private Toggle TopSchoolToggle;
//        [SerializeField]
//        private Toggle TopHospitalToggle;
//        //public enum OneToogleIsOn
//        //{
//        //    Lcoation,
//        //    Information,
//        //    HouseType,
//        //    LcoationBtn,
//        //    Panorama,
//        //    ThreeD,
//        //    Landscape,
//        //    ExitBtn
//        //}
//        //private OneToogleIsOn itsCurrentOneToogleIsOn = OneToogleIsOn.Panorama;
//        public static ToggleCtrl Instance;
//        private void Awake()
//        {
//            Instance = this;
//        }

//        private void Update()
//        {
//            if (InformationBtn.isOn != true)
//            {
//                ProjectSignsToggle.isOn = false;
//                EnvironmentalSignsToggle.isOn = false;
//                PlanSignsToggle.isOn = false;
//            }

//            if (LcoationBtn.isOn != true)
//            {
//                TrafficBtn.isOn = false;
//                AroundBtn.isOn = false;
//            }
//            if (TrafficBtn.isOn != true)
//            {
//                BusToggle.isOn = false;
//                MetroToggle.isOn = false;
//            }

//            if (EnvironmentalSignsToggle != true)
//            {
//                TopMetroToggle.isOn = false;
//                TopParkToggle.isOn = false;
//                TopBusinessToggle.isOn = false;
//                TopSchoolToggle.isOn = false;
//                TopHospitalToggle.isOn = false;
//            }
//        }
//    }
//}

