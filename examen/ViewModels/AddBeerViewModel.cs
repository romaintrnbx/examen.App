using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using examen.Models;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace examen.ViewModels
{
	public class AddBeerViewModel : INotifyPropertyChanged
	{
		private string name;
		private string breweryName;
		private string description;
		private float alcool;
		private int ibu;
		private int ebc;
		private ObservableCollection<StylesBeer> styles = new ObservableCollection<StylesBeer>();
		private StylesBeer styleName;
		private ObservableCollection<GlassBeer> glasses = new ObservableCollection<GlassBeer>();
		private GlassBeer glassName;
		private ObservableCollection<FermentationsBeer> fermentations = new ObservableCollection<FermentationsBeer>();
		private FermentationsBeer fermentationName;
		private ObservableCollection<FormatsBeer> formats = new ObservableCollection<FormatsBeer>();
		private int createdAt;
		private bool isBusy;
		private readonly HttpClient httpClient;

		public string Name
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string BreweryName
		{
			get => breweryName;
			set
			{
				breweryName = value;
				OnPropertyChanged(nameof(BreweryName));
			}
		}

		public string Description
		{
			get => description;
			set
			{
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		public float Alcool
		{
			get => alcool;
			set
			{
				alcool = value;
				OnPropertyChanged(nameof(Alcool));
			}
		}

		public int IBU
		{
			get => ibu;
			set
			{
				ibu = value;
				OnPropertyChanged(nameof(IBU));
			}
		}

		public int EBC
		{
			get => ebc;
			set
			{
				ebc = value;
				OnPropertyChanged(nameof(EBC));
			}
		}

		public ObservableCollection<StylesBeer> Styles
		{
			get => styles;
			set
			{
				styles = value;
				OnPropertyChanged(nameof(Styles));
			}
		}

		private async Task LoadStyles()
		{
			var viewstyles = await StylesBeer.LoadStyles();
			Styles = new ObservableCollection<StylesBeer>(viewstyles);
		}

		public StylesBeer StyleName
		{
			get { return styleName; }
			set
			{
				if (styleName != value)
				{
					styleName = value;
					OnPropertyChanged(nameof(StyleName));
				}
			}
		}

		public ObservableCollection<GlassBeer> Glasses
		{
			get => glasses;
			set
			{
				glasses = value;
				OnPropertyChanged(nameof(Glasses));
			}
		}

		private async Task LoadGlasses()
		{
			var viewglasses = await GlassBeer.LoadGlasses();
			Glasses = new ObservableCollection<GlassBeer>(viewglasses);
		}

		public GlassBeer GlassName
		{
			get { return glassName; }
			set
			{
				if (glassName != value)
				{
					glassName = value;
					OnPropertyChanged(nameof(GlassName));
				}
			}
		}

		public ObservableCollection<FermentationsBeer> Fermentations
		{
			get => fermentations;
			set
			{
				fermentations = value;
				OnPropertyChanged(nameof(Fermentations));
			}
		}

		private async Task LoadFermentations()
		{
			var viewfermentations = await FermentationsBeer.LoadFermentations();
			Fermentations = new ObservableCollection<FermentationsBeer>(viewfermentations);
		}

		public FermentationsBeer FermentationName
		{
			get { return fermentationName; }
			set
			{
				if (fermentationName != value)
				{
					fermentationName = value;
					OnPropertyChanged(nameof(FermentationName));
				}
			}
		}

		public ObservableCollection<FormatsBeer> Formats
		{
			get => formats;
			set
			{
				formats = value;
				OnPropertyChanged(nameof(Formats));
			}
		}

		private async Task LoadFormats()
		{
			var viewformats = await FormatsBeer.LoadFormats();
			Formats = new ObservableCollection<FormatsBeer>(viewformats);
		}

		public int CreatedAt
		{
			get => createdAt;
			set
			{
				createdAt = value;
				OnPropertyChanged(nameof(CreatedAt));
			}
		}

		public bool IsBusy
		{
			get => isBusy;
			set
			{
				isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
			}
		}

		public ICommand AddBeerCommand { get; set; }

		public AddBeerViewModel()
		{
			httpClient = new HttpClient();
			AddBeerCommand = new Command(async () => await AddBeer());
			Name = "";
			BreweryName = "";
			Description = "";
			Alcool = 0;
			IBU = 0;
			EBC = 0;
			Styles = new ObservableCollection<StylesBeer>();
			LoadStyles().ConfigureAwait(false);
			Glasses = new ObservableCollection<GlassBeer>();
			LoadGlasses().ConfigureAwait(false);
			Fermentations = new ObservableCollection<FermentationsBeer>();
			LoadFermentations().ConfigureAwait(false);
			Formats = new ObservableCollection<FormatsBeer>();
			LoadFormats().ConfigureAwait(false);
			foreach (FormatsBeer format in Formats)
			{
				format.IsSelected = false;
			}
			CreatedAt = 1900;
		}

		private async Task AddBeer()
		{
			IsBusy = true;
			var selectedFormats = Formats.Where(f => f.IsSelected).ToList();
			var formatsString = string.Join(",", selectedFormats.Select(f => f.Id));

			if (string.IsNullOrWhiteSpace(Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Entrer un nom de bière", "OK");
				IsBusy = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(StyleName.Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Sélectionner un style de bière", "OK");
				IsBusy = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(GlassName.Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Sélectionner un verre", "OK");
				IsBusy = false;
				return;
			}

			var newBeer = new Beer
			{
				Name = Name,
				BreweryName = BreweryName,
				Description = Description,
				Alcool = Alcool,
				IBU = IBU,
				EBC = EBC,
				StyleName = StyleName.Name,
				GlassName = GlassName.Name,
				FermentationName = FermentationName.Name,
				Formats = formatsString,
				CreatedAt = CreatedAt
			};

			try
			{
				await newBeer.AddBeer(newBeer);
				await Application.Current.MainPage.DisplayAlert("Success", "La bière a bien été ajoutée", "OK");
				await Application.Current.MainPage.Navigation.PopToRootAsync();

				Name = string.Empty;
				Description = string.Empty;
				Alcool = 0;
				IBU = 0;
				EBC = 0;
				StyleName.Name = string.Empty;
				GlassName.Name = string.Empty;
				FermentationName.Name = string.Empty;
				CreatedAt = 0;
				BreweryName = string.Empty;
			}
			catch (Exception)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "La bière n'a pas été ajoutée, recommencez.", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}