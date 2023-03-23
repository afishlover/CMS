namespace CoreLayer.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Guid ParentId { get; set; }
    }
}