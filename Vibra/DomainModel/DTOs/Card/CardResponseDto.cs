namespace Vibra.DTOs.Card
{
    public class CardResponseDto
    {
        public Guid Id { get; set; }
        public string CardholderName { get; set; }
        public string TokenizedNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Brand { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
