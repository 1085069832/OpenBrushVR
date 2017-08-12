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
    PinchDraw pinchDraw;
    [SerializeField] float brushSize = 0.0035f;
    [SerializeField] GameObject paper;
    LayerMask layerMask;
    // Use this for initialization
    void Start()
    {
        pinchDraw = GetComponent<PinchDraw>();
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
                penPoint.transform.position = new Vector3(raycastHit.point.x, paper.transform.position.y, raycastHit.point.z);
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
                var penDepth = Mathf.Clamp((startTrsanform.position - raycastHit.point).magnitude, 0.01f, 0.05f);
                BrushManager.cursorsize = brushSize / (penDepth * 50);
            }
        }
        else
        {
            BrushManager.cursorsize = 0;
        }
    }
}
