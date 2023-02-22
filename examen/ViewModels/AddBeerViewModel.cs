using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using examen.Models;
using Newtonsoft.Json;

namespace examen.ViewModels
{
	public class AddBeerViewModel : BaseViewModel
	{
		private string name;

		public string Name
		{
			get { return name; }
			set { SetProperty(ref name, value); }
		}

		private string description;

		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

		private double alcool;

		public double Alcool
		{
			get { return alcool; }
			set { SetProperty(ref alcool, value); }
		}

		private int ibu;

		public int IBU
		{
			get { return ibu; }
			set { SetProperty(ref ibu, value); }
		}

		private int ebc;

		public int EBC
		{
			get { return ebc; }
			set { SetProperty(ref ebc, value); }
		}

		private string styleName;

		public string StyleName
		{
			get { return styleName; }
			set { SetProperty(ref styleName, value); }
		}

		private string glassName;

		public string GlassName
		{
			get { return glassName; }
			set { SetProperty(ref glassName, value); }
		}

		private string fermentationName;

		public string FermentationName
		{
			get { return fermentationName; }
			set { SetProperty(ref fermentationName, value); }
		}

		private string picture;

		public string Picture
		{
			get { return picture; }
			set { SetProperty(ref picture, value); }
		}

		private DateTime createdAt;

		public DateTime CreatedAt
		{
			get { return createdAt; }
			set { SetProperty(ref createdAt, value); }
		}

		private string breweryName;

		public string BreweryName
		{
			get { return breweryName; }
			set { SetProperty(ref breweryName, value); }
		}

		private ObservableCollection<StylesBeer> styles;

		public ObservableCollection<StylesBeer> Styles
		{
			get { return styles; }
			set { SetProperty(ref styles, value); }
		}

		private bool isBusy;

		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		private readonly HttpClient _httpClient;

		public AddBeerViewModel()
		{
			_httpClient = new HttpClient();
			Styles = new ObservableCollection<StylesBeer>();
			LoadStyles().ConfigureAwait(false);
			LoadGlasses().ConfigureAwait(false);
			LoadFermentations().ConfigureAwait(false);
			LoadFormats().ConfigureAwait(false);
		}

		private async Task LoadStyles()
		{
			IsBusy = true;

			var response = await _httpClient.GetAsync("http://beertime/controller/JSON/styles_JSON.php");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var loadedStyles = JsonConvert.DeserializeObject<List<StylesBeer>>(content);
				Styles = new ObservableCollection<StylesBeer>(loadedStyles);
			}

			IsBusy = false;
		}

		private ObservableCollection<GlassBeer> glasses;

		public ObservableCollection<GlassBeer> Glasses
		{
			get { return glasses; }
			set { SetProperty(ref glasses, value); }
		}

		private async Task LoadGlasses()
		{
			IsBusy = true;

			var response = await _httpClient.GetAsync("http://beertime/controller/JSON/glasses_JSON.php");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var loadedGlasses = JsonConvert.DeserializeObject<List<GlassBeer>>(content);
				Glasses = new ObservableCollection<GlassBeer>(loadedGlasses);
			}

			IsBusy = false;
		}

		private ObservableCollection<FermentationsBeer> fermentations;

		public ObservableCollection<FermentationsBeer> Fermentations
		{
			get { return fermentations; }
			set { SetProperty(ref fermentations, value); }
		}

		private async Task LoadFermentations()
		{
			IsBusy = true;

			var response = await _httpClient.GetAsync("http://beertime/controller/JSON/fermentations_JSON.php");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var loadedFermentations = JsonConvert.DeserializeObject<List<FermentationsBeer>>(content);
				Fermentations = new ObservableCollection<FermentationsBeer>(loadedFermentations);
			}
			IsBusy = false;
		}

		private ObservableCollection<FormatsBeer> _formats;

		public ObservableCollection<FormatsBeer> Formats
		{
			get { return _formats; }
			set { SetProperty(ref _formats, value); }
		}

		private async Task LoadFormats()
		{
			IsBusy = true;

			var response = await _httpClient.GetAsync("http://beertime/controller/JSON/formats_JSON.php");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var loadedFormats = JsonConvert.DeserializeObject<List<FormatsBeer>>(content);
				Formats = new ObservableCollection<FormatsBeer>(loadedFormats);
			}

			IsBusy = false;
		}
	}
}