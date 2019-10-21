/***
*   Title:"旭景清园"项目开发
*   [副标题]
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
    public class HouseGameContorl : MonoBehaviour
    {

        public static HouseGameContorl Instance;

        public KGFOrbitCam HouseCamera;
        public KGFOrbitCamSettings itsEventSwitchToHouseCameraStart;
        public enum eCameraRoot
        {
            HouseCamera,
            HouseCameraStart
        }

        private void Awake()
        {
            Instance = this;
        }

        private eCameraRoot itsCurrentCameraRoot = eCameraRoot.HouseCamera;
        void Start()
        {
            itsEventSwitchToHouseCameraStart.Apply();
            itsCurrentCameraRoot = eCameraRoot.HouseCamera;
        }
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                HouseCamera.EnableInput(true);

            }
            else
            {
                HouseCamera.EnableInput(false);

            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                HouseCamera.SetTargetZoom(HouseCamera.GetCurrentZoom() - Input.GetAxis("Mouse ScrollWheel") * HouseCamera.GetZoomAxisSensitivity() * HouseCamera.GetZoomSpeed());
            }
        }
    }
}

