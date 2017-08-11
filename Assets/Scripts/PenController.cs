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
    // Use this for initialization
    void Start()
    {
        pinchDraw = GetComponent<PinchDraw>();
    }

    // Update is called once per frame
    void Update()
    {


        var rayCursor = new Ray(StartPenCursorTransf.position, tipTransform.position - StartPenCursorTransf.position);
        var isPaper = Physics.Raycast(rayCursor, out raycastHit, 1);
        if (isPaper)
        {
            penPoint.transform.position = new Vector3(raycastHit.point.x, paper.transform.position.y, raycastHit.point.z);
        }


        var writeDisPos = tipTransform.position - startTrsanform.position;
        var rayWrite = new Ray(startTrsanform.position, writeDisPos);
        var isCollider = Physics.Raycast(rayWrite, writeDisPos.magnitude);
        if (isCollider)
        {
            if (raycastHit.transform.name == "Paper")
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
