using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{

    [SerializeField] Transform StartPenCursorTransf;
    [SerializeField] Transform startTrsanform;
    [SerializeField] Transform tipTransform;
    RaycastHit raycastHit;
    [SerializeField] GameObject penPoint;
#if COLUMN
    [SerializeField] float brushSize = 0.0035f;
#endif
    [SerializeField] GameObject paper;
    LayerMask layerMask;
    [SerializeField] Transform cursorHigh;
    [SerializeField] Transform cursorLow;
    [SerializeField] Transform cursor;
    [SerializeField] UIController uiController;
    Transform raycastTransf;
    Vector3 oldPos;
    dLineManager lineManager;
    bool startDraw;
    PlayerWriteStatu playerWriteStatu;

    // Use this for initialization
    void Start()
    {
#if PLANE
        GetComponent<PinchDraw>().enabled = false;
        lineManager = GetComponent<dLineManager>();
#endif

#if COLUMN
        GetComponent<dLineManager>.enabled = false;
#endif
        layerMask = ~(1 << LayerMask.NameToLayer(MyConst.MIRRORCENTER));

        playerWriteStatu = new PlayerWriteStatu();
    }

    // Update is called once per frame
    void Update()
    {
        //检测纸
        var rayCursor = new Ray(StartPenCursorTransf.position, tipTransform.position - StartPenCursorTransf.position);
        var isPaper = Physics.Raycast(rayCursor, out raycastHit, 1, layerMask);
        if (isPaper)
        {
            raycastTransf = raycastHit.transform;
            if (raycastTransf.name == MyConst.PAPER || raycastTransf.tag == MyConst.HZCOLLIDER || raycastTransf.tag == MyConst.HZSTARTCOLLIDER)
            {
#if PLANE
                penPoint.transform.position = new Vector3(raycastHit.point.x, paper.transform.position.y + 0.001f, raycastHit.point.z);
#endif
#if COLUMN
                penPoint.transform.position = new Vector3(raycastHit.point.x, paper.transform.position.y, raycastHit.point.z);
#endif
            }
        }
        //笔头
        var writeDisPos = tipTransform.position - startTrsanform.position;
        var rayWrite = new Ray(startTrsanform.position, writeDisPos);
        var isCollider = Physics.Raycast(rayWrite, writeDisPos.magnitude, layerMask);
        if (isCollider)
        {
            if (!startDraw && raycastTransf.tag == MyConst.HZSTARTCOLLIDER)
            {
#if PLANE
                if (lineManager.isCanDraw)
                {
                    //按下trigger,并且是开始位置
                    startDraw = true;
                }
#endif
            }

            //开始写字
            if (startDraw)
            {

                if (raycastTransf.name == MyConst.PAPER || raycastTransf.tag == MyConst.HZCOLLIDER || raycastTransf.tag == MyConst.HZSTARTCOLLIDER)
                {

                    var penDepth = Mathf.Clamp((startTrsanform.position - raycastHit.point).magnitude, 0.01f, 1f);

#if COLUMN
                    BrushManager.cursorsize = brushSize / (penDepth * 50);
#endif

#if PLANE
                    if (oldPos != Vector3.zero)
                        cursor.LookAt(oldPos);

                    //平面画
                    cursorHigh.localPosition = new Vector3(0.0025f / penDepth, 0, 0);
                    cursorLow.localPosition = new Vector3(-0.0025f / penDepth, 0, 0);

                    //写在纸的其它地方
                    if (raycastTransf.name == MyConst.PAPER)
                    {
                        if (lineManager.isCanDraw)
                            playerWriteStatu.Grade -= 0.4f;
                    }

                    //记录旧点
                    oldPos = raycastHit.point;
#endif
                }
            }
        }
        else
        {

#if COLUMN
            BrushManager.cursorsize = 0;
#endif
#if PLANE
            //平面画
            cursorHigh.localPosition = Vector3.zero;
            cursorLow.localPosition = Vector3.zero;
#endif
        }
        uiController.ShowGradeText(playerWriteStatu.Grade);
    }
}
