using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class ExitBtnToggle : MonoBehaviour
    {
        public static ExitBtnToggle Instance;
        [SerializeField]
        private Toggle _exitBtntoggle;
        [SerializeField]
        private GameObject _exitBtnTowMenu;



        private void Awake()
        {
            Instance = this;
        }
        public void ExitBtn()
        {

            if (_exitBtntoggle.isOn == true)
            {
                _exitBtnTowMenu.SetActive(true);
            }
            else
            {
                _exitBtnTowMenu.SetActive(false);

            }
        }
        public void FixBtn()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
        public void CancelBtn()
        {
            _exitBtnTowMenu.SetActive(false);
        }
    }
}

