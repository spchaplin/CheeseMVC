using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
	public class MenuController : Controller
	{
		private readonly CheeseDbContext context;

		public MenuController(CheeseDbContext dbContext)
		{
			this.context = dbContext;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{

			IList<Menu> menus = context.Menus.Include(context => context.Name).ToList();

			return View(menus);

			/*
			 	// GET: /<controller>/
		public IActionResult Index()
		{
			// List<Cheese> cheeses = context.Cheeses.ToList();
			IList<Cheese> cheeses = context.Cheeses.Include(context => context.Category).ToList();

			return View(cheeses);
		}
			 */

		}

		[HttpGet]
		public IActionResult Add()
		{
			AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

			return View(addMenuViewModel);

		}
	}
}

