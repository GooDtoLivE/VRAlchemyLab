﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteAlways]
[RequireComponent(typeof(VisualEffect))]
public class LiquidPour : MonoBehaviour
{
    [Range(0.01f, 7)]
    public float bottleNeckDiameter = 2;
    public Color color = Color.white;

    VisualEffect vfx;
    float flow;
    float flowLimit;

    float verticality;
    Vector3 upVector;
    bool playVFX;

    private void Init()
    {
        vfx = GetComponent<VisualEffect>();
        flowLimit = vfx.GetFloat("StripFlowLimit");
    }

    private void Verif()
    {
        if(vfx == null)
            vfx = GetComponent<VisualEffect>();

        flowLimit = vfx.GetFloat("StripFlowLimit");
    }

    private void UpdateVFX()
    {
        upVector = transform.up;
        verticality = Vector3.Dot(upVector, new Vector3(0, -1, 0));
        flow = Mathf.Clamp01(verticality);
    }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        Verif();
    }

    void Update()
    {
        Verif();
        UpdateVFX();

        if ((!playVFX) && (flow <= flowLimit))
        {
            playVFX = true;
        }

        if ((playVFX) && (flow > flowLimit))
        {
            Debug.Log("test");
            vfx.Play();
            playVFX = false;
        }
        

        vfx.SetFloat("Flow", flow);
        vfx.SetFloat("BottleDiameter(cm)", bottleNeckDiameter);
        vfx.SetVector4("Color", color);

    }
}