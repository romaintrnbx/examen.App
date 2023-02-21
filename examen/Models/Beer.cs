using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace examen.Models
{
	public class Beer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("alcool")]
		public double Alcool { get; set; }

		[JsonProperty("ibu")]
		public int IBU { get; set; }

		[JsonProperty("ebc")]
		public int EBC { get; set; }

		[JsonProperty("style_name")]
		public string StyleName { get; set; }

		[JsonProperty("glass_name")]
		public string GlassName { get; set; }

		[JsonProperty("fermentation_name")]
		public string FermentationName { get; set; }

		[JsonProperty("picture")]
		public string Picture { get; set; }

		[JsonProperty("created_at")]
		public int CreatedAt { get; set; }

		[JsonProperty("brewery_name")]
		public string BreweryName { get; set; }

		[JsonProperty("formats")]
		public string Formats { get; set; }

		[JsonIgnore]
		public string DisplayPicture => $"C:/wamp64/www/Beer_Time{Picture.Substring(2)}";

	}

}