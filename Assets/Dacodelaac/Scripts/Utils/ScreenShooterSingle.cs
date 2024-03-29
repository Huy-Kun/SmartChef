using System.IO;
using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.Utils
{
    public class ScreenShooterSingle : BaseMono
    {
        [SerializeField] Resolution resolution;
        [SerializeField] KeyCode keyCode;
        
#if UNITY_EDITOR
        public override void Tick()
        {
            if (Input.GetKeyDown(keyCode))
            {
                Shoot();
            }
        }
        
        [ContextMenu("Shoot")]
        public void Shoot()
        {
            if (!Directory.Exists(Application.dataPath + $"/../ScreenShots"))
            {
                Directory.CreateDirectory(Application.dataPath + $"/../ScreenShots");
            }
            TextureUtils.CaptureToFile(GetComponent<Camera>(), Application.dataPath + $"/../ScreenShots/{TimeUtils.CurrentTicks}", resolution.width, resolution.height, refresh: false);
        }
#endif
    }
}