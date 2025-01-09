namespace HealthcareApp.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
