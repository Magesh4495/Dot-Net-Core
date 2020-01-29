using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entity.Entities;
using Entity.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataBaseServiceLayer DbServiceLayer;

        public HomeController(ILogger<HomeController> logger,IDataBaseServiceLayer DbServiceLayer)
        {
            _logger = logger;
            this.DbServiceLayer = DbServiceLayer;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Category()
        {
            List<Category> CategoriesList = new List<Category>();
            CategoriesList = DbServiceLayer.GetCategories();
            return View(CategoriesList);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
