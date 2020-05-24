using Code.Interfaces;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "FloatValue", menuName = "Int Value", order = 1)]
    public class IntValue : ScriptableObject, IAtomicValue<int>
    {
        [SerializeField]
        private int _value;

        public int Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
