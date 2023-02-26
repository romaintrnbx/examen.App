using System;
using examen.Models;
using examen.ViewModels;
using Microsoft.Maui.Controls;

namespace examen.Views
{
	public partial class EditBeerPage : ContentPage
	{
		public EditBeerPage(Beer beer)
		{
			InitializeComponent();
			BindingContext = new EditBeerViewModel(beer);
		}
	}
}