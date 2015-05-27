// Project: HoneyTracks
using System;
using System.Collections.Generic;
using System.Text;

namespace HoneyTracks.Helper
{
	/// <summary>
	/// Time helper
	/// </summary>
	public class TimeHelper
	{
		#region Private
		/// <summary>
		/// Date time of 01. jan 1970
		/// </summary>
		private static DateTime jan1970 = 
			new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		#endregion

		#region GetCurrentTimestampMilliseconds
		/// <summary>
		/// Return the current timestamp in milliseconds.
		/// </summary>
		public static long GetCurrentTimestampMilliseconds()
		{
			//NOTE: This works only for .NET, java has System.currentTimeMillis()
			TimeSpan span = DateTime.UtcNow - jan1970;
			return (long)span.TotalMilliseconds;
		} // GetCurrentTimestampMilliseconds()
		#endregion

		#region GetCurrentTimestampSeconds
		/// <summary>
		/// Return the current timestamp in seconds.
		/// </summary>
		public static long GetCurrentTimestampSeconds()
		{
			TimeSpan span = DateTime.UtcNow - jan1970;
			return (long)span.TotalSeconds;
		} // GetCurrentTimestampSeconds()
		#endregion
	} // class TimeHelper
} // namespace HoneyTracks.Helper
