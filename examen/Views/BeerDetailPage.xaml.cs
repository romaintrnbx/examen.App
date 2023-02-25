using Microsoft.EntityFrameworkCore.Metadata.Internal;
using examen.ViewModels;
using examen.Models;

namespace examen.Views;

public partial class BeerDetailPage : ContentPage
{
	private BeerDetailViewModel viewModel;

	public BeerDetailPage(Beer selectedBeer)
	{
		InitializeComponent();
		viewModel = new BeerDetailViewModel();
		viewModel.SelectedBeer = selectedBeer;
		BindingContext = selectedBeer;
		Title = selectedBeer.Name;
	}

	private async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		bool answer = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette bière ?", "Oui", "Non");
		if (answer)
		{
			viewModel.DeleteBeer();
		}
	}
}