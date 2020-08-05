using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyWeeLinks.Api.Data
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Shortcut { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string Secret { get; set; }
        public virtual ICollection<Click> Clicks { get; set; }
    }
}
