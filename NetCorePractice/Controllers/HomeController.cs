using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCorePractice.Models;
using NetCorePractice.Services;

namespace NetCorePractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ISample _transient;
        private readonly ISample _scoped;
        private readonly ISample _singleton;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(  ISampleTransient transient,
                                            ISampleScoped scoped,
                                            ISampleSingleton singleton)
        {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
        }


        public IActionResult Index()
        {   
            ViewBag.SingletonId = _singleton.Id;
            ViewBag.SingletonHashCode = _singleton.GetHashCode();
            
            ViewBag.ScopedId = _scoped.Id;
            ViewBag.ScopedHashCode = _scoped.GetHashCode();

            ViewBag.TransientId = _transient.Id;
            ViewBag.TransientHashCode = _transient.GetHashCode();

   
         
            return View();
        }





      



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
