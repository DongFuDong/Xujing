﻿/***
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



public class IOClod : MonoBehaviour
{
    public float Lod1;
    public float Lod2;
    public float LodMargin;
    public bool LodOnly;

    private Vector3 hitPoint;
    private float hitDistance;
    private float lod_1;
    private float lod_2;
    private float lodMargin;
    private bool realtimeShadows;
    private IOCcam iocCam;
    private int counter;
    private Renderer[] rs0;
    private Renderer[] rs1;
    private Renderer[] rs2;
    private Renderer[] rs;
    private bool hidden;
    private int currentLod;
    private float prevDist;
    private float distOffset;
    private int lods;
    private float dt;
    private float lodDistanceFromCam;
    private float hitTimeOffset;
    private float prevHitTime;
    private bool sleeping;

    private Shader shInvisible;
    private Shader[] sh;
    private Shader[] sh0;
    private float distanceFromCam;
    private float shadowDistance;
    private int frameInterval;

    private RaycastHit h;
    private Ray r;
    private bool visible;

    public Camera _camera;

    void Awake()
    {
        shadowDistance = QualitySettings.shadowDistance * 2f;
        //for (int i = 0; i < _camera.Length; i++)
        //{
            iocCam = _camera.GetComponent<IOCcam>();
        //}

        if (iocCam == null)
        {
            this.enabled = false;
        }
        else
        {
            prevDist = 0f;
            prevHitTime = Time.time;
            sleeping = true;
            h = new RaycastHit();
        }
    }

    void Start()
    {
        UpdateValues();
        if (transform.Find("Lod_0"))
        {
            lods = 1;
            rs0 = transform.Find("Lod_0").GetComponentsInChildren<Renderer>(false);
            sh0 = new Shader[rs0.Length];
            for (int i = 0; i < rs0.Length; i++)
            {
                sh0[i] = rs0[i].material.shader;
            }
            if (transform.Find("Lod_1"))
            {
                lods++;
                rs1 = transform.Find("Lod_1").GetComponentsInChildren<Renderer>(false);

                if (transform.Find("Lod_2"))
                {
                    lods++;
                    rs2 = transform.Find("Lod_2").GetComponentsInChildren<Renderer>(false);
                }
            }
        }
        else
        {
            lods = 0;
        }
        rs = GetComponentsInChildren<Renderer>(false);
        sh = new Shader[rs.Length];
        for (int i = 0; i < rs.Length; i++)
        {
            sh[i] = rs[i].material.shader;
        }
        shInvisible = Shader.Find("Custom/Invisible");
        Initialize();
    }
    public void Initialize()
    {
        if (iocCam.enabled == true)
        {
            HideAll();
        }
        else
        {
            this.enabled = false;
            ShowLod(1);
        }
    }
    void Update()
    {
        frameInterval = Time.frameCount % 10;
        if (frameInterval == 0)
        {
            switch (LodOnly)
            {
                case false:
                    if (!hidden && Time.frameCount - counter > iocCam.hideDelay)
                    {
                        switch (currentLod)
                        {
                            case 0:
                                visible = rs0[0].isVisible;
                                break;
                            case 1:
                                visible = rs1[0].isVisible;
                                break;
                            case 2:
                                visible = rs2[0].isVisible;
                                break;
                            default:
                                visible = rs[0].isVisible;
                                break;
                        }
                        if (visible && hitDistance > 100f)
                        {
                            r = new Ray(hitPoint, iocCam.transform.position - hitPoint);
                            if (Physics.Raycast(r, out h, iocCam.viewDistance))
                            {
                                if (h.transform.tag != iocCam.tag)
                                {
                                    Hide();
                                }
                                else
                                {
                                    counter = Time.frameCount;
                                }
                            }
                        }
                        else
                        {
                            Hide();
                        }
                    }
                    break;
                case true:
                    if (!sleeping && Time.frameCount - counter > iocCam.hideDelay)
                    {
                        ShowLod(3000f);
                        sleeping = true;
                    }
                    break;
            }
        }
        else if (realtimeShadows && frameInterval == 5)
        {
            distanceFromCam = Vector3.Distance(transform.position, iocCam.transform.position);
            if (hidden)
            {
                switch (lods)
                {
                    case 0:
                        if (distanceFromCam > shadowDistance)
                        {
                            if (rs[0].enabled)
                            {
                                for (int i = 0; i < rs.Length; i++)
                                {
                                    rs[i].enabled = false;
                                    rs[i].material.shader = sh[i];
                                }
                            }
                        }
                        else
                        {
                            if (!rs[0].enabled)
                            {
                                for (int i = 0; i < rs.Length; i++)
                                {
                                    rs[i].material.shader = shInvisible;
                                    rs[i].enabled = true;
                                }
                            }
                        }
                        break;
                    default:
                        if (distanceFromCam > shadowDistance)
                        {
                            if (rs0[0].enabled)
                            {
                                for (int i = 0; i < rs0.Length; i++)
                                {
                                    rs0[i].enabled = false;
                                    rs0[i].material.shader = sh0[i];
                                }
                            }
                        }
                        else
                        {
                            if (!rs0[0].enabled)
                            {
                                for (int i = 0; i < rs0.Length; i++)
                                {
                                    rs0[i].material.shader = shInvisible;
                                    rs0[i].enabled = true;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }

    public void UpdateValues()
    {
        if (Lod1 != 0)
        {
            lod_1 = Lod1;
        }
        else lod_1 = iocCam.lod1Distance;
        if (Lod2 != 0)
        {
            lod_2 = Lod2;
        }
        else lod_2 = iocCam.lod2Distance;
        if (LodMargin != 0)
        {
            lodMargin = LodMargin;
        }
        else lodMargin = iocCam.lodMargin;
        realtimeShadows = iocCam.realtimeShadows;
    }

    public void UnHide(RaycastHit h)
    {
        counter = Time.frameCount;
        hitPoint = h.point;
        hitDistance = h.distance;
        if (hidden)
        {
            hidden = false;
            ShowLod(h.distance);
        }
        else
        {
            if (lods > 0)
            {
                distOffset = prevDist - h.distance;
                hitTimeOffset = Time.time - prevHitTime;
                prevHitTime = Time.time;
                if (Mathf.Abs(distOffset) > lodMargin | hitTimeOffset > 1f)
                {
                    ShowLod(h.distance);
                    prevDist = h.distance;
                    sleeping = false;
                }
            }
        }
    }

    public void ShowLod(float d)
    {
        int i = 0;
        switch (lods)
        {
            case 0:
                currentLod = -1;
                break;
            case 2:
                if (d < lod_1)
                {
                    currentLod = 0;
                }
                else
                {
                    currentLod = 1;
                }
                break;
            case 3:
                if (d < lod_1)
                {
                    currentLod = 0;
                }
                else if (d > lod_1 & d < lod_2)
                {
                    currentLod = 1;
                }
                else
                {
                    currentLod = 2;
                }
                break;
        }
        switch (currentLod)
        {
            case 0:
                if (!LodOnly && rs0[0].enabled)
                {
                    for (i = 0; i < rs0.Length; i++)
                    {
                        rs0[i].material.shader = sh0[i];
                    }
                }
                else
                {
                    for (i = 0; i < rs0.Length; i++)
                    {
                        rs0[i].enabled = true;
                    }
                }
                for (i = 0; i < rs1.Length; i++)
                {
                    rs1[i].enabled = false;
                }
                if (lods == 3)
                {
                    for (i = 0; i < rs2.Length; i++)
                    {
                        rs2[i].enabled = false;
                    }
                }
                break;
            case 1:
                for (i = 0; i < rs1.Length; i++)
                {
                    rs1[i].enabled = true;
                }
                for (i = 0; i < rs0.Length; i++)
                {
                    rs0[i].enabled = false;
                    if (!LodOnly && realtimeShadows)
                    {
                        rs0[i].material.shader = sh0[i];
                    }
                }
                if (lods == 3)
                {
                    for (i = 0; i < rs2.Length; i++)
                    {
                        rs2[i].enabled = false;
                    }
                }
                break;
            case 2:
                for (i = 0; i < rs2.Length; i++)
                {
                    rs2[i].enabled = true;
                }
                for (i = 0; i < rs0.Length; i++)
                {
                    rs0[i].enabled = false;
                    if (!LodOnly && realtimeShadows)
                    {
                        rs0[i].material.shader = sh0[i];
                    }
                }
                for (i = 0; i < rs1.Length; i++)
                {
                    rs1[i].enabled = false;
                }
                break;
            default:
                if (!LodOnly && rs[0].enabled)
                {
                    for (i = 0; i < rs.Length; i++)
                    {
                        rs[i].material.shader = sh[i];
                    }
                }
                else
                {
                    for (i = 0; i < rs.Length; i++)
                    {
                        rs[i].enabled = true;
                    }
                }
                break;
        }
    }
    public void Hide()
    {
        int i = 0;
        hidden = true;
        switch (currentLod)
        {
            case 0:
                if (realtimeShadows && distanceFromCam <= shadowDistance)
                {
                    for (i = 0; i < rs0.Length; i++)
                    {
                        rs0[i].material.shader = shInvisible;
                    }
                }
                else
                {
                    for (i = 0; i < rs0.Length; i++)
                    {
                        rs0[i].enabled = false;
                    }
                }
                break;
            case 1:
                for (i = 0; i < rs1.Length; i++)
                {
                    rs1[i].enabled = false;
                }
                break;
            case 2:
                for (i = 0; i < rs2.Length; i++)
                {
                    rs2[i].enabled = false;
                }
                break;
            default:
                if (realtimeShadows && distanceFromCam <= shadowDistance)
                {
                    for (i = 0; i < rs.Length; i++)
                    {
                        rs[i].material.shader = shInvisible;
                    }
                }
                else
                {
                    for (i = 0; i < rs.Length; i++)
                    {
                        rs[i].enabled = false;
                    }
                }
                break;
        }
    }
    public void HideAll()
    {
        int i = 0;
        switch (LodOnly)
        {
            case false:
                hidden = true;
                if (lods == 0 && rs != null)
                {
                    for (i = 0; i < rs.Length; i++)
                    {
                        rs[i].enabled = false;
                        if (realtimeShadows)
                        {
                            rs[i].material.shader = sh[i];
                        }
                    }
                }
                else
                {
                    for (i = 0; i < rs0.Length; i++)
                    {
                        rs0[i].enabled = false;
                        if (realtimeShadows)
                        {
                            rs0[i].material.shader = sh0[i];
                        }
                    }
                    for (i = 0; i < rs1.Length; i++)
                    {
                        rs1[i].enabled = false;
                    }
                    for (i = 0; i < rs2.Length; i++)
                    {
                        rs2[i].enabled = false;
                    }
                }
                break;
            case true:
                prevHitTime = prevHitTime - 3f;
                ShowLod(3000f);
                break;
        }
    }
}