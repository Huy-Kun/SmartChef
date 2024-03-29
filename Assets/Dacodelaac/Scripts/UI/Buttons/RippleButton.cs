using Dacodelaac.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Event = Dacodelaac.Events.Event;

namespace Dacodelaac.UI.Buttons
{
    public class RippleButton : BaseMono, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] Ease ease = Ease.OutQuint;
        [SerializeField] float scale = 0.9f;
        [SerializeField] public bool isInteractable = true;
        [SerializeField] protected Button.ButtonClickedEvent m_OnClick;
        [SerializeField] Event buttonClickEvent;

        public Button.ButtonClickedEvent OnClicked => m_OnClick;

        protected Vector3 originScale = Vector3.one;

        public override void DoEnable()
        {
            base.DoEnable();
            originScale = transform.localScale;
        }

        public override void DoDisable()
        {
            base.DoDisable();
            ResetScale();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
            if(buttonClickEvent) buttonClickEvent.Raise();
            DoScale();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetScale();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
            m_OnClick.Invoke();
        }

        void DoScale()
        {
            DOTween.Kill(this);
            transform.DOScale(originScale * scale, 0.15f).SetEase(ease).SetUpdate(true).SetTarget(this).Play();
        }

        void ResetScale()
        {
            DOTween.Kill(this);
            transform.localScale = originScale;
        }
    }
}