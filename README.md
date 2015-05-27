unity-sdk
=======

**HoneyTracks Unity SDK**

For more implementation details please also have a look to https://honeytracks.com/first-steps/ and https://docs.honeytracks.com/wiki/Getting_started#tab=Unity_C_23.


**Disclaimer**
This plugin is loosely based on the c# plugin by freejamgames you can find at
https://docs.honeytracks.com/images/d/d0/HoneyTracks_Tracking_Library_3rd_party_SDK_Source.zip

----------------------------------
HowTo
----------------------------------

See the ExampleHoneyTracks scene for an example usage. 

 * Add the Prefab HoneyTracksManager to each of your scenes or a bootstrap scene (it is a DontDestroyOnLoad object)
 * Adjust the HoneyTracksConfig prefab with your api key, version and default language
 * Make sure that your HoneyTracksManager links to your HoneyTracksConfig
 * Adjust the HoneyTracksManager.SetUserspecificData method to fit your needs.
 * Start tracking things, e.g.
	 *   HoneyTracksManager.Default().TrackLogin();
	 * HoneyTracksManager.Default().TrackUserGender("female");
	 * HoneyTracksManager.Space("anotherSpace").TrackLogout();
 * You can find all possible tracking methods in the HoneyTracks.ITracking interface

**ATTENTION:** 
The PlayerPref storage is limited and therefore the number of stored undelivered events is limited.
If you track more than the limit the system drops the old undelivered events.
Try to keep the number of tracking events to a sane number (don't track each Update() call).
This is only the case if the client is offline and you track hundreds of events during that time.

--------------------------------------------------------------------
What happens to the tracking if the application is not online?
--------------------------------------------------------------------

The undelivered events get stored in the PlayerPrefs and the application tries to
deliver them on a regular basis. So they get send the next time the application is online.
Closing the application does not delete these stored events.


--------------------------------------------------------------------
Is it possible to have multiple configs? One per target platform?
--------------------------------------------------------------------

Yes, you can create different HoneyTrackConfig prefabs and in your
HoneyTracksManager.SetUserspecificData you assign the correct config to this.config.


--------------------------------------------------------------------
Can I call the api from multiple threads?
--------------------------------------------------------------------

No, the Api is not thread safe. The tracking api
runs within the Unity Thread and it in most cases there is no locking necessary.
If you call it from real extra threads you need to implement it yourself. 
On quite easy way would be to queue Action calls in a locked queue that you execute within the context
of the HoneyTracksManager.Update().

HoneyTracksManager.Queue(() => {
	HoneyTracksManager.Instance.GetSpace("anotherSpace").TrackLogout();
});


--------------------------------------------------------------------
Where can I set the secret key?
--------------------------------------------------------------------

Because your customers client is not safe your should not use your secret key here.
But if you really want you can add it similar to HoneyTracksConfig.ApiKey
(see HoneyTracksManagerBase.GetSpace and HoneyTracksConfig.ApiKey).
