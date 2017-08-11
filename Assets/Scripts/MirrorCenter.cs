using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCenter : MonoBehaviour
{

    [SerializeField] Transform steamVRModel;
    [SerializeField] Transform steamCamera;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, steamVRModel.position.y, steamVRModel.position.z);
    }

    //public void InitMirrorCenter()
    //{
    //    transform.position = steamCamera.position;
    //    transform.LookAt(steamCamera);
    //}
}
