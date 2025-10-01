using Dima.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required (ErrorMessage = "O Titulo é Inválido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Tipo é Inválido")]
        public ETransactionType Type { get; set; }

        [Required(ErrorMessage = "O Valor é Inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "A Categoria é Inválida")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A Data é Inválida")]
        public DateTime PaidOrReceivedAt { get; set; }
    }
}
