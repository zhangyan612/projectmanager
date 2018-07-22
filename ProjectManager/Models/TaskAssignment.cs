using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    public class TaskAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string UserProfileId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public double PlannedHours { get; set; }

        public double ActualHours { get; set; }
    }
}