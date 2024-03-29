using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.Variables;
using Lean.Touch;
using UnityEngine;

namespace Dacodelaac.Inputs
{
    public class InputHandler : BaseMono
    {
        [SerializeField] InputHandlerDataVariable inputHandlerDataVariable;

        InputHandlerData data = new InputHandlerData();
        LeanFingerFilter use = new LeanFingerFilter(true);
        List<LeanFinger> fingers;

        public override void Initialize()
        {
            inputHandlerDataVariable.Value = data;
        }

        public override void Tick()
        {
            data.Finger = null;
            if (data.Stopped)
            {
                return;
            }

            fingers = use.GetFingers();
            if (fingers.Count > 0)
            {
                data.Finger = fingers[0];
            }
        }

        public void OnStopInput()
        {
            data.Stopped = true;
        }
    }
}