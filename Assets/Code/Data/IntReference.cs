using System;
using Code.Interfaces;

namespace Code.Data
{
    [Serializable]
    public class IntReference : IAtomicValue<int>
    {
        public int ConstantValue;
        public bool UseConstant;
        public IntValue Variable;

        public int Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set => Variable.Value = value;
        }
    }
}