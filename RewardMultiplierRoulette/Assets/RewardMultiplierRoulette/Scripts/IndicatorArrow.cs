using DG.Tweening;
using UnityEngine;

namespace RewardMultiplierRoulette
{
    public class IndicatorArrow : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _moveDuration = 0.5f;
        
        private Vector3 _startLocalPosition;
        private Vector2 _startSizeDelta;
        
        private float _minimumPositionX;
        private float _middlePositionX;
        private float _maximumPositionX;
        
        private Tween _positionTween;
        private Tween _shakeTween;
        private Tween _zoomTween;

        public void SetMinimumPositionX(float minimumPositionX) => _minimumPositionX = minimumPositionX;

        public void SetMaximumPositionX(float maximumPositionX) => _maximumPositionX = maximumPositionX;

        public void StartAnimation()
        {
            KillTween();
            
            _rectTransform.localPosition = _startLocalPosition;
            _rectTransform.sizeDelta = _startSizeDelta;
            
            _positionTween = DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPosX(_minimumPositionX, _moveDuration).SetEase(Ease.OutQuad))
                .Append(_rectTransform.DOAnchorPosX(_middlePositionX, _moveDuration).SetEase(Ease.InQuad))
                .Append(_rectTransform.DOAnchorPosX(_maximumPositionX, _moveDuration).SetEase(Ease.OutQuad))
                .Append(_rectTransform.DOAnchorPosX(_middlePositionX, _moveDuration).SetEase(Ease.InQuad))
                .SetLoops(-1);
        }
        
        public void StopAnimation()
        {
            KillTween();
            
            _shakeTween = _rectTransform.DOShakeAnchorPos(.5f, new Vector2(0, 20f)).SetEase(Ease.InOutBack);
            
            _zoomTween = _rectTransform.DOScale(1.2f, 0.25f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _rectTransform.DOScale(1f, 0.12f).SetEase(Ease.InBack);
                });
        }

        private void KillTween()
        {
            _positionTween?.Kill();
            _positionTween = null;
            
            _shakeTween?.Kill();
            _shakeTween = null;
            
            _zoomTween?.Kill();
            _zoomTween = null;
        }

        private void Start()
        {
            _startLocalPosition = _rectTransform.localPosition;
            _startSizeDelta = _rectTransform.sizeDelta;
            _middlePositionX = _rectTransform.localPosition.x;
        }

        private void OnDestroy()
        {
            KillTween();
        }
    }
}
