using examen.ViewModels;
using Microsoft.Maui.Controls;


namespace examen.Views
{
	public partial class AddBeerPage : ContentPage
	{
		public AddBeerPage()
		{
			InitializeComponent();
			BindingContext = new AddBeerViewModel();
		}
	}
}