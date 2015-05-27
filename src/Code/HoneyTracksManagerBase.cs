using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoneyTracks;
using HoneyTracks.Exceptions;

/// <summary>
/// Singleton to access the tracking api. You can savely place one in each scene and 
/// there will be only 1 global persistant instance of it.
/// 
/// Usage example:
/// HoneyTracksManager.Default().TrackLogin();
/// HoneyTracksManager.Default().TrackUserGender("female");
/// HoneyTracksManager.Space("anotherSpace").TrackLogout();
/// 
/// To set the Unique Id, Client Ip or Language (IMPORTANT do it prior HoneyTracksManager.Start):
/// HoneyTracksManager.Instance.UniqueCustomerIdentifier = "123";
/// HoneyTracksManager.Instance.Language = "de";
/// HoneyTracksManager.Instance.ClientIp = "127.0.0.0";
/// 
/// </summary>
public abstract class HoneyTracksManagerBase : MonoBehaviour
{
    #region Singleton
    private static HoneyTracksManagerBase instance = null;

    /// <summary>
    /// Singleton access point, use this to access the different tracking spaces
    /// </summary>
    public static HoneyTracksManagerBase Instance
    {
        get
        {
            if (instance == null)
            {
                throw new GeneralException("There is no HoneyTracksManager in the scene");
            }
            else
            {
                return instance;
            }
        }
    }
    #endregion

    #region TrackerUrl
    private string trackerUrl = "http://tracker.honeytracks.com/?ApiKey=%1$s";
    /// <summary>
    /// Tracker url
    /// </summary>
    public string TrackerUrl
    {
        get
        {
            return trackerUrl.Replace("%1$s", config.ApiKey);
        }
        set
        {
            trackerUrl = value;
        }
    } // TrackerUrl
    #endregion

    [SerializeField]
    protected HoneyTracksConfig config = null;

    /// <summary>
    /// This value defaults to unique device id. If you want to use another user id 
    /// set this prior the execution of the Start method.
    /// </summary>    
    public string UniqueCustomerIdentifier
    {
        get;
        protected set;
    }

    /// <summary>
    /// If you want to set the client ip
    /// set this prior the execution of the Start method.
    /// </summary>
    public string ClientIp
    {
        get;
        protected set;
    }

    /// <summary>
    /// If you want to set the language ip
    /// set this prior the execution of the Start method.
    /// </summary>
    public string Language
    {
        get;
        protected set;
    }

    /// <summary>
    /// Disable this to replace the GetDefault/GetSpace calls with a NOP object
    /// (there will be no calls).
    /// </summary>
    public bool IsEnabled = true;

    /// <summary>
    /// Shows Debug.Log of the honeytrack activity
    /// </summary>
    public bool ShowLog = true;

    /// <summary>
    /// contains all undelivered events
    /// </summary>
    private List<TrackingEvent> undeliveredEvents = new List<TrackingEvent>();

    private Transport transport;

    private Dictionary<string, HoneyTracks.ITracking> spaceTrackingMap = new Dictionary<string, HoneyTracks.ITracking>();

    /// <summary>
    /// this turns true if the system is ready to be used
    /// </summary>
    private bool isSetupDone = false;

    #region static shortcut methods

    /// <summary>
    /// Access the default tracking space, static shortcut
    /// </summary>
    /// <returns></returns>
    public static  HoneyTracks.ITracking Default()
    {
        return Instance.GetDefault();
    }

    /// <summary>
    /// Access a named tracking space, static shortcut
    /// </summary>
    /// <param name="space"></param>
    /// <returns></returns>
    public static HoneyTracks.ITracking Space(string space)
    {
        return Instance.GetSpace(space);
    }

    #endregion


    #region access the tracking api

    /// <summary>
    /// Access the default tracking space
    /// </summary>
    /// <returns></returns>
    public HoneyTracks.ITracking GetDefault()
    {
        return GetSpace("Default");
    }

    /// <summary>
    /// Access a named tracking space
    /// </summary>
    /// <param name="space"></param>
    /// <returns></returns>
    public HoneyTracks.ITracking GetSpace(string space)
    {
        if (!IsEnabled)
        {
            return new HoneyTracks.TrackingNop();
        }

        if (!isSetupDone)
        {
            throw new System.Exception("HoneyTracks is not ready, you need to call HoneyTrackManager.Start() prior calling any tracking methods. Don't track in Awake() methods. You should add the HoneyTracksManager above \"Default Time\" in \"Edit > Project Settings > Script Execution Order\".");
        }

        if (!spaceTrackingMap.ContainsKey(space))
        {
            // this is the "real" honeytracks config
            // if you want to use a secret key you can add it here.
            var c = new Config();
            c.ApiKey = config.ApiKey;
            c.Language = string.IsNullOrEmpty(Language) ? config.Language : Language;
            c.Space = space;
            c.UniqueCustomerIdentifier = UniqueCustomerIdentifier;
            c.ClientIP = ClientIp;
            c.Version = config.Version;

            var t = new HoneyTracks.Tracking(c, transport);
            spaceTrackingMap[space] = t;
            return t;
        }
        else
        {
            return spaceTrackingMap[space];
        }
    }

