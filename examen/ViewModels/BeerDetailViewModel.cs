using System.ComponentModel;
using System.Runtime.CompilerServices;
using examen.Models;

namespace examen.ViewModels
{
	public class BeerDetailViewModel : INotifyPropertyChanged
	{
		private Beer _selectedBeer;

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

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}