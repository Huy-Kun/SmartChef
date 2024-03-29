using Dacodelaac.Core;
using DG.Tweening;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class PulseAnimation : BaseMono
    {
        [SerializeField] float scale = 1.2f;
        [SerializeField] float duration = 0.2f;
        [SerializeField] Ease ease = Ease.Linear;
        [SerializeField] int loopCount = -1;
        [SerializeField] bool from;

        Vector3 originalScale;

        public override void DoEnable()
        {
            base.DoEnable();
            originalScale = transform.localScale;
            DOTween.Kill(this);
            var tween = transform.DOScale(scale * originalScale, duration);
            if (from)
            {
                tween = tween.From();
            }
            if (loopCount != 0)
            {
                tween = tween.SetLoops(loopCount, LoopType.Yoyo);   
            }
            tween.SetEase(ease).SetTarget(this);
            tween.Play();
        }

        public override void DoDisable()
        {
            base.DoDisable();
            DOTween.Kill(this);
            transform.localScale = originalScale;
        }
    }
}