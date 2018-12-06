using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
	//represent all of the data for the Add() view - both displaying form and processing form
	public class AddCheeseViewModel
	{
		//information received from the form:
		[Required]
		[Display(Name = "Cheese Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "You must give your cheese a description")]
		public string Description { get; set; }
	}
}
