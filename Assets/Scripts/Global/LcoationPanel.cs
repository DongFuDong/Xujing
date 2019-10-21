/***
*   Title:"旭景清园"项目开发
*   使用DoTween插件的一些方法
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
    public class LcoationPanel : MonoBehaviour
    {
        [SerializeField]
        //定义“区位规划”按钮
        private GameObject Lcoation;
        [SerializeField]
        //定义二级交通按钮
        private GameObject TrafficPanelTow;
        //定义缩放的速度
        [SerializeField]
        private float ScaleSpeed = 1;
        //定义区位规划界面里面所有的圆圈
        [SerializeField]
        private Image Circle1;
        [SerializeField]
        private Image Circle2;
        [SerializeField]
        private Image Circle3;
        [SerializeField]
        private Image Circle4;
        [SerializeField]
        private Image Circle5;
        [SerializeField]
        private Image Circle6;
        //定义区位规划界面里面商圈的名字
        [SerializeField]
        private Image BeiText;
        [SerializeField]
        private Image UniversityText;
        [SerializeField]
        private Image CLDText;
        [SerializeField]
        private Image TaihuaText;
        [SerializeField]
        private Image AdministrativeText;
        [SerializeField]
        private Image FCWLText;
        //定义区位规划界面商圈动画的速度
        public float TextSpeed = 1;
        //定义圆圈缩放的速度
        [SerializeField]
        private GameObject TextAndCircle;
        [SerializeField]
        private float CircleScaleSpeed = 1;

        /// <summary>
        /// 定义区位规划地铁的三条线
        /// </summary>
        [SerializeField]
        private GameObject Line;
        [SerializeField]
        private GameObject Line01;
        private Material Line01Mat;
        [SerializeField]
        private GameObject Line02;
        private Material Line02Mat;
        [SerializeField]
        private GameObject Line03;
        private Material Line03Mat;
        [SerializeField]
        private float LineSpeed = 1;
        [SerializeField]
        private Image Signsbei;
        [SerializeField]
        private Image SubwaySigns;

        [SerializeField]
        private GameObject BusSigns;

        //实例化
        public static LcoationPanel Instance;
        private Sequence quence;

        private void Awake()
        {
            Instance = this;
            //获得区为规划地图上地铁的三条线的材质组件
            Line01Mat = Line01.GetComponent<MeshRenderer>().material;
            Line02Mat = Line02.GetComponent<MeshRenderer>().material;
            Line03Mat = Line03.GetComponent<MeshRenderer>().material;


        }
        /// <summary>
        /// 区为规划界面的Y放大
        /// </summary>
        public void LcoationPanelEnlarge()
        {
            Lcoation.SetActive(true);
            Lcoation.transform.DOScaleY(1, ScaleSpeed);

        }
        /// <summary>
        /// 区为规划界面的Y缩小
        /// </summary>
        public void LcoationPanelNarrow()
        {
            Lcoation.transform.DOScaleY(0, ScaleSpeed);
            ResetCircleAndText();
            TrafficPanelTow.SetActive(false);
            Lcoation.SetActive(false);
        }
        /// <summary>
        /// 显示交通按钮
        /// </summary>
        public void TrafficBtn()
        {
            TrafficPanelTow.SetActive(true);
            ResetCircleAndText();
        }

        /// <summary>
        /// 显示周边配套按钮
        /// </summary>
        public void AroundBtn()
        {
            TrafficPanelTow.SetActive(false);
            CircleAndTextAnimation();
            ResetMetroLineAnimation();
            ResetBusLineAnimation();


        }
        /// <summary>
        /// 点击地铁
        /// </summary>
        public void MetroBtn()
        {

            MetroLineAnimation();
            ResetBusLineAnimation();


        }
        /// <summary>
        /// 点击公交
        /// </summary>
        public void BusBtn()
        {
            BusLineAnimation();
            ResetMetroLineAnimation();

        }
        /// <summary>
        /// 区为规划界面的圈和字动画
        /// </summary>
        private void CircleAndTextAnimation()
        {

            TextAndCircle.SetActive(true);
            Circle1.transform.DOScale(1, CircleScaleSpeed);
            BeiText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed);
            Circle2.transform.DOScale(1, CircleScaleSpeed).SetDelay(0.5f);
            UniversityText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed + 0.5f);
            Circle3.transform.DOScale(1, CircleScaleSpeed).SetDelay(1f);
            CLDText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed + 1f);
            Circle4.transform.DOScale(1, CircleScaleSpeed).SetDelay(1.5f);
            TaihuaText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed + 1.5f);
            Circle5.transform.DOScale(1, CircleScaleSpeed).SetDelay(2f);
            AdministrativeText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed + 2f);
            Circle6.transform.DOScale(1, CircleScaleSpeed).SetDelay(2.5f);
            FCWLText.DOFade(1, TextSpeed).SetDelay(CircleScaleSpeed + 2.5f);
        }
        /// <summary>
        /// 重置区为规划界面周边配套的圈和字
        /// </summary>
        private void ResetCircleAndText()
        {
            TextAndCircle.SetActive(false);
            Circle1.transform.DOKill();
            Circle1.rectTransform.localScale = new Vector3(0, 0, 0);
            Circle2.transform.DOKill();
            Circle2.rectTransform.localScale = new Vector3(0, 0, 0);
            Circle3.transform.DOKill();
            Circle3.rectTransform.localScale = new Vector3(0, 0, 0);
            Circle4.transform.DOKill();
            Circle4.rectTransform.localScale = new Vector3(0, 0, 0);
            Circle5.transform.DOKill();
            Circle5.rectTransform.localScale = new Vector3(0, 0, 0);
            Circle6.transform.DOKill();
            Circle6.rectTransform.localScale = new Vector3(0, 0, 0);
            BeiText.DOKill();
            BeiText.color = new Color(1, 1, 1, 0);
            UniversityText.DOKill();
            UniversityText.color = new Color(1, 1, 1, 0);
            CLDText.DOKill();
            CLDText.color = new Color(1, 1, 1, 0);
            TaihuaText.DOKill();
            TaihuaText.color = new Color(1, 1, 1, 0);
            AdministrativeText.DOKill();
            AdministrativeText.color = new Color(1, 1, 1, 0);
            FCWLText.DOKill();
            FCWLText.color = new Color(1, 1, 1, 0);
        }


        /// <summary>
        /// 区位规划交通环境的二级地铁的方法
        /// </summary>
        private void MetroLineAnimation()
        {
            Line.SetActive(true);
            Line01Mat.DOOffset(new Vector2(0.5f, 0.5f), LineSpeed);
            Line02Mat.DOOffset(new Vector2(0.5f, 0.5f), LineSpeed).SetDelay(LineSpeed);
            Line03Mat.DOOffset(new Vector2(0f, 0f), LineSpeed).SetDelay(LineSpeed * 2);
            Signsbei.DOFade(1, LineSpeed).SetDelay(LineSpeed);
            SubwaySigns.DOFade(1, LineSpeed).SetDelay(LineSpeed * 2);
        }
        /// <summary>
        /// 重置区位规划交通环境的二级地铁的方法
        /// </summary>
        private void ResetMetroLineAnimation()
        {
            Line.SetActive(false);
            Line01Mat.DOKill();
            Line02Mat.DOKill();
            Line03Mat.DOKill();
            Line01Mat.mainTextureOffset = new Vector2(0, 0);
            Line02Mat.mainTextureOffset = new Vector2(0, 0);
            Line03Mat.mainTextureOffset = new Vector2(-1, 0);
            Signsbei.DOKill();
            SubwaySigns.DOKill();
            Signsbei.color = new Color(1, 1, 1, 0);
            SubwaySigns.color = new Color(1, 1, 1, 0);
        }

        /// <summary>
        /// 公交线路动画方法
        /// </summary>
        private void BusLineAnimation()
        {
            BusSigns.SetActive(true);
            quence = DOTween.Sequence();
            quence.Append(BusSigns.transform.DOLocalMove(new Vector3(-60, -4, -2), 1f));
            quence.AppendInterval(0.5f);
            quence.Append(BusSigns.transform.DOLocalMove(new Vector3(-60, 398, 0), 1f));
            quence.AppendInterval(0.5f);
            quence.Append(BusSigns.transform.DOLocalMove(new Vector3(5, 613, 0), 0.5f));
        }
        /// <summary>
        /// 公交线路重置方法 (这里在点击切换公交线路和地铁的时候有个Bug...Todo)
        /// </summary>
        private void ResetBusLineAnimation()
        {
            BusSigns.SetActive(false);
            quence.Kill();
            BusSigns.transform.localPosition = new Vector3(-60, -613, -2);
        }

        public void ReturnBtn()
        {
            LcoationPanelNarrow();
            ResetMetroLineAnimation();
            ResetBusLineAnimation();
        }
    }
}

