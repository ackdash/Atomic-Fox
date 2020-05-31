using System;
using Code.Events.Core;
using Code.Interfaces;
using UnityEngine;

namespace Code.Team
{
    public class TeamProgress : MonoBehaviour, IProgressInspector
    {
        [SerializeField]
        private AtomicEvent teamFinished;
        
        private ITeamProgressMonitorable progressInspector;
        private bool canInspectProgress;

        private void Start()
        {
            progressInspector =  GetComponent<ITeamProgressMonitorable>();
            canInspectProgress = progressInspector != null;
        }

        public bool HasReachedTarget() => canInspectProgress && progressInspector.HasReachedTarget();
        public float PercentComplete() => canInspectProgress ? progressInspector.PercentComplete() : 0f;

        public bool CheckState()
        {
            var completedObjective = HasReachedTarget();
            if (completedObjective) teamFinished.Trigger();

            return completedObjective;
        }
        
        public void CheckIfComplete()
        {
            CheckState();
        }
    }
}
