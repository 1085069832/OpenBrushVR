using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{

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
        var disPos = tipTransform.position - startTrsanform.position;
        Ray ray = new Ray(startTrsanform.position, disPos);

        bool isPaper = Physics.Raycast(ray, out raycastHit, 1);
        if (isPaper)
        {
            penPoint.transform.position = new Vector3(raycastHit.point.x, paper.transform.position.y, raycastHit.point.z);
        }

        bool isCollider = Physics.Raycast(ray, disPos.magnitude);
        if (isCollider)
        {
            if (raycastHit.transform.name == "Paper")
            {
                BrushManager.cursorsize = brushSize;
            }
        }
        else
        {
            BrushManager.cursorsize = 0;
        }
    }
}
