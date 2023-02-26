using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen.Models
{
	public class FermentationsBeer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		public static async Task<List<FermentationsBeer>> LoadFermentations()
		{
			try
			{
				var httpClient = new HttpClient();
				var json = await httpClient.GetAsync("http://databasebeer/controller/fermentations_JSON.php");

				if (json.IsSuccessStatusCode)
				{
					var content = await json.Content.ReadAsStringAsync();
					var fermentations = JsonConvert.DeserializeObject<List<FermentationsBeer>>(content);
					return fermentations;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors du chargement des fermentations {ex}");
			}

			return new List<FermentationsBeer>();
		}
	}
}