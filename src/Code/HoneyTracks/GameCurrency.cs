// Project: HoneyTracks
using System;
using System.Collections.Generic;
using System.Text;

namespace HoneyTracks
{
	/// <summary>
	/// Game currency
	/// </summary>
	public class GameCurrency
	{
		#region Private
		/// <summary>
		/// List of game currency
		/// </summary>
		private List<KeyValuePair<string, string>> gameCurrencies;
		#endregion

		#region Constructor
		/// <summary>
		/// Create game currency
		/// </summary>
		public GameCurrency()
		{
			gameCurrencies = new List<KeyValuePair<string, string>>();
		} // GameCurrency()
		#endregion

		#region Add
		/// <summary>
		/// Add game currency
		/// </summary>
		/// <param name="resource">resources name</param>
		/// <param name="value">amount of resources as string</param>
		public void Add(string resource, string value)
		{
			Add(new KeyValuePair<string,string>(resource, value));
		} // Add(resource, value)

		/// <summary>
		/// Add game currency
		/// </summary>
		/// <param name="gameCurrency">key = resources name, 
		/// value = amount of resources as string</param>
		public void Add(KeyValuePair<string, string> gameCurrency)
		{
			gameCurrencies.Add(gameCurrency);
		} // Add(gameCurrency)
		#endregion

		#region GetParameterList
		/// <summary>
		/// Get parameter list
		/// </summary>
		internal List<KeyValuePair<string, string>> GetParameterList()
		{
			return gameCurrencies;
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
			return List2Json.Encode(this.gameCurrencies);
		} // ToJsonString()
		#endregion
	} // class GameCurrency
} // namespace HoneyTracks
