using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.Utils
{
    public class CameraExtra : BaseMono
    {
        [SerializeField] private bool keepMinHorizontalFov = true;
        [SerializeField] private Vector2 minReferenceAspect = new(9, 16);
        [SerializeField] private bool keepMaxHorizontalFov = true;
        [SerializeField] private Vector2 maxReferenceAspect = new(3, 4);

        private float _originalFov;
        private float _originalOrthoSize;
        private Camera _cam;
        private bool _setup;

        public override void Initialize()
        {
            _cam = GetComponent<Camera>();
            Setup(_cam);
            Apply();
        }

        private void Setup(Camera c)
        {
            if (_setup) return;
            _setup = true;

            _cam = c;
            _originalFov = _cam.fieldOfView;
            _originalOrthoSize = _cam.orthographicSize;
            // originalOrthoSize = cam.orthographicSize / currentCameraConfig.Value.sizeMul;
        }

        private void Apply()
        {
            if (keepMinHorizontalFov)
            {
                KeepMinHorizontalFov();
            }

            if (keepMaxHorizontalFov)
            {
                KeepMaxHorizontalFov();
            }
        }

        private void KeepMinHorizontalFov()
        {
            KeepHorizontalFov(minReferenceAspect.x / minReferenceAspect.y, true);
        }

        private void KeepMaxHorizontalFov()
        {
            KeepHorizontalFov(maxReferenceAspect.x / maxReferenceAspect.y, false);
        }

        private void KeepHorizontalFov(float refAspect, bool min)
        {
            if ((min && _cam.aspect < refAspect) || (!min && _cam.aspect > refAspect))
            {
                if (_cam.orthographic)
                {
                    _cam.orthographicSize = _originalOrthoSize * refAspect / _cam.aspect;
                }
                else
                {
                    var hfov = VFov2HFov(_originalFov, refAspect);
                    _cam.fieldOfView = HFov2VFov(hfov, _cam.aspect);
                }
            }
        }

        private static float VFov2HFov(float vfov, float camAspect)
        {
            vfov *= Mathf.Deg2Rad;
            var camHalfH = Mathf.Tan(vfov / 2);
            var camHalfW = camHalfH * camAspect;

            return 2 * Mathf.Atan(camHalfW) * Mathf.Rad2Deg;
        }

        private static float HFov2VFov(float hfov, float camAspect)
        {
            hfov *= Mathf.Deg2Rad;
            var camHalfW = Mathf.Tan(hfov / 2);
            var camHalfH = camHalfW / camAspect;

            return 2 * Mathf.Atan(camHalfH) * Mathf.Rad2Deg;
        }
    }
}