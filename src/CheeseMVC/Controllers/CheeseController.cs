using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
	public class CheeseController : Controller
	{

		// GET: /<controller>/
		public IActionResult Index()
		{
			List<Cheese> cheeses= CheeseData.GetAll();

			return View(cheeses);
		}

		public IActionResult Add()
		{
			AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
			return View();
		}

		[HttpPost]
		public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
		{

			if (ModelState.IsValid)
			{
				Cheese newCheese = new Cheese
				{
					Name = addCheeseViewModel.Name,
					Description = addCheeseViewModel.Description
				};

				// Add the new cheese to my existing cheeses
				CheeseData.Add(newCheese);

				return Redirect("/Cheese");
			}

			return View(addCheeseViewModel);
		}

		public IActionResult Remove()
		{
			ViewBag.title = "Remove Cheeses";
			ViewBag.cheeses = CheeseData.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult Remove(int[] cheeseIds)
		{
			//TODO- remove cheeses from list
			foreach (int cheeseId in cheeseIds)
			{
				CheeseData.Remove(cheeseId);
			}
			return Redirect("/");
		}

		public IActionResult Edit(int cheeseId)
		{
			ViewBag.cheeseToEdit = CheeseData.GetById(cheeseId);
			return View();
		}

		[HttpPost]
		public IActionResult Edit(int cheeseId, string name, string description)
		{
			Cheese cheeseToUpdate = CheeseData.GetById(cheeseId);
			cheeseToUpdate.Name = name;
			cheeseToUpdate.Description = description;
			return Redirect("/");
		}
	}
}
