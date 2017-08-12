using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Button initMirrorCenter;
    [SerializeField] Slider udSlider;
    [SerializeField] MirrorCenter mirrorCenter;


    private void OnEnable()
    {
        initMirrorCenter.onClick.AddListener(OnInitMirrorCenter);
        udSlider.onValueChanged.AddListener(OnChangeSceneUd);
    }

    void OnInitMirrorCenter()
    {
        mirrorCenter.InitMirrorCenter();
    }

    void OnChangeSceneUd(float value)
    {
        mirrorCenter.ChangeSceneUd(value);
    }
}
