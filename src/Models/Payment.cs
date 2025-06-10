namespace SemanticKernel.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
