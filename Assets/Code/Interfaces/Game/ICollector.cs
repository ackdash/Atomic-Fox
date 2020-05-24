using UnityEngine;

namespace Code.Interfaces.Game
{
    public interface ICollector
    {
        bool HasItems { get; set; }
        void Collect(GameObject item);
        Transform GetItem(string itemTag);
        void Clear();
    }
}