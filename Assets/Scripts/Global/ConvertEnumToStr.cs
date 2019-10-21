/***
*   Title:"旭景清园"项目开发
*   全局类型：枚举类型转换字符串
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


namespace Global
{
    public class ConvertEnumToStr
    {
        private static ConvertEnumToStr Instance;
        //枚举场景类型集合
        private Dictionary<GlobalParameter.ScenesEnum, string> _DicScenesEnumLib;
        /// <summary>
        /// 构造函数
        /// </summary>
        private ConvertEnumToStr()
        {
            _DicScenesEnumLib = new Dictionary<GlobalParameter.ScenesEnum, string>();
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.StartScenes, "StartScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.MainScenes, "MainScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.LoadingScenes, "LoadingScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.HouseScenes, "HouseScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.ShowHomesAScenes, "ShowHomesAScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.ShowHomesBScenes, "ShowHomesBScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.ShowHomesCScenes, "ShowHomesCScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.ShowHomesDScenes, "ShowHomesDScenes");
            _DicScenesEnumLib.Add(GlobalParameter.ScenesEnum.LandScapeScenes, "LandScapeScenes");


        }
        public static ConvertEnumToStr GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ConvertEnumToStr();
            }
            return Instance;
        }
        /// <summary>
        /// 得到字符串形式的场景名称
        /// </summary>
        /// <param name="scenesEnum">枚举类型的场景名称</param>
        /// <returns></returns>
        public string GetStrByEnumScenes(GlobalParameter.ScenesEnum scenesEnum)
        {
            if (_DicScenesEnumLib != null && _DicScenesEnumLib.Count >= 1)
            {
                return _DicScenesEnumLib[scenesEnum];
            }
            else
            {
                Debug.LogWarning(GetType() + "/GetStrByEnumScenes()/_DicScenesEnumLib.count<=0!,检查");
                return null;
            }
        }
    }
}
