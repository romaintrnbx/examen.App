using Microsoft.EntityFrameworkCore.Metadata.Internal;
using examen.ViewModels;
using examen.Models;

namespace examen.Views;

public partial class BeerDetailPage : ContentPage
{
	private readonly BeerDetailViewModel viewModel;

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
			await viewModel.DeleteBeer();
		}
	}

	private async void OnEditButtonClicked(object sender, EventArgs e)
	{
		var editBeerPage = new EditBeerPage((Beer)BindingContext);
		await Navigation.PushAsync(editBeerPage);
	}
}