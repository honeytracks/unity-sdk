using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HoneyTracksConfig))]
public class HoneyTracksConfigInspector : Editor
{
    private bool showInternals = false;

    public override void OnInspectorGUI()
    {
        HoneyTracksConfig t = (HoneyTracksConfig)target;

        t.ApiKey = EditorGUILayout.TextField("Api key", t.ApiKey);
        t.Language = EditorGUILayout.TextField("Default Language", t.Language);
        t.Version = EditorGUILayout.TextField("Version", t.Version);

        EditorGUILayout.Space();

        showInternals = EditorGUILayout.Foldout(showInternals, "Internals");
        if (showInternals)
        {
            t.MaxEventCountPerDeliver = EditorGUILayout.IntField("Max number of events per delivery", t.MaxEventCountPerDeliver);
            t.DeliverTimeout = EditorGUILayout.FloatField("Delivery timeout", t.DeliverTimeout);
            t.DeliverErrorPause = EditorGUILayout.FloatField("Pause time between delivery errors", t.DeliverErrorPause);
            t.MaxStoredEvents = EditorGUILayout.IntField("Max number of stored undelivered events", t.MaxStoredEvents);
        }

        if (GUI.changed) EditorUtility.SetDirty(t);
    }
}