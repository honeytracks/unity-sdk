using UnityEngine;
using System.Collections;

/// <summary>
/// Prefab with the static configuration part
/// </summary>
public class HoneyTracksConfig : MonoBehaviour
{
    public string ApiKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
    public string Language = "EN";
    public string Version;

    #region general configs
    public int MaxEventCountPerDeliver = 50;
    public float DeliverTimeout = 0.1f;
    public float DeliverErrorPause = 5f;
    public int MaxStoredEvents = 200;
    #endregion
}
