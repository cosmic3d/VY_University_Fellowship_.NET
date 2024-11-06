using BankAccountOOPMultiuser.XCutting.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BankAccountOOPMultiuser.Business.Contracts.DTOs
{
    /// <summary>
    /// This class is used to transfer the full account information to the client.
    /// </summary>
    public class FullAccountDto
    {
        [IgnoreDataMember]
        public bool HasErrors;
        [IgnoreDataMember]
        public AccountErrorEnum AccountError;
        /// <summary>
        /// Account's internal identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The account balance.
        /// </summary>
        [Range(0, 999999999.99)]
        public decimal Balance { get; set; }

        /// <summary>
        /// The IBAN of the account.
        /// </summary>
        [StringLength(24, MinimumLength = 8)]
        public string Iban { get; set; }

        /// <summary>
        /// The PIN of the account.
        /// </summary>
        [StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }

        /// <summary>
        /// The list of account movements.
        /// </summary>
        public List<Tuple<DateTime, Decimal>> Movements { get; set; }
    }
}
