using Code.Data;
using UnityEngine;

namespace Code.Team
{
    [CreateAssetMenu(fileName = "TeamData", menuName = "Team Data", order = 1)]
    public class TeamData : ScriptableObject, IProgressInspector, IProgressable, IProgressSetable<int>
    {
        public IntReference currentLevel = new IntReference();
        public IntReference targetLevel = new IntReference();

        public void Progress() => currentLevel.Value++;
        public void SetProgress(int level) => currentLevel.Value = level;

        public bool HasReachedTarget() => currentLevel.Value >= targetLevel.Value;
        public float PercentComplete() => currentLevel.Value / (float) targetLevel.Value * 100;
    }

    public interface IProgressSetable<T>
    {
        void SetProgress(T progress);
    }

    public interface IProgressable
    {
        void Progress();
    }

    public interface IProgressInspector
    {
        bool HasReachedTarget();
        float PercentComplete();
    }
}