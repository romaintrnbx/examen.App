using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace examen.Models
{
	public class Beer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("alcool")]
		public double Alcool { get; set; }

		[JsonProperty("ibu")]
		public int IBU { get; set; }

		[JsonProperty("ebc")]
		public int EBC { get; set; }

		[JsonProperty("style_name")]
		public string StyleName { get; set; }

		[JsonProperty("glass_name")]
		public string GlassName { get; set; }

		[JsonProperty("fermentation_name")]
		public string FermentationName { get; set; }

		[JsonProperty("picture")]
		public string Picture { get; set; }

		[JsonProperty("created_at")]
		public int CreatedAt { get; set; }

		[JsonProperty("brewery_name")]
		public string BreweryName { get; set; }

		[JsonProperty("formats")]
		public string Formats { get; set; }

		[JsonIgnore]
		public string DisplayPicture => $"C:/wamp64/www/Beer_Time{Picture.Substring(2)}";

		public static async Task<List<Beer>> LoadBeers()
		{
			try
			{
				var httpClient = new HttpClient();
				var response = await httpClient.GetAsync("http://beertime/controller/JSON/beer_JSON.php");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var beers = JsonConvert.DeserializeObject<List<Beer>>(content);
					return beers;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors du chargement des bières {ex}");
			}

			return new List<Beer>();
		}

		public async Task AddBeer(Beer newBeer)
		{
			var values = new Dictionary<string, object>
			{
				{ "name", newBeer.Name },
				{ "brewery_name", newBeer.BreweryName },
				{ "description", newBeer.Description },
				{ "alcool", newBeer.Alcool },
				{ "ibu", newBeer.IBU },
				{ "ebc", newBeer.EBC },
				{ "styleName", newBeer.StyleName },
				{ "glassName", newBeer.GlassName },
				{ "fermentationName", newBeer.FermentationName },
				{ "formats", newBeer.Formats },
				{ "createdAt", newBeer.CreatedAt }
			};

			var httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(values);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://databasebeer/controller/addBeer_JSON.php", content);

			if (response.IsSuccessStatusCode)
			{
				var responseJson = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<Beer>(responseJson);
				await Task.FromResult(result);
			}
		}

		public static async Task<bool> DeleteBeer(int beerId)
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = null;
			response = await httpClient.DeleteAsync($"http://databasebeer/controller/deleteBeer_JSON.php?id={beerId}");
			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public async Task DeleteBeer()
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = null;
			response = await httpClient.DeleteAsync($"http://databasebeer/controller/deleteBeer_JSON.php?id={Id}");
			if (response.IsSuccessStatusCode)
			{
				await Application.Current.MainPage.DisplayAlert("Success", "La bière a bien été supprimée", "OK");
				await Application.Current.MainPage.Navigation.PopToRootAsync();
			}
			else
			{
				await Application.Current.MainPage.DisplayAlert("Error", "La bière n'a pas été supprimée", "OK");
			}
		}
	}
}