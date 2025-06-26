using System;
using UnityEngine;

namespace RewardMultiplierRoulette
{
    public class RewardRoulette : MonoBehaviour
    {
        [SerializeField]
        private IndicatorArrow _indicatorArrow;
        [SerializeField]
        private Multiplier[] _multipliers = Array.Empty<Multiplier>();

        public void Launch()
        {
            _indicatorArrow.StartAnimation();
        }

        public void Stop()
        {
            _indicatorArrow.StopAnimation();
        }

        public void ShowMultiplier()
        {
            Debug.Log($"Selected Multiplier: {GetMultiplier()}x");
        }

        public int GetMultiplier()
        {
            float arrowX = _indicatorArrow.transform.localPosition.x;
            int closestIndex = 0;
            float minDistance = Mathf.Abs(_multipliers[0].RectTransform.localPosition.x - arrowX);

            for (int i = 1; i < _multipliers.Length; i++)
            {
                float distance = Mathf.Abs(_multipliers[i].RectTransform.localPosition.x - arrowX);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }

            return _multipliers[closestIndex].Value;
        }

        private void Start()
        {
            _indicatorArrow.SetMinimumPositionX(_multipliers[0].RectTransform.localPosition.x);
            _indicatorArrow.SetMaximumPositionX(_multipliers[^1].RectTransform.localPosition.x);
        }
    }
}
