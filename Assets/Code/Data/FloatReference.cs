using System;
using Code.Interfaces;
using UnityEngine;

namespace Data
{

    [CreateAssetMenu(fileName = "FloatReference", menuName = "Float Reference", order = 0)]
    public class FloatReference: ScriptableObject, IAtomicFloat
    {
        public bool UseConstant;
        public float ConstantValue;
        public FloatValue Variable;

        public event Action<float> OnChange;
        
        public float Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                Variable.Value = value;
                OnChange?.Invoke(Variable.Value);
            }
        }
    }
}