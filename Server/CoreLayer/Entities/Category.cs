namespace CoreLayer.Entities {
    public class Category {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }
    }
}