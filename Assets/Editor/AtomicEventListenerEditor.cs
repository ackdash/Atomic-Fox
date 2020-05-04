using Code.Framework.Events;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AtomicEventListener))]
public class AtomicEventListenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var targetComponent = (AtomicEventListener) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Trigger Event"))
        {
            if (Application.isPlaying)
                targetComponent.OnEventTriggered();
            else
                Debug.LogWarning("You must be playing the game to trigger a game event.");
        }
    }
}