namespace Vibra.DTOs.Card
{
    public class CardCreateDto
    {
        public string CardholderName { get; set; }
        public string TokenizedNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Brand { get; set; }
        public Guid UserId { get; set; }
    }
}
