using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Button initMirrorCenter;
    [SerializeField] Slider udSlider;
    [SerializeField] MirrorCenter mirrorCenter;
    [SerializeField] Text gradeText;

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

    public void ShowGradeText(float grade)
    {
        gradeText.text = grade + "";
    }

}
