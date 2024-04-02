using Dacodelaac.Core;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

namespace SurvivorRoguelike.UI
{
    public class Star : BaseMono
    {
        [SerializeField] Image star;
        [SerializeField] Sprite starEnable;
        [SerializeField] Sprite starDisable;
        private float minAlpha = 0.2f;
        private float maxAlpha = 1.0f;
        private float duration = 0.4f;

        public void Setup(bool enable)
        {
            Refresh();
            star.sprite = enable ? starEnable : starDisable;
        }

        public void Blink()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(star.DOFade(minAlpha, 0));
            sequence.Append(star.DOFade(maxAlpha, duration));
            sequence.Append(star.DOFade(minAlpha, 0));
            sequence.SetLoops(-1, LoopType.Yoyo);
            sequence.SetTarget(this);
        }

        public override void DoDisable()
        {
            Refresh();
        }

        void Refresh()
        {
            DOTween.Kill(this);
            star.color = Color.white;
        }
    }
}