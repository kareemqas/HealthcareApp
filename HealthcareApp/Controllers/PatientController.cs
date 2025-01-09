using HealthcareApp.Data;
using HealthcareApp.Models;
using HealthcareApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public PatientController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Patient patient, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                if (document != null && document.Length > 0)
                {
                    patient.DocumentPath = await _blobService.UploadFileAsync(document);
                }

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(patient);
        }

        public async Task<IActionResult> Profile(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var patients = from p in _context.Patients select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.Name.Contains(searchString) || p.Email.Contains(searchString));
            }

            return View(await patients.ToListAsync());
        }
    }
}
