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
			/*why is syntax different than IList<Cheese> cheeses = context.Cheeses.Include(context => context.Category).ToList(); ? */
			IList<Menu> menus = context.Menus.ToList();

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

		[HttpPost]
		public IActionResult Add(AddMenuViewModel addMenuViewModel)
		{
			if (ModelState.IsValid)
			{

				Menu newMenu = new Menu
				{
					Name = addMenuViewModel.Name
				};

				context.Menus.Add(newMenu);
				context.SaveChanges();

				return Redirect("/Menu/ViewMenu" + newMenu.ID);

			}

			return View(addMenuViewModel);

		}

		// default routing for MVC applications is /{Controller}/{Action}/{id
		//  /Menu/ViewMenu/5
		[HttpGet]
		public IActionResult ViewMenu(int id)
		{
			Menu menu = context.Menus.Single(m => m.ID == id);

			List<CheeseMenu> items = context
				.CheeseMenus
				.Include(item => item.Cheese)
				.Where(cm => cm.MenuID == id)
				.ToList();

			ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
			{
				Menu = menu,
				Items = items
			};
			
			return View(viewMenuViewModel);

		}
	}
}

