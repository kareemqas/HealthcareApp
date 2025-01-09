namespace HealthcareApp.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
