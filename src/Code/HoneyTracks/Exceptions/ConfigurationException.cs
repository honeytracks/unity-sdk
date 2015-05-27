// Project: HoneyTracks
using System;
using System.Collections.Generic;
using System.Text;

namespace HoneyTracks.Exceptions
{
	/// <summary>
	/// Configuration exception
	/// </summary>
	public class ConfigurationException : Exception
	{
		#region Constructor
		/// <summary>
		/// Create configuration exception
		/// </summary>
		public ConfigurationException()
			: base()
		{
		} // ConfigurationException()

		/// <summary>
		/// Create configuration exception
		/// </summary>
		public ConfigurationException(string message)
			: base(message)
		{
		} // ConfigurationException(message)

		/// <summary>
		/// Create configuration exception
		/// </summary>
		public ConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		} // ConfigurationException(message, innerException)

		#endregion
	} // class ConfigurationException
} // namespace HoneyTracks.Exceptions
