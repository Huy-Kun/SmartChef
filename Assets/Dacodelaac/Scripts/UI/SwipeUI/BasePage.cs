using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.UI.SwipeUI
{
    public class BasePage : BaseMono
    {
        public Vector2 Position { get; set; }

        public virtual void OnBeginShow()
        {
            
        }

        public virtual void OnBeginDismiss()
        {
            
        }
    }
}