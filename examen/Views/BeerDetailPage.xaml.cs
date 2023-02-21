using Microsoft.EntityFrameworkCore.Metadata.Internal;
using examen.ViewModels;
using examen.Models;

namespace examen.Views;

public partial class BeerDetailPage : ContentPage
{
	public BeerDetailPage(Beer selectedBeer)
	{
		InitializeComponent();
		BindingContext = selectedBeer;
		Title = selectedBeer.Name;
		// Add labels to display other beer details
		// ...
	}
}