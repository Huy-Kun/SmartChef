using System;
using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.Rendering;

public class SkyBoxManager : BaseMono
{
    [SerializeField] private float skyboxRotationSpeed = 0.5f;

    public override void Tick()
    {
        base.Tick();
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
    }
}
