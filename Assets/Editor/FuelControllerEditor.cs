using Code.Actor.Fuel;
using Code.Events.Core;
using UnityEditor;
using UnityEngine;

namespace Editor
{


    namespace Editor
    {
        [CustomEditor(typeof(FuelController))]
        public class FuelControllerEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var targetComponent = (FuelController) target;

                DrawDefaultInspector();

                if (GUILayout.Button("Trigger Drop"))
                {
                    if (Application.isPlaying)
                        targetComponent.Dropped();
                    else
                        Debug.LogWarning("You must be playing the game to trigger Fuel Drop.");
                }
                
                if (GUILayout.Button("Trigger Reset"))
                {
                    if (Application.isPlaying)
                        targetComponent.Reset();
                    else
                        Debug.LogWarning("You must be playing the game to trigger Fuel Reset.");
                }
            }
        }
    }
}