using UnityEngine;

namespace Code.Interfaces.Game
{
    public interface ICollector
    {
        void Collect(GameObject item);

        bool HasItems { get; set; }
        Transform GetItem(string itemTag);
        void Clear();

    }
}