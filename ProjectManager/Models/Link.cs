﻿using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    /// <summary>
    /// The link between task
    /// </summary>
    public class Link
    {
        public int Id { get; set; }
        [MaxLength(1)]
        public string Type { get; set; }
        public int SourceTaskId { get; set; }
        public int TargetTaskId { get; set; }
    }
}