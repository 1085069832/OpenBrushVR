using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPen : MonoBehaviour
{

    [SerializeField] MirrorCenter mirrirCenter;
    [SerializeField] Transform steamVRModel;

    // Update is called once per frame
    void Update()
    {
        var model2Center = mirrirCenter.CenterPos - steamVRModel.position;
        transform.position = mirrirCenter.CenterPos + model2Center;
        transform.rotation = steamVRModel.parent.rotation;
    }
}
