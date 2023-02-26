using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace examen.Models
{
	public class FormatsBeer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		public bool IsSelected { get; set; }

		public static async Task<List<FormatsBeer>> LoadFormats()
		{
			try
			{
				var httpClient = new HttpClient();
				var json = await httpClient.GetAsync("http://databasebeer/controller/formats_JSON.php");

				if (json.IsSuccessStatusCode)
				{
					var content = await json.Content.ReadAsStringAsync();
					var formats = JsonConvert.DeserializeObject<List<FormatsBeer>>(content);
					return formats;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors du chargement des formats {ex}");
			}

			return new List<FormatsBeer>();
		}
	}
}