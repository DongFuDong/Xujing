/***
*   Title:"旭景清园"项目开发
*   [副标题] 全景鸟瞰功能开发
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

namespace Global
{
    public class PanoramaPanel : MonoBehaviour
    {
        public static PanoramaPanel Instance;
        [SerializeField]
        private Toggle Panorama;
        
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 全景鸟瞰
        /// </summary>
        public void PanoramaSigns()
        {
            //Panorama.isOn = true;
            
            GamerContorl.Instance.PanoramaCamaera();
            
        }
   
    }
}

