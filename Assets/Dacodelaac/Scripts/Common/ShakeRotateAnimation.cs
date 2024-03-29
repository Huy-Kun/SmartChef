using Dacodelaac.Core;
using DG.Tweening;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class ShakeRotateAnimation : BaseMono
    {
        [SerializeField] float angle = 30f;
        [SerializeField] float duration = 0.1f;
        [SerializeField] float interval = 0.5f;
        [SerializeField] Ease ease = Ease.Linear;

        Quaternion originalRotation;

        public override void DoEnable()
        {
            base.DoEnable();
            originalRotation = transform.localRotation;
            DOTween.Kill(this);

            DOTween.Sequence()
                .Append(transform.DORotateQuaternion(originalRotation * Quaternion.Euler(Vector3.back * angle), duration).SetEase(ease))
                .Append(transform.DORotateQuaternion(originalRotation, duration).SetEase(ease))
                .AppendInterval(interval).SetLoops(-1, LoopType.Yoyo).SetTarget(this).Play();
        }

        public override void DoDisable()
        {
            base.DoDisable();
            DOTween.Kill(this);
            transform.localRotation = originalRotation;
        }
    }
}