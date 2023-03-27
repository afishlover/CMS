using System.Security.AccessControl;

namespace Client.Models.DTOs
{
    public class CreateResourceDTO
    {
		public Guid CourseId { get; set; }
		public string Content { get; set; }
		public string OpenTime { get; set; }
		public string CloseTime { get; set; }
		public string FileURL { get; set; }
	}
}
