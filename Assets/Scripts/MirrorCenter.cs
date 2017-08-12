using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCenter : MonoBehaviour
{

    [SerializeField] Transform steamVRModel;//手柄
    [SerializeField] Transform steamCamera;//头盔
    [SerializeField] Transform scene;
    RaycastHit raycastHit;
    Vector3 centerPos;//镜像中心点
    Vector3 raycastDir;
    Vector3 startPos;

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
        startPos = scene.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(steamVRModel.position, raycastDir);
        bool isCollider = Physics.Raycast(ray, out raycastHit, 1);
        if (isCollider)
        {
            if (raycastHit.transform.name == MyConst.MIRRORCENTER)
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
    /// <summary>
    /// 调整高度
    /// </summary>
    /// <param name="ud"></param>
    public void ChangeSceneUd(float ud)
    {
        scene.position = startPos + scene.up * (ud - 0.5f);
    }
}
