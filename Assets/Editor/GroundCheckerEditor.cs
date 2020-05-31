using Code.Events.Core;
using Code.Movement;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    
    [CustomEditor(typeof(GroundChecker))]
    public class GroundCheckEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var targetComponent = (GroundChecker) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Check Ground"))
            {
                if (Application.isPlaying)
                    Debug.Log($"GroundCheck:: is on ground => {targetComponent.Check().ToString()}");
                else
                    Debug.LogWarning("You must be playing the game to trigger a game event.");
            }
            
        }
    }
}