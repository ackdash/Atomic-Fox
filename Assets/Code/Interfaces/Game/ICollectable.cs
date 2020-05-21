using UnityEngine;

namespace Code.Interfaces.Game
{
    public interface ICollectable
    {
        void Collect(Transform collector);
    }
}