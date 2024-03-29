using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using Dacodelaac.DataType;
using Dacodelaac.Events;
using Dacodelaac.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Dacodelaac.UI
{
    public class FlyCoinTarget : BaseMono
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private GameObject flyCoinPrefab;
        [SerializeField] private ShortDoubleVariable currency;
        [SerializeField] private PlayAudioEvent playAudioEvent;
        [SerializeField] private AudioClip collectAudio;

        protected List<GameObject> flyCoins = new List<GameObject>();

        public void FlyCoin(FlyCoinData data)
        {
            StartCoroutine(IEFlyCoin(data));
        }

        public override void DoDisable()
        {
            base.DoDisable();
            DOTween.Kill(this);
            foreach (var coin in flyCoins) pools.Despawn(coin);
            flyCoins.Clear();
        }

        private IEnumerator IEFlyCoin(FlyCoinData data)
        {
            yield return new WaitForEndOfFrame();

            if(playAudioEvent) playAudioEvent.Raise(collectAudio);
            
            var container = rect.GetComponentInParent<CanvasScaler>().transform;
            var corners = new Vector3[4];
            var position = container.InverseTransformPoint(data.Position);
            position.z = 0;
            var count = Random.Range(5, 15);
            if (data.Count != -1) count = data.Count;
            var completedCount = 0;
            var oldCurrency = currency.Value;
            
            for (var i = 0; i < count; i++)
            {
                var coin = pools.Spawn(flyCoinPrefab, container);

                flyCoins.Add(coin);
                coin.transform.localScale = Vector3.one;

                var coinRect = coin.GetComponent<RectTransform>();
                coinRect.anchoredPosition3D = position;

                var offset = (Vector3) Random.insideUnitCircle * 200f;
                var delay = Random.Range(0.3f, 0.7f);
                var duration = Random.Range(0.5f, 1f);
                coinRect.DOAnchorPos(position + offset, 0.3f).SetEase(Ease.OutQuad)
                    .SetTarget(this).OnComplete(() =>
                {
                    var startPos = coinRect.anchoredPosition3D;
                    DOTween.To(() => 0f,
                            x =>
                            {
                                rect.GetWorldCorners(corners);
                                var pos = (corners[0] + corners[2]) / 2;
                                var targetPos = container.InverseTransformPoint(pos);
                                targetPos.z = 0;
                                coinRect.anchoredPosition3D = Vector3.Lerp(startPos, targetPos, x);
                            }, 1f, duration).SetEase(Ease.InQuad).SetDelay(delay).SetTarget(this)
                        .OnComplete(() =>
                            {
                                flyCoins.Remove(coin.gameObject);
                                pools.Despawn(coin.gameObject);
                                completedCount++;
                                if (data.Animated) currency.Value++;
                            }).Play();
                }).Play();
            }

            yield return new WaitUntil(() => completedCount >= count);
            if (data.Animated) currency.Value = oldCurrency;
            yield return new WaitForSecondsRealtime(0.2f);
            data.OnCompleted?.Invoke();
        }
    }
}