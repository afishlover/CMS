using CoreLayer.Entities;
using CoreLayer.Enums;

namespace Api.Models.DTOs
{
    public class ResourceDTO
    {
        public string ResourceType { get; set; }
        public Guid CourseId { get; set; }
        public string FileURL { get; set; }
        public string Content { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Status { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime Since { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