    /// <summary>
    /// Removes all bound spaces. Call this if you change the customer UID because
    /// spaces bind the current config on first access.
    /// </summary>
    public void ClearSpaces()
    {
        spaceTrackingMap.Clear();
    }

    #endregion

    #region storage of events

    void OnApplicationPause (bool pause)
    {
        if (pause)
        {
            SaveUndeliveredEvents();
        }
        else
        {
            LoadUndeliveredEvents();
        }
    }

    void OnApplicationQuit()
    {
        SaveUndeliveredEvents();
    }

    /// <summary>
    /// loads the stored events and prepares them for resending
    /// </summary>
    private void LoadUndeliveredEvents()
    {
        int count = 0;
        var json = PlayerPrefs.GetString("htevents", "[]");
        var events = MiniJSON.Json.Deserialize(json) as List<object>;
        foreach(var entry in events)
        {
            try 
            {
                undeliveredEvents.Add(TrackingEvent.FromJson(entry.ToString()));
                ++count;
            }
            catch(System.Exception e)
            {
                Debug.LogError("error parsing stored event: " + e.Message);
            }
        }
        PlayerPrefs.SetString("htevents", "[]");

        if (ShowLog) Debug.Log(string.Format("loaded {0} undelivered tracking events", count));
    }

    /// <summary>
    /// store the undelivered events
    /// </summary>
    private void SaveUndeliveredEvents()
    {
        List<string> events = new List<string>();
        
        int count = 0;

        for (int i = 0; i < undeliveredEvents.Count; ++i )
        {
            events.Add(undeliveredEvents[i].ToJson());
            ++count;
        }
        undeliveredEvents.Clear();

        PlayerPrefs.SetString("htevents", MiniJSON.Json.Serialize(events));

       if (ShowLog) Debug.Log(string.Format("saved {0} undelivered tracking events", count));
    }

    #endregion

    #region startup

    void Awake()
    {
        if (instance == null)
        {
            if (ShowLog) Debug.Log("Registering HoneyTracksManager globally", gameObject);
            instance = this;
            transport = new Transport(this.undeliveredEvents, config.MaxStoredEvents);
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
           if (ShowLog) Debug.Log("There is already a HoneyTracksManager registered -> destroy this one", gameObject);
            GameObject.Destroy(gameObject);
        }
    }

    public virtual void SetUserspecificData()
    {
        // override this to set
        // UniqueCustomerIdentifier
        // ClientIp
        // Language
    }

    void Start () 
    {
        if (instance != this) return;

        SetUserspecificData();
        isSetupDone = true;

        if (string.IsNullOrEmpty(UniqueCustomerIdentifier))
        {
            UniqueCustomerIdentifier = "NO_UNIQUE_CUSTOMER_IDENTIFIER";
        }

       if (ShowLog) Debug.Log("tracked user id: " + UniqueCustomerIdentifier);

        LoadUndeliveredEvents();

        StartCoroutine(CoDeliverEvents());
	}

    #endregion

    #region delivery

    /// <summary>
    /// CoRoutine that runs continuously and tries to deliver tracking events if necessary.
    /// </summary>
    /// <returns></returns>
    IEnumerator CoDeliverEvents()
    {
        while (true)
        {
            if (undeliveredEvents.Count > 0)
            {
                // deliver events
                int count = Mathf.Min(config.MaxEventCountPerDeliver, undeliveredEvents.Count);
                var events = undeliveredEvents.GetRange(0, count);
                undeliveredEvents.RemoveRange(0, count);
                yield return StartCoroutine(CoDeliverEventsChunk(events));
            }

            yield return new WaitForSeconds(config.DeliverTimeout);
        }
    }

    private IEnumerator CoDeliverEventsChunk(List<TrackingEvent> events)
    {
        if (ShowLog) Debug.Log(string.Format("start to deliver {0} tracking events", events.Count));

        var form = new WWWForm();

        // store the events in the post data
        for (int i = 0; i < events.Count; ++i)
        {
            var ev = events[i];

            form.AddField("Packets[" + i + "][Action]", ev.Action);

            foreach(var pair in ev.EventData)
            {
                form.AddField("Packets[" + i + "][" + pair.Key + "]", pair.Value);
            }
        }

        // post tracking data
        var www = new WWW(TrackerUrl, form);
        yield return www;

        // something went wrong?
        if (string.IsNullOrEmpty(www.error))
        {
           if (ShowLog) Debug.Log(string.Format("delivered {0} tracking events", events.Count));
        }
        else
        {
           if (ShowLog) Debug.Log(string.Format("put the {0} tracking events back into undelivered ({1})", 
                events.Count, www.error));
            undeliveredEvents.InsertRange(0, events);
            // wait a little longer if there was an error to not spam of the player is offline
            yield return new WaitForSeconds(config.DeliverErrorPause);
        }
    }

    #endregion
}
