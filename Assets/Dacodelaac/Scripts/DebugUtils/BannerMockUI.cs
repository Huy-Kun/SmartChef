using Dacodelaac.Common;
using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.DebugUtils
{
    public class BannerMockUI : BaseMono
    {
        [SerializeField] RandomImage image;

        public override void Initialize()
        {
            base.Initialize();
            image.gameObject.SetActive(false);
        }

        public void OnShow()
        {
            image.gameObject.SetActive(true);
        }

        public void OnHide()
        {
            image.gameObject.SetActive(false);
        }
    }
}