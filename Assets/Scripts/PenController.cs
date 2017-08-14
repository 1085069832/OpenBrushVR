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
    Vector3 oldPos;

    // Use this for initialization
    void Start()
    {
#if PLANE
        GetComponent<PinchDraw>().enabled = false;
#endif

#if COLUMN
        GetComponent<dLineManager>.enabled = false;
#endif
        layerMask = ~(1 << LayerMask.NameToLayer(MyConst.MIRRORCENTER));
    }

    // Update is called once per frame
    void Update()
    {
        //检测纸
        var rayCursor = new Ray(StartPenCursorTransf.position, tipTransform.position - StartPenCursorTransf.position);
        var isPaper = Physics.Raycast(rayCursor, out raycastHit, 1, layerMask);
        if (isPaper)
        {
            if (raycastHit.transform.name == MyConst.PAPER)
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
            if (raycastHit.transform.name == MyConst.PAPER)
            {
                var penDepth = Mathf.Clamp((startTrsanform.position - raycastHit.point).magnitude, 0.01f, 1f);
#if COLUMN
                BrushManager.cursorsize = brushSize / (penDepth * 50);
#endif
#if PLANE
                if (oldPos != Vector3.zero)
                    cursor.LookAt(oldPos);
                //平面画
                cursorHigh.localPosition = new Vector3(0.0015f / penDepth, 0, 0);
                cursorLow.localPosition = new Vector3(-0.0015f / penDepth, 0, 0);

                //记录旧点
                oldPos = raycastHit.point;
#endif
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
    }
}
