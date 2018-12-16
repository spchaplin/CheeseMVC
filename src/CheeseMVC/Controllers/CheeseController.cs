using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CheeseMVC.Controllers
{
	public class CheeseController : Controller
	{
		private CheeseDbContext context;

		public CheeseController(CheeseDbContext dbContext)
		{
			context = dbContext;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			// List<Cheese> cheeses = context.Cheeses.ToList();
			IList<Cheese> cheeses = context.Cheeses.Include(context => context.Category).ToList();

			return View(cheeses);
		}

		public IActionResult Add()
		{
			AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
			return View(addCheeseViewModel);
		}

		[HttpPost]
		public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
		{
			if (ModelState.IsValid)
			{

				CheeseCategory newCheeseCategory =
					context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

				// Add the new cheese to my existing cheeses
				Cheese newCheese = new Cheese
				{
					Name = addCheeseViewModel.Name,
					Description = addCheeseViewModel.Description,
					Category = newCheeseCategory
					//Type = addCheeseViewModel.Type
				};

				context.Cheeses.Add(newCheese);
				context.SaveChanges();

				return Redirect("/Cheese");
			}

			return View(addCheeseViewModel);
		}

		public IActionResult Remove()
		{
			ViewBag.title = "Remove Cheeses";
			ViewBag.cheeses = context.Cheeses.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Remove(int[] cheeseIds)
		{
			foreach (int cheeseId in cheeseIds)
			{

				Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
				context.Cheeses.Remove(theCheese);
				//CheeseData.Remove(cheeseId);
			}

			context.SaveChanges();

			return Redirect("/");
		}

		public IActionResult Category(int id)
		{
			if (id == 0)
			{
				return Redirect("/Category");
			}

			CheeseCategory theCategory = context.Categories.Include(cat => cat.Cheeses).Single(cat => cat.ID == id);

			ViewBag.title = "Cheeses in category: " + theCategory.Name;

			return View("Index", theCategory.Cheeses);
		}
	}
}
