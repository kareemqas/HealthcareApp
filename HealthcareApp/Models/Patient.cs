namespace HealthcareApp.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MedicalHistory { get; set; }
        public string DocumentPath { get; set; } // Path to uploaded document

        // Navigation properties
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Billing> Billings { get; set; } = new List<Billing>(); // Added for Billing relationship
        public List<Notification> Notifications { get; set; } = new List<Notification>(); // Added for Notification relationship
    }
}
