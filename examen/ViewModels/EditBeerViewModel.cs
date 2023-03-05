using examen.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace examen.ViewModels
{
	internal class EditBeerViewModel : INotifyPropertyChanged
	{
		private Beer _selectedBeer;
		private ObservableCollection<StylesBeer> styles = new ObservableCollection<StylesBeer>();
		private StylesBeer styleName;
		private StylesBeer _selectedStyle;
		private ObservableCollection<GlassBeer> glasses = new ObservableCollection<GlassBeer>();
		private GlassBeer glassName;
		private GlassBeer _selectedGlass;
		private ObservableCollection<FermentationsBeer> fermentations = new ObservableCollection<FermentationsBeer>();
		private FermentationsBeer fermentationName;
		private FermentationsBeer _selectedFerm;
		private ObservableCollection<FormatsBeer> formats = new ObservableCollection<FormatsBeer>();
		private readonly HttpClient httpClient;
		private bool isBusy;

		public Beer SelectedBeer
		{
			get { return _selectedBeer; }
			set
			{
				if (_selectedBeer != value)
				{
					_selectedBeer = value;
					OnPropertyChanged();
				}
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
			SelectedStyle = Styles.FirstOrDefault(styles => styles.Name == SelectedBeer.StyleName);
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

		public StylesBeer SelectedStyle
		{
			get { return _selectedStyle; }
			set
			{
				if (_selectedStyle != value)
				{
					_selectedStyle = value;
					OnPropertyChanged();
					SelectedBeer.StyleName = value.Name;
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
			SelectedGlass = Glasses.FirstOrDefault(glasses => glasses.Name == SelectedBeer.GlassName);
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

		public GlassBeer SelectedGlass
		{
			get { return _selectedGlass; }
			set
			{
				if (_selectedGlass != value)
				{
					_selectedGlass = value;
					OnPropertyChanged();
					SelectedBeer.GlassName = value.Name;
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
			SelectedFerm = Fermentations.FirstOrDefault(ferm => ferm.Name == SelectedBeer.FermentationName);
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

		public FermentationsBeer SelectedFerm
		{
			get { return _selectedFerm; }
			set
			{
				if (_selectedFerm != value)
				{
					_selectedFerm = value;
					OnPropertyChanged();
					SelectedBeer.FermentationName = value.Name;
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

		public ObservableCollection<FormatsBeer> SelectedFormats { get; private set; }

		private async Task LoadFormats()
		{
			var viewformats = await FormatsBeer.LoadFormats();
			Formats = new ObservableCollection<FormatsBeer>(viewformats);
			string[] formatSplit = SelectedBeer.Formats.Split(',');

			foreach (var format in Formats)
			{
				if (formatSplit.Any(form => form == format.Name))
				{
					format.IsSelected = true;
					SelectedFormats.Add(format);
				}
				else
				{
					format.IsSelected = false;
					SelectedFormats.Add(format);
				}
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

		public ICommand UpdateBeerCommand { get; set; }

		public EditBeerViewModel(Beer selectedBeer)
		{
			httpClient = new HttpClient();
			UpdateBeerCommand = new Command(async () => await UpdateBeer());
			SelectedBeer = selectedBeer;
			Styles = new ObservableCollection<StylesBeer>();
			LoadStyles().ConfigureAwait(false);
			Glasses = new ObservableCollection<GlassBeer>();
			LoadGlasses().ConfigureAwait(false);
			Fermentations = new ObservableCollection<FermentationsBeer>();
			LoadFermentations().ConfigureAwait(false);
			SelectedFormats = new ObservableCollection<FormatsBeer>();
			LoadFormats().ConfigureAwait(false);
		}

		private async Task UpdateBeer()
		{
			IsBusy = true;
			var selectedFormats = Formats.Where(f => f.IsSelected).ToList();
			var formatsString = string.Join(",", selectedFormats.Select(f => f.Id));

			if (string.IsNullOrWhiteSpace(SelectedBeer.Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Entrer un nom de bière", "OK");
				IsBusy = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(SelectedStyle.Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Sélectionner un style de bière", "OK");
				IsBusy = false;
				return;
			}
			if (string.IsNullOrWhiteSpace(SelectedGlass.Name))
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Sélectionner un verre", "OK");
				IsBusy = false;
				return;
			}

			var newBeer = new Beer
			{
				Id = SelectedBeer.Id,
				Name = SelectedBeer.Name,
				BreweryName = SelectedBeer.BreweryName,
				Description = SelectedBeer.Description,
				Alcool = SelectedBeer.Alcool,
				IBU = SelectedBeer.IBU,
				EBC = SelectedBeer.EBC,
				StyleName = SelectedStyle.Name,
				GlassName = SelectedGlass.Name,
				FermentationName = SelectedFerm.Name,
				Formats = formatsString,
				CreatedAt = SelectedBeer.CreatedAt
			};

			try
			{
				await newBeer.UpdateBeer(newBeer);
				await Application.Current.MainPage.DisplayAlert("Success", "La bière a bien été modifié", "OK");
				await Application.Current.MainPage.Navigation.PopToRootAsync();

				SelectedBeer.Id = 0;
				SelectedBeer.Name = string.Empty;
				SelectedBeer.Description = string.Empty;
				SelectedBeer.Alcool = 0;
				SelectedBeer.IBU = 0;
				SelectedBeer.EBC = 0;
				SelectedStyle.Name = string.Empty;
				SelectedGlass.Name = string.Empty;
				SelectedFerm.Name = string.Empty;
				SelectedBeer.CreatedAt = 0;
				SelectedBeer.BreweryName = string.Empty;
			}
			catch (Exception)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "La bière n'a pas été modifiée, recommencez.", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}