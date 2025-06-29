using System;
using System.ComponentModel.DataAnnotations;

namespace AidimsApi.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string MedicalHistory { get; set; } = string.Empty;

    }
}


