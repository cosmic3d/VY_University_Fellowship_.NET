﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PokeApiDDD.Infrastructure.Contracts.Entities
{
    public partial class PokeStatisticsByInitial
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Initial { get; set; }
        public int Counter { get; set; }
    }
}