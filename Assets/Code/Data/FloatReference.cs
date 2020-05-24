using System;
using Code.Interfaces;

namespace Data
{
    [Serializable]
    public class FloatReference : IAtomicFloat
    {
        public float ConstantValue;
        public bool UseConstant;
        public FloatValue Variable;

        public float Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set => Variable.Value = value;
        }
    }
}