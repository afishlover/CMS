namespace CoreLayer.Entities
{
    public class Category
    {
        public Category()
        {
            Categories = new List<Category>();
            Courses = new List<Course>();
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Guid ParentId { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Course> Courses { get; set; }
    }
}