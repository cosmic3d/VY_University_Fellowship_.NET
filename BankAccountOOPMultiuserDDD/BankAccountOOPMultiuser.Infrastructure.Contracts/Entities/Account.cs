﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts.Entities
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            Movements = new HashSet<Movement>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(11, 2)")]
        public decimal Balance { get; set; }
        [Required]
        [Column("IBAN")]
        [StringLength(24)]
        public string Iban { get; set; }
        [Required]
        [StringLength(4)]
        public string Pin { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Movement> Movements { get; set; }
    }
}