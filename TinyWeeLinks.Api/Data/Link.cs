using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TinyWeeLinks.Api.Data
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Shortcut { get; set; }
        public string Url { get; set; }
        [Required]
        public DateTime DateTimeCreated { get; set; }
        public string Secret { get; set; }
        public ICollection<Click> Clicks { get; set; }
    }
}
