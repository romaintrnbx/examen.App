using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using examen.Models;
using Newtonsoft.Json;

namespace examen.ViewModels
{
	public class BeerListViewModel : BaseViewModel
	{
		private ObservableCollection<Beer> _beers;

		public ObservableCollection<Beer> Beers
		{
			get => _beers;
			set => SetProperty(ref _beers, value);
		}

		public BeerListViewModel()
		{
			Beers = new ObservableCollection<Beer>();
			LoadBeers().ConfigureAwait(false);
		}

		private async Task LoadBeers()
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync("http://beertime/controller/JSON/beer_JSON.php");
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var beers = JsonConvert.DeserializeObject<ObservableCollection<Beer>>(content);
					Beers = beers;
				}
			}
		}

		public async void RefreshBeers()
		{
			await LoadBeers();
		}
	}
}