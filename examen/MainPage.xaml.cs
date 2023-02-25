﻿using examen.ViewModels;
using examen.Models;
using examen.Views;

namespace examen;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new BeerListViewModel();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		var viewModel = (BeerListViewModel)BindingContext;
		viewModel.RefreshBeers();
	}

	private void OnRefreshClicked(object sender, EventArgs e)
	{
		var viewModel = (BeerListViewModel)BindingContext;
		viewModel.RefreshBeers();
	}

	private void OnBeerSelected(object sender, EventArgs e)
	{
		var beer = (Beer)((Button)sender).CommandParameter;
		Navigation.PushAsync(new BeerDetailPage(beer));
	}

	private async void OnAddBeerClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddBeerPage());
	}
}