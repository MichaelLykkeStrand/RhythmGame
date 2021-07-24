﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineEffects : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float punchEffectScale = -0.5f;
    [SerializeField] private float punchEffectTime = 0.1f;
    private float defaultOrthographicSize;
    // Start is called before the first frame update
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        defaultOrthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Bob();
        }
    }


    public void Bob()
    {
        float newSize = defaultOrthographicSize + punchEffectScale;
        float orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        Tween t = DOTween.To(() => orthographicSize, x => orthographicSize = x, newSize, punchEffectTime);
        t.OnUpdate(() =>
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
        });
        

        t.OnComplete(() => {
            Tween k = DOTween.To(() => orthographicSize, x => orthographicSize = x, defaultOrthographicSize, punchEffectTime);
            k.OnUpdate(() => {
                cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
            });
        });
    }
}
