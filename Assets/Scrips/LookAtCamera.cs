using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class LookAtCamera : BaseMono
{
    private enum Mode
    {
        LookAt,
        LookAtInvert,
        CameraForward,
        CameraForwardInvert
    }

    [SerializeField] private Mode mode;

    public override void LateTick()
    {
        base.LateTick();
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInvert:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
