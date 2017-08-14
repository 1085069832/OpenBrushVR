using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HzController : MonoBehaviour
{
    public static HzController _instance;
    [SerializeField] Transform paper;
    [SerializeField] GameObject[] hzColliders;

    Material mat;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        mat = paper.GetComponent<MeshRenderer>().material;
        CheckCharacter(mat);
    }

    /// <summary>
    /// 检查字
    /// </summary>
    /// <param name="mat"></param>
    void CheckCharacter(Material mat)
    {
        switch (mat.name)
        {
            case MyConst.BU:
                ChooseCollider(MyConst.BUCOLLIDER);
                break;
            case MyConst.GONG:
                ChooseCollider(MyConst.GONGCOLLIDER);
                break;
            case MyConst.MU:
                ChooseCollider(MyConst.MUCOLLIDER);
                break;
        }
    }

    /// <summary>
    /// 获取当前字的collider
    /// </summary>
    /// <param name="colliderName"></param>
    void ChooseCollider(string colliderName)
    {
        for (int i = 0; i < hzColliders.Length; i++)
        {
            if (hzColliders[i].transform.name == colliderName)
            {
                hzColliders[i].SetActive(true);
            }
            else
            {
                hzColliders[i].SetActive(false);
            }
        }
    }
}
