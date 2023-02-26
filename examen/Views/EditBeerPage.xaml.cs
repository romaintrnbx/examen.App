using examen.ViewModels;
using examen.Models;
using Microsoft.Maui.Controls;

namespace examen.Views
{
	public partial class EditBeerPage : ContentPage
	{
		public EditBeerPage(Beer selectedBeer)
		{
			InitializeComponent();
			BindingContext = new EditBeerViewModel(selectedBeer);
		}
	}
}