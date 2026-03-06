using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnergySaver))]
public class EnergySaverEditor : Editor
{
    bool foldout = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Repaint();

        EnergySaver energySaver = (EnergySaver)target;
        EnergyProfile profile = energySaver.CurrentProfile;

        foldout = EditorGUILayout.Foldout(foldout, "Current Profile");
        GUI.enabled = false;
        if(foldout)
        {
            if (profile != null)
            {
                DrawCurrentProfile(profile);
            }
            else
            {
                EditorGUILayout.LabelField("No profile active.");
            }
        }
        GUI.enabled = true;
    }

    void DrawCurrentProfile(EnergyProfile profile)
    {
        EditorGUILayout.LabelField(profile.Definition.name, EditorStyles.boldLabel);
        EditorGUILayout.FloatField("Activate After Idle", profile.ActivateAfterIdle);
        EditorGUILayout.EnumFlagsField("Power Constraints", profile.PowerConstraints);
        EditorGUILayout.IntField("Priority", profile.Priority);
        EditorGUILayout.IntField("Max FPS", profile.MaxFps);
        EditorGUILayout.IntField("RenderInterval", profile.RenderInterval);
        EditorGUILayout.FloatField("Render Scale Multiplier", profile.RenderScaleMultiplier);
        EditorGUILayout.EnumFlagsField("Physics Simulation Mode", profile.PhysicsSimulationMode);
    }
}
