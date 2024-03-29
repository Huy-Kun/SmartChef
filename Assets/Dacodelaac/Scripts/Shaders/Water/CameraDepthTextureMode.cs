using Dacodelaac.Core;
using UnityEngine;

public class CameraDepthTextureMode : BaseMono 
{
    [SerializeField] DepthTextureMode depthTextureMode;

    void OnValidate()
    {
        SetCameraDepthTextureMode();
    }

    public override void Initialize()
    {
        SetCameraDepthTextureMode();
    }

    void SetCameraDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = depthTextureMode;
    }
}
