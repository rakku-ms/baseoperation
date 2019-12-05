using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BaseOperations.Controllers
{
    public class PerimeterSecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Retraining()
        {
            return View();
        }

        public IActionResult ApplyModel()
        {
            return View();
        }
    }
}