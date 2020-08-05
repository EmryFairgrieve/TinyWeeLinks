﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyWeeLinks.Api.Data
{
    public class Click
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateTimeClicked { get; set; }
        [Required]
        public int LinkId { get; set; }
        [ForeignKey("LinkId")]
        public virtual Link Link { get; set; }
    }
}