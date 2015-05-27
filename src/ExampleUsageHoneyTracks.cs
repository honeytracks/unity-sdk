using UnityEngine;
using System.Collections;

/// <summary>
/// An example how to call the tracking api
/// </summary>
public class ExampleUsageHoneyTracks : MonoBehaviour
{
    void Awake()
    {
        HoneyTracksManager.Default().TrackLogin();
    }

    void OnGUI()
    {
        float width = 100;
        float height = 50;

        if (GUILayout.Button("male", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackUserGender("male");
        }
        if (GUILayout.Button("female", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackUserGender("female");
        }
        if (GUILayout.Button("login", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackLogin();
        }
        if (GUILayout.Button("logout", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackLogout();
        }
        if (GUILayout.Button("clickme", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackClick("clickme");
        }
        if (GUILayout.Button("clickyou", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackClick("clickyou");
        }
        if (GUILayout.Button("signup", GUILayout.Width(width), GUILayout.Height(height)))
        {
            HoneyTracksManager.Default().TrackSignup();
        }
    }
}
