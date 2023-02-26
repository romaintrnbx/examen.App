using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace examen.Models
{
	public class StylesBeer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		public static async Task<List<StylesBeer>> LoadStyles()
		{
			try
			{
				var httpClient = new HttpClient();
				var json = await httpClient.GetAsync("http://databasebeer/controller/styles_JSON.php");

				if (json.IsSuccessStatusCode)
				{
					var content = await json.Content.ReadAsStringAsync();
					var styles = JsonConvert.DeserializeObject<List<StylesBeer>>(content);
					return styles;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors du chargement des styles {ex}");
			}

			return new List<StylesBeer>();
		}
	}
}