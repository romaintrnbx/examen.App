using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using examen.Models;

namespace examen.ViewModels
{
	public class BeerDetailViewModel : INotifyPropertyChanged
	{
		private Beer _selectedBeer;
		public ICommand DeleteBeerCommand { get; set; }

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
			DeleteBeerCommand = new Command(DeleteBeer);
		}

		public async void DeleteBeer()
		{
			var httpClient = new HttpClient();
			HttpResponseMessage response = null;
			response = await httpClient.DeleteAsync($"http://databasebeer/controller/deleteBeer_JSON.php?id={SelectedBeer.Id}");
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

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}