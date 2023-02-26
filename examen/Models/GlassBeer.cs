using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace examen.Models
{
	public class GlassBeer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		public static async Task<List<GlassBeer>> LoadGlasses()
		{
			try
			{
				var httpClient = new HttpClient();
				var json = await httpClient.GetAsync("http://databasebeer/controller/glasses_JSON.php");

				if (json.IsSuccessStatusCode)
				{
					var content = await json.Content.ReadAsStringAsync();
					var glasses = JsonConvert.DeserializeObject<List<GlassBeer>>(content);
					return glasses;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors du chargement des verres {ex}");
			}

			return new List<GlassBeer>();
		}
	}
}