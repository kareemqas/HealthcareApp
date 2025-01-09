using HealthcareApp.Data;
using HealthcareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repositories
{
    public class PatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
    }
}
