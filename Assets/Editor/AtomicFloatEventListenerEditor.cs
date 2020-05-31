using Code.Events.Core;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AtomicFloatEventListener))]
    public class AtomicFloatEventListenerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var targetComponent = (AtomicFloatEventListener) target;

            DrawDefaultInspector();

            if (GUILayout.Button("Trigger Event"))
            {
                if (Application.isPlaying)
                    targetComponent.OnEventTriggered(4f);
                else
                    Debug.LogWarning("You must be playing the game to trigger a game event.");
            }
        }
    }
}