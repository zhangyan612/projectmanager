using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    /// <summary>
    /// Each task in a project
    /// </summary>
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public int SortOrder { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TaskAssignment> AssignedTo { get; set; }

        [JsonIgnore]
        public virtual ProjectBoard Board { get; set; }
        public int? BoardId { get; set; }

        public Guid ProjectId { get; set; }
        public int? DescriptionId { get; set; }

        //public virtual Project Project { get; set; }
    }
}