using System;
using Code.Movement;

namespace Code.Interfaces
{
    public interface IAttacked
    {
        event Action<Direction> UnderAttack;
        event Action AttackFinished;
        bool IsUnderAttack { get; set; }
    }
}