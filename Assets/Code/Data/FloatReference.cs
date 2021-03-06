﻿using System;
using Code.Interfaces;

namespace Code.Data
{
    [Serializable]
    public class FloatReference : IAtomicValue<float>
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