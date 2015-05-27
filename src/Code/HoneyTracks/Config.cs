// Project: HoneyTracks
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using HoneyTracks.Exceptions;
using HoneyTracks.Helper;
namespace HoneyTracks
{
	/// <summary>
	/// Config
	/// </summary>
	public class Config
	{
		#region Constants
		/// <summary>
		/// Default tracker url
		/// </summary>
		#endregion

		#region Public
		#region TrackerUrl
		private string trackerUrl = "http://tracker.honeytracks.com/?ApiKey=%1$s";
		/// <summary>
		/// Tracker url
		/// </summary>
		public string TrackerUrl
		{
			get
			{
				return trackerUrl.Replace ("%1$s", ApiKey);
			}
			set {
				trackerUrl = value;
			}
		} // TrackerUrl
		#endregion

		#region ApiKey
		private string apiKey = "";
		/// <summary>
		/// The Api-Key defines the application, the value is defined by 
		/// HoneyTracks and is available in the HoneyTracks application settings.
		/// </summary>
		public string ApiKey
		{
			get
			{
				return apiKey;
			}
			set
			{
				Regex rx = new Regex("^([a-f0-9]{32,40})$");
				MatchCollection matches = rx.Matches(value);
				if (matches.Count == 0)
				{
					throw new ConfigurationException("ApiKey is invalid");
				}
				apiKey = value;
			} // set
		} // ApiKey
		#endregion

		#region SecrecKey
		private string secretKey = "";
		/// <summary>
		/// Secret key
		/// </summary>
		public string SecretKey
		{
			get
			{
				return secretKey;
			}
			set
			{
				Regex rx = new Regex("^([a-f0-9]{32,40})$");
				MatchCollection matches = rx.Matches(value);
				if (matches.Count == 0)
				{
					throw new ConfigurationException("SecretKey is invalid");
				}
				secretKey = value;
			} // set
		} // SecretKey
		#endregion

		#region UniqueCustomerIdentifier
		/// <summary>
		/// A unique user identification within your application.
		/// • „en_US::World1::12345“
		/// • „&lt;FACEBOOK USER ID&gt;“
		/// • „en_US::World1::&lt;FACEBOOK USER ID&gt;
		/// If your application is published on more than one distribution 
		/// channel, e.g. game worlds, social networking plattforms or closed 
		/// language versions, you have to add the corresponding information to 
		/// your UniqueCustomerIdentifier-token 
		/// </summary>
		public string UniqueCustomerIdentifier
		{
			get;
			set;
		} // UniqueCustomerIdentifier
		#endregion

		#region Language
		/// <summary>
		/// Language of your application, e.g. 
		/// • „deu“ (ISO - 639 - 3)  
		/// • „de_DE“ („locale“ - definition, „language_TERRITORY“) 
		/// </summary>
		public string Language
		{
			get;
			set;
		} // Language
		#endregion

		#region Version
		/// <summary>
		/// Current code - version of your application, you should use your real 
		/// sourcecode versions to be able to track differences between 
		/// application changes, e.g.
		/// • 0.9.1 r1234
		/// </summary>
		public string Version
		{
			get;
			set;
		} // Version
		#endregion

		#region ClientIP
		/// <summary>
		/// The IP of the client if available. The last octet has to be masked 
		/// for privacy protection. The ClientIP is used only for identifying the 
		/// place of origin. 
		/// </summary>
		public string ClientIP
		{
			get;
			set;
		} // ClientIP
		#endregion

		#region Space
		/// <summary>
		/// The definition of your application distribution channel, e.g. game 
		/// world or social 
		/// networking plattforms:
		/// • „World1“
		/// • „Facebook“
		/// • „hi5“
		/// • „orkut“  
		/// </summary>
		public string Space
		{
			get;
			set;
		} // Space
		#endregion

		#region Timestamp
		private long timestamp;
		/// <summary>
		/// Defines the time of the action in GMT. This value is optional and if 
		/// not present the current time (GMT) will used by our tracking servers.
		/// </summary>
		public long Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				if (value <= 1262300400)
				{
					throw new ConfigurationException("Timestamp is invalid.");
				}
				timestamp = value;
			}
		} // TimeStamp
		#endregion

		#region UniqueCustomerSubToken
		/// <summary>
		/// A unique sub token for an user, e .g. a character id if the user can 
		/// have more than one character within a game.
		/// </summary>
		public string UniqueCustomerSubToken
		{
			get;
			set;
		} // UniqueCustomerSubToken
		#endregion

		#region BackupPath
		/// <summary>
		/// Backup path, here will the tracker save the packets, that can't be send.
		/// </summary>
		public string BackupPath
		{
			get;
			set;
		} // BackupPath
		#endregion

		#region AutoTimestamp
		/// <summary>
		/// Set client timestamp automaticaly.
		/// </summary>
		public bool AutoTimestamp
		{
			get;
			set;
		} // AutoTimestamp
		#endregion

		#region AutoCommitCycle
		/// <summary>
		/// Commit all packets in the given cycle in seconds.
		/// Default is -1: No auto commit
		/// 0: Send after each event.
		/// </summary>
		public long AutoCommitCycle
		{
			get;
			set;
		} // AutoCommit
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create config
		/// </summary>
		public Config()
		{
			Space = "";
			Language = "";
			ClientIP = "";
			Version = "";
			UniqueCustomerIdentifier = "";
			BackupPath = "";
			timestamp = 0;
			UniqueCustomerSubToken = "";
			AutoTimestamp = false;
			AutoCommitCycle = -1;
		} // Config()
		#endregion

		#region GetParameterList
		/// <summary>
		/// Get parameter list
		/// </summary>
		internal List<KeyValuePair<string, string>> GetParameterList(long customEventTimestamp)
		{
			List<KeyValuePair<string, string>> param = 
				new List<KeyValuePair<string,string>>();

			param.Add(new KeyValuePair<string, string>("UniqueCustomerIdentifier",
				UniqueCustomerIdentifier));
			param.Add(new KeyValuePair<string, string>("Language", Language));
			param.Add(new KeyValuePair<string, string>("Version", Version));
			param.Add(new KeyValuePair<string, string>("ClientIP", ClientIP));
			param.Add(new KeyValuePair<string, string>("Space", Space));
			
			if(customEventTimestamp == 0) {
				if (Timestamp == 0 || 
					AutoTimestamp)
				{
					Timestamp = TimeHelper.GetCurrentTimestampSeconds();
				}
			} else {
				Timestamp = customEventTimestamp;
			}
			param.Add(new KeyValuePair<string, string>("Timestamp", 
				Timestamp.ToString()));

			param.Add(new KeyValuePair<string, string>("UniqueCustomerSubToken",
				UniqueCustomerSubToken));

			return param;
		} // GetParameterList()
		#endregion
	} // class Config
} // namespace HoneyTracks
