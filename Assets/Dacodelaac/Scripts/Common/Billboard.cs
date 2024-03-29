using Dacodelaac.Core;
using Dacodelaac.Variables;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class Billboard : Core.BaseMono
    {
        [SerializeField] CameraVariable mainCamera;

        public override void LateTick()
        {
            base.LateTick();
            transform.rotation =
                Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(transform.parent.forward, mainCamera.Value.transform.forward),
                    -mainCamera.Value.transform.forward);
        }
    }
}