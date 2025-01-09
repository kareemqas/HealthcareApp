using HealthcareApp.Data;
using HealthcareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApp.Repositories
{
    public class DoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }
    }
}
