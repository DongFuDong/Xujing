/***
*   Title:"旭景清园"项目开发
*   [副标题] 全局参数
*   Description：
*   [描述] 
*   1 定义整个项目的枚举
*   2 定义整个项目的系统常量
*   3
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


namespace Global
{
    public class GlobalParameter
    {
        public static ScenesEnum NextScenesName = ScenesEnum.MainScenes;
        public enum ScenesEnum
        {
            StartScenes,
            MainScenes,
            LoadingScenes,
            HouseScenes,
            ShowHomesAScenes,
            ShowHomesBScenes,
            ShowHomesCScenes,
            ShowHomesDScenes,
            LandScapeScenes
        }

        //public const string STARTSCENES = "StartScenes";
        //public const string MAINSCENES = "MainScenes";
        //public const string LOADINGSCENE = "LoadingScenes";
        //public const string HOUSSESCENES = "HouseScenes";
        //public const string SHOWHOMESASCENES = "ShowHomesAScenes";
        //public const string SHOWHOMESBSCENES = "ShowHomesBScenes";
        //public const string SHOWHOMESCSCENES = "ShowHomesCScenes";
        //public const string SHOWHOMESDSCENES = "ShowHomesDScenes";

        //public const string LANDSCAPESCENES = "LandScapeScenes";

    }
}

