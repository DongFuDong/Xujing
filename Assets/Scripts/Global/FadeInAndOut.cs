/***
*   Title:"旭景清园"项目开发
*   公共层：场景淡入淡出
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
    public class FadeInAndOut : MonoBehaviour
    {
        /// <summary>
        /// 单例
        /// </summary>
        //public static FadeInAndOut Instance;

        public float BGColorChangeSpeed = 0.5f;
        public float LogoColorChangeSpeed = 0.5f;
        public GameObject StartBG;//做淡入淡出的物体
        public GameObject StartLogo;//开始Logo
        private Image _ImageBG;//这个物体上的Image组件
        private Image _ImageLogo;//
        private bool _BoolSceneToClear= false;
        private void Awake()
        {
            //Instance = this;
            //得到物体身上的组件
            if (StartBG)
            {
                _ImageBG = StartBG.GetComponent<Image>();
            }
            if (StartLogo)
            {
                _ImageLogo = StartLogo.GetComponent<Image>();
            }
        }

        private void Start()
        {
            SetSceneToClear();
        }
        /// <summary>
        /// 开始淡入（屏幕逐渐清晰）
        /// </summary>
        private void StartBGFadeToClear()
        {
            _ImageBG.color = Color.Lerp(_ImageBG.color, Color.clear, BGColorChangeSpeed * Time.deltaTime);
        }

        ///// <summary>
        ///// Logo淡出（屏幕逐渐黯淡）
        ///// </summary>
        //private void StarLogoFadeToClear()
        //{
        //    _ImageLogo.color = Color.Lerp(_ImageLogo.color, Color.clear, LogoColorChangeSpeed * Time.deltaTime);
        //}

        /// <summary>
        /// 开始淡出的方法
        /// </summary>
        private void StartBGToClear()
        {
            StartBGFadeToClear();
            if (_ImageBG.color.a <= 0.05f)
            {
                _ImageBG.color = Color.clear;
                if (_ImageBG.color == Color.clear)
                {
                    NextScenes();
                }
                StartBG.SetActive(false);
            }
        }
        /// <summary>
        /// 跳转Main场景
        /// </summary>
        private void NextScenes()
        {
            GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.MainScenes;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景

        }
        /// <summary>
        /// 设置场景淡出方法
        /// </summary>
        public void SetSceneToClear()
        {
            _BoolSceneToClear = true;
        }
        private void Update()
        {
            if (_BoolSceneToClear)
                StartBGToClear();
        }
    }
}

