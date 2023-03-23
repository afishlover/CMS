namespace CoreLayer.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Since { get; set; }
        public DateTime LastUpdate { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string CreatorCode { get; set; }
    }
}