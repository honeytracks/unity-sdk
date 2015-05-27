using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton to access the tracking api. You can savely place one in each scene and 
/// there will be only 1 global persistant instance of it.
/// 
/// Usage example:
/// HoneyTracksManager.Default().TrackLogin();
/// HoneyTracksManager.Default().TrackUserGender("female");
/// HoneyTracksManager.Space("anotherSpace").TrackLogout();
/// 
/// HoneyTracksManager.Instance.UniqueCustomerIdentifier = "123";
/// HoneyTracksManager.Instance.Language = "de";
/// HoneyTracksManager.Instance.ClientIp = "127.0.0.0";
/// 
/// This manager contains a link to your HoneyTracksConfig prefab.
/// 
/// </summary>
public class HoneyTracksManager : HoneyTracksManagerBase
{
    /// <summary>
    /// An example implementation that generates user ids randomly and saves them
    /// in the PlayerPrefs
    /// </summary>
    /// <returns></returns>
    private string GetOrSetRandomId()
    {
        if (PlayerPrefs.HasKey("honeytrackid"))
        {
            return PlayerPrefs.GetString("honeytrackid");
        }
        else
        {
            string id = "";
            while (id.Length < 32)
            {
                // +1: right border is exclusive
                id += (char)Random.Range((int)'a', 1+(int)'z');
            }
            PlayerPrefs.SetString("honeytrackid", id);
            return id;
        }
    }

    public override void SetUserspecificData()
    {
        // See https://docs.honeytracks.com/wiki/SDK_Default_and_mandatory_event_properties

        Debug.LogWarning("You need to adjust these values to fit your needs. Respect the user's data privacy!");

        // TODO adjust these to fit your needs
        ClientIp = "127.0.0.0";
        Language = "de";
        UniqueCustomerIdentifier = GetOrSetRandomId();

        // You can also adjust the config (api key, ...) depending on for example the target platform
        // config.ApiKey = "xxxxxxxxxxxxxx";
    }
}
