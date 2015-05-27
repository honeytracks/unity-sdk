// Project: HoneyTracks
using System;
using System.Collections.Generic;
using System.Text;

namespace HoneyTracks
{
	/// <summary>
	/// List 2 json
	/// </summary>
	internal class List2Json
	{
		/// <summary>
		/// simple encoding for list 2 json to avoid huge dependencies
		/// </summary>
		public static string Encode(List<KeyValuePair<string, string>> value)
		{
			StringBuilder result = new StringBuilder();

			result.Append("{");

			for (int entryId = 0; entryId < value.Count; entryId++)
			{
				KeyValuePair<string, string> entry = value[entryId];
				result.Append("\"").Append(entry.Key).Append("\":\"").Append(
					entry.Value.ToString().Replace("\"", "\\\"")).Append("\"");
				if (entryId + 1 < value.Count)
				{
					result.Append(",");
				}
			} // for

			result.Append("}");

			return result.ToString();
		} // Encode(value)
	} // class List2Json
} // namespace HoneyTracks
