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

        if (GUILayout.Button("Trigger Event")) targetComponent.OnEventTriggered();
    }
}