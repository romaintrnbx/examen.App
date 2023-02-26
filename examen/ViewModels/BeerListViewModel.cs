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
		private ObservableCollection<Beer> beers;

		public ObservableCollection<Beer> Beers
		{
			get => beers;
			set => SetProperty(ref beers, value);
		}

		public BeerListViewModel()
		{
			Beers = new ObservableCollection<Beer>();
			LoadBeers().ConfigureAwait(false);
		}

		private async Task LoadBeers()
		{
			var viewbeers = await Beer.LoadBeers();
			Beers = new ObservableCollection<Beer>(viewbeers);
		}

		public async Task RefreshBeers() => await LoadBeers();
	}
}