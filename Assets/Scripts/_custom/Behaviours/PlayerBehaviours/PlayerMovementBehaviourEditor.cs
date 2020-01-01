using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerMovementBehaviour))]
public class PlayerMovementBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerMovementBehaviour current = (PlayerMovementBehaviour)target;
        GUILayout.Space(15);
        GUILayout.TextArea("Important! *before pressing the button below, make sure the current main camera is the desired player camera*");
        if(GUILayout.Button("Auto Assemble Player"))
        {
            current.AssemblePlayer();
        }
    }
}
