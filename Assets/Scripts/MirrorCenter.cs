using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCenter : MonoBehaviour
{

    [SerializeField] Transform steamVRModel;//手柄
    [SerializeField] Transform steamCamera;//头盔
    RaycastHit raycastHit;
    Vector3 centerPos;//镜像中心点
    Vector3 raycastDir;

    public Vector3 CenterPos
    {
        get
        {
            return centerPos;
        }
    }

    private void Start()
    {
        raycastDir = -steamCamera.right;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(steamVRModel.position, raycastDir);
        bool isCollider = Physics.Raycast(ray, out raycastHit, 1);
        if (isCollider)
        {
            if (raycastHit.transform.name == "MirrorCenter")
                centerPos = raycastHit.point;
        }
        else
        {
            raycastDir = -raycastDir;
        }
    }
    /// <summary>
    /// 初始化位置
    /// </summary>
    public void InitMirrorCenter()
    {
        transform.position = steamCamera.position;
        transform.rotation = steamCamera.rotation;
        raycastDir = -steamCamera.right;
    }
}
