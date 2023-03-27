using System.Security.AccessControl;

namespace Client.Models.DTOs
{
    public class CreateResourceDTO
    {
		public Guid CourseId { get; set; }
		public string Content { get; set; }
		public DateTime? OpenTime { get; set; }
		public DateTime? CloseTime { get; set; }
	}
}
