using Code.Events.Core;
using Code.Interfaces;
using UnityEngine;

namespace Code.Team
{
    public class TeamProgress : MonoBehaviour, IProgressInspector
    {
        private bool canInspectProgress;

        private ITeamProgressMonitorable progressInspector;

        [SerializeField] private AtomicEvent teamFinished;

        public bool HasReachedTarget() => canInspectProgress && progressInspector.HasReachedTarget();
        public float PercentComplete() => canInspectProgress ? progressInspector.PercentComplete() : 0f;

        private void Start()
        {
            progressInspector = GetComponent<ITeamProgressMonitorable>();
            canInspectProgress = progressInspector != null;
        }

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