using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPen : MonoBehaviour
{

    [SerializeField] Transform mirrirCenter;
    [SerializeField] Transform steamVRModel;

    // Update is called once per frame
    void Update()
    {
        var model2Center = mirrirCenter.position - steamVRModel.position;
        transform.position = new Vector3(model2Center.x, steamVRModel.position.y, steamVRModel.position.z);
        transform.rotation = steamVRModel.parent.rotation;
    }
}
