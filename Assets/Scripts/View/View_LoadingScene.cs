/***
*   Title:"旭景清园"项目开发
*   [副标题] 场景异步加载
*   Description：
*   [描述]
*   Date:2019/01/29
*   Version:0.1
*   Developer:lixin
*   ModifyRecoder:
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Global;
namespace View
{
    public class View_LoadingScene : MonoBehaviour
    {
        
        private AsyncOperation _AsyOper;//接受场景
        public Slider _SliderProgress;//定义滑动条
        private float _FloProgressNumber;//定义滑动条的数值

        void Start()
        {
            StartCoroutine("LoadingScenesProgress");
        }


        /// <summary>
        /// 使用协程加载户型展示场景；
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadingScenesProgress() 
        {
            //_AsyOper = Application.LoadLevelAsync("HouseScenes");
            //Application.LoadLevelAsync内容已经过时现在使用SceneManager.LoadSceneAsync
            //需要添加using UnityEngine.SceneManagement;的引用   
            yield return new WaitForEndOfFrame();
            _AsyOper = SceneManager.LoadSceneAsync(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.NextScenesName));
            //_AsyOper = SceneManager.LoadSceneAsync(GlobalParameter.HOUSSESCENES);
            _FloProgressNumber = _AsyOper.progress;
            yield return _AsyOper;

        }
        /// <summary>
        /// 显示进度条 
        /// </summary>
        private void Update()
        {
            if (_FloProgressNumber <= 1)
            {
                _FloProgressNumber += 0.01f;
            }
            _SliderProgress.value = _FloProgressNumber;
        }
    }
}

