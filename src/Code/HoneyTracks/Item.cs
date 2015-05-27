// Project: HoneyTracks

using System.Collections.Generic;
namespace HoneyTracks
{
	/// <summary>
	/// Item
	/// </summary>
	public class Item
	{
		#region Public
		/// <summary>
		/// Unique id
		/// </summary>
		public string UniqueId
		{
			get;
			set;
		} // UniqueId

		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			get;
			set;
		} // Name

		/// <summary>
		/// Image url
		/// </summary>
		public string ImageUrl
		{
			get;
			set;
		} // ImageUrl
		#endregion

		#region Constructor
		/// <summary>
		/// Create item
		/// </summary>
		public Item(string uniqueId, string name, string imageUrl)
		{
			UniqueId = uniqueId;
			Name = name;
			ImageUrl = imageUrl;
		} // Item(uniqueId, name, imageUrl)
		#endregion

		#region GetParameterList
		/// <summary>
		/// Get parameter list
		/// </summary>
		internal List<KeyValuePair<string, string>> GetParameterList()
		{
			List<KeyValuePair<string, string>> param =
				new List<KeyValuePair<string, string>>();

			param.Add(new KeyValuePair<string, string>("UniqueId",
				UniqueId));
			param.Add(new KeyValuePair<string, string>("ImageUrl",
				ImageUrl));
			param.Add(new KeyValuePair<string, string>("Name",
				Name));

			return param;
		} // GetParameterList()
		#endregion

		#region ToJsonString
		/// <summary>
		/// Returns a System.String that represents the game currency as json string.
		/// </summary>
		/// <returns>A System.String that represents the game currency as json 
		/// string</returns>
		internal string ToJsonString()
		{
			return List2Json.Encode(GetParameterList());
		} // ToJsonString()
		#endregion
	} // class Item
} // namespace HoneyTracks
