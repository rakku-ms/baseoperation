using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseOperations.Data;
using Microsoft.AspNetCore.Mvc;

namespace BaseOperations.Controllers
{
    public class SupplyManagementController : Controller
    {
        private readonly BaseContext _context;

        public SupplyManagementController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Shipments.ToList());
        }
        public IActionResult AddShipment()
        {
            return View();
        }

        public IActionResult ViewShipments()
        {
            return PartialView("ViewShipments", _context.Shipments.ToList());
        }
    }
}