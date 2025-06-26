using UnityEngine;

namespace RewardMultiplierRoulette
{
    public class Multiplier : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private int _value;
        
        public RectTransform RectTransform => _rectTransform;
        public int Value => _value;
    }
}