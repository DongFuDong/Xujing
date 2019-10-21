using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContorl : MonoBehaviour
{
  
    public GameObject goRawImage;//RawImage游戏物体
    private RawImage rawImage;//RawImage组件
    public float colorSpeed = 1;//颜色变化速率


    bool _BoolSceneToClear = true;//屏幕逐渐清晰
    bool _BoolSceneToBlack = false;//屏幕逐渐暗淡

    

    public void Awake()
    {
      
       


    }

    void Start()
    {
        rawImage = goRawImage.GetComponent<RawImage>();//获得游戏物体的RawImage组件
    }


    void Update()
    {

        Invoke("SceneBlack", 3f);
       
       

    }


    private void FadeToBack()//淡出
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, colorSpeed * Time.deltaTime);//

    }


    private void SceneBlack()
    {
        FadeToBack();
        if (rawImage.color.a <= 0.2f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            goRawImage.SetActive(false);
        }
    }


}

