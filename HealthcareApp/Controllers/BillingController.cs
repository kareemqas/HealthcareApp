using HealthcareApp.Data;
using HealthcareApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Controllers
{
    public class BillingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var billings = await _context.Billings.Include(b => b.Patient).ToListAsync();
            return View(billings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Patients = _context.Patients.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Billings.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Patients = _context.Patients.ToList();
            return View(billing);
        }
    }
}
