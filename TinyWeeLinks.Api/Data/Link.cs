using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TinyWeeLinks.Api.Data
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Shortcut { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public DateTime DateTimeCreated { get; set; }
        public string Secret { get; set; }
        public ICollection<Click> Clicks { get; set; }
    }
}
