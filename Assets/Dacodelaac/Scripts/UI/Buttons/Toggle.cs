using Dacodelaac.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dacodelaac.UI.Buttons
{
    public class Toggle : BaseMono
    {
        [SerializeField] Image background;
        [SerializeField] RectTransform thumb;
        [SerializeField] Sprite onSprite;
        [SerializeField] Sprite offSprite;
        [SerializeField] Text onText;
        [SerializeField] Text offText;
        [SerializeField] Vector2 offThumbPos;
        [SerializeField] Vector2 onThumbPos;

        public void SetState(bool isOn, bool animate)
        {
            onText.gameObject.SetActive(isOn);
            offText.gameObject.SetActive(!isOn);
            background.sprite = isOn ? onSprite : offSprite;
            DOTween.Kill(this);
            if (animate)
            {
                thumb.DOAnchorPos(isOn ? onThumbPos : offThumbPos, 0.15f).SetTarget(this).Play();
            }
            else
            {
                thumb.anchoredPosition = isOn ? onThumbPos : offThumbPos;
            }
        }
    }
}