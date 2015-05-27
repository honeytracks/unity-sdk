using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HoneyTracksManager))]
public class HoneyTracksManagerInspector : Editor
{
    public override void OnInspectorGUI ()
    {
        HoneyTracksManager t = (HoneyTracksManager)target;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("UniqueCustomerIdentifier", t.UniqueCustomerIdentifier);
        EditorGUILayout.LabelField("ClientIp", t.ClientIp);
        EditorGUILayout.LabelField("Language", t.Language);

        if (GUI.changed) EditorUtility.SetDirty(t);
    }
}
