using Code.Data;
using Code.Interfaces;
using UnityEngine;

namespace Code.Team
{
    public class FuelingTeamProgress : MonoBehaviour, ITeamProgressMonitorable
    {
        public IntReference currentLevel = new IntReference();
        public IntReference targetLevel = new IntReference();
        public void Progress() => currentLevel.Value++;
        public void SetProgress(int level) => currentLevel.Value = level;

        public bool HasReachedTarget() => currentLevel.Value >= targetLevel.Value;
        public float PercentComplete() => currentLevel.Value / (float) targetLevel.Value * 100;

        private void Start()
        {
            ResetProgress();
        }

        public void ResetProgress() => currentLevel.Value = 0;
        
    }
}