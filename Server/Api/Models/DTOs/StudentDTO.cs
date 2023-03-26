using CoreLayer.Entities;
using CoreLayer.Enums;

namespace Api.Models.DTOs
{
    public class StudentDTO
    {
        public string StudentCode { get; set; }
        public string Major { get; set; }
        public string FullName { get; set; }
        public string? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }

    }
}

