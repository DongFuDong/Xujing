using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LandScapeCtrl : MonoBehaviour
    {
        [SerializeField]
        private GameObject _timeline;
        [SerializeField]
        private PlayableDirector _timelinePlay;
        //public GameObject _rigidBodyFPSController;
        // public RigidbodyFirstPersonController _myCameraRig;//pc交互使用后
        [SerializeField]
        private GameObject _easyToch;
        [SerializeField]
        private GameObject _fade;
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private GameObject _miniMap;
        private AudioSource _landScapeAudio;

        private void Start()
        {

            _landScapeAudio = gameObject.GetComponent<AudioSource>();
        }
        /// <summary>
        /// 点击自动漫游
        /// </summary>
        public void OnClikFreeRoamingBtn()
        {
            //Cursor.visible = false;
            _timeline.SetActive(true);
            _timelinePlay.Play();
            _fade.SetActive(true);
            _easyToch.SetActive(false);
            _player.SetActive(false);
            _miniMap.SetActive(false);
            _landScapeAudio.Play();
        }
        /// <summary>
        /// 点击手动漫游
        /// </summary>
        public void OnClickFixdRoamingBtn()
        {
            _timeline.SetActive(false);
            _timelinePlay.Stop();

            //_myCameraRig.enabled = true;//pc交互使用后
            //_rigidBodyFPSController.SetActive(true);//pc交互使用后
            _easyToch.SetActive(true);
            _fade.SetActive(false);

            _player.SetActive(true);
            _miniMap.SetActive(true);
            _landScapeAudio.Stop();

        }

        public void OnClickReturn()
        {
            GlobalParameter.NextScenesName = GlobalParameter.ScenesEnum.MainScenes;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParameter.ScenesEnum.LoadingScenes));//加载场景
        }
        #region PC端自动漫游和手动漫游的切换
        //private void Update()
        //{
        //    //按下ESC禁用第一人称摄像机
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        _myCameraRig.enabled = false;
        //        _rigidBodyFPSController.SetActive(false);
        //        //显示鼠标
        //        Cursor.visible = true;
        //    }
        //}
        #endregion
    }
}

 