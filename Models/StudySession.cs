namespace myRestApiApp.Models
{
    public class StudySession
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public DateTime SessionDate { get; set; }
        public double SessionTime { get; set; }
        public bool SessionType { get; set; }
        public double Progress { get; set; }
    }
}
