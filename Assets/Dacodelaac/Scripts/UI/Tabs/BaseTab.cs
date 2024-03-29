using Dacodelaac.Core;
using Dacodelaac.UI.Popups;
using UnityEngine;

namespace Dacodelaac.UI.Tabs
{
    public class BaseTab : BaseMono
    {
        public BaseTabController Controller { get; set; }
        public int Index { get; set; }

        public virtual void Setup()
        {

        }

        public virtual void OnBeginShow()
        {

        }

        public virtual void OnShown()
        {

        }

        public virtual void OnResume()
        {

        }

        public virtual void OnBeginDismiss()
        {

        }

        public virtual void OnDismissed()
        {

        }
    }
}
