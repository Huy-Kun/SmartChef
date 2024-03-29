using Dacodelaac.Core;
using Dacodelaac.Variables;
using SurvivorRoguelike;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class CameraVariableSetter : BaseMono
    {
        [SerializeField] CameraVariable variable;
        [SerializeField] new Camera camera;

        public override void Initialize()
        {
            base.Initialize();
            //camera.orthographicSize /= cameraConfigVariable.Value.sizeMul;
            variable.Value = camera;
        }
    }
}