using examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen.ViewModels
{
	internal class EditBeerViewModel
	{
		private Beer beer;

		public EditBeerViewModel(Beer beer)
		{
			this.beer = beer;
		}
	}
}