namespace Vibra.DTOs.Transaction
{
    public class TransactionCreateDto
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
    }
}
