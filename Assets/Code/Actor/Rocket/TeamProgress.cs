using System;
using Code.Data;
using Code.Events.Core;
using Code.Interfaces;
using Code.Team;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Playables;

namespace Code.Actor.Rocket
{
    public class TeamProgress : MonoBehaviour, IProgressInspector
    {
        [SerializeField]
        private ScriptableObject teamData;
        
        [SerializeField]
        private AtomicEvent completedLevel;
        
        private IProgressInspector progressInspector;
        private bool canInspectProgress;

        private void Start()
        {
            progressInspector = (IProgressInspector) teamData;
            canInspectProgress = progressInspector != null;
        }

        public bool HasReachedTarget() => canInspectProgress && progressInspector.HasReachedTarget();
        public float PercentComplete() => canInspectProgress ? progressInspector.PercentComplete() : 0f;

        public bool CheckState()
        {
            var levelComplete = HasReachedTarget();
            if (levelComplete) completedLevel.Trigger();

            return levelComplete;
        }
        
        public void CheckIfComplete()
        {
            CheckState();
        }
    }
}
