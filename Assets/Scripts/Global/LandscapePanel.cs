using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LandscapePanel : MonoBehaviour
    {
        public static LandscapePanel Instance;

        private void Awake()
        {
            Instance = this;
        }
        public void OnLandscape()
        {
            GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.LandScapeScenes;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
        }
    }

}

