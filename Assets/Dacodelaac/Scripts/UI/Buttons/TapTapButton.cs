using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Dacodelaac.UI.Buttons
{
    public class TapTapButton : Core.BaseMono, IPointerClickHandler
    {
        [SerializeField] int tapCount = 5;
        [SerializeField] UnityEvent onTap;
        
        float lastTimeClick;
        int count = 0;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Time.time - lastTimeClick > 0.5f)
            {
                count = 1;
            }
            else
            {
                count++;
                if (count >= tapCount)
                {
                    count = 0;
                    onTap.Invoke();
                }
            }

            lastTimeClick = Time.time;
        }
    }
}