using BankAccountOOPMultiuser.XCutting.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
    /// <summary>
    /// This class is used to transfer the full account information to the client.
    /// </summary>
    public class NewAccountDto
    {
        /// <summary>
        /// The IBAN of the account.
        /// </summary>
        /// <example>IBAN2536</example>
        [Required]
        [StringLength(24, MinimumLength = 8)]
        public string Iban { get; set; }

        /// <summary>
        /// The PIN of the account.
        /// </summary>
        /// <example>1234</example>
        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }
    }
}
