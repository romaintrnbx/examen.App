using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using examen.Models;
using examen.Views;

namespace examen.ViewModels
{
	public class BeerDetailViewModel : INotifyPropertyChanged
	{
		private Beer _selectedBeer;
		public ICommand DeleteBeerCommand { get; set; }
		public ICommand EditBeerCommand { get; set; }

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

		public BeerDetailViewModel()
		{
			DeleteBeerCommand = new Command(async () =>
			{
				await SelectedBeer.DeleteBeer();
			});
			EditBeerCommand = new Command(async () =>
			{
				await Application.Current.MainPage.Navigation.PushAsync(new EditBeerPage(SelectedBeer));
			});
		}

		public async Task DeleteBeer()
		{
			var isDeleted = await Beer.DeleteBeer(SelectedBeer.Id);
			if (isDeleted)
			{
				await Application.Current.MainPage.DisplayAlert("Success", "La bière a bien été supprimée", "OK");
				await Application.Current.MainPage.Navigation.PopToRootAsync();
			}
			else
			{
				await Application.Current.MainPage.DisplayAlert("Error", "La bière n'a pas été supprimée", "OK");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}