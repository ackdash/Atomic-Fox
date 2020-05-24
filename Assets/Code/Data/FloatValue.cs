using Code.Interfaces;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "FloatValue", menuName = "Float Value", order = 1)]
    public class FloatValue : ScriptableObject, IAtomicFloat
    {
        [SerializeField]
        private float _value;

        public float Value
        {
            get => _value;
            set => _value = value;
        }
    }
}