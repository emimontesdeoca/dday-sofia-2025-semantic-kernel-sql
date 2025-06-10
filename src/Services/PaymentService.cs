using Microsoft.EntityFrameworkCore;
using SemanticKernel.Models;

namespace SemanticKernel.Services
{
    public static class PaymentService
    {
        public static async Task<Payment> AddPaymentAsync(string customerName, string paymentMethod, decimal amount)
        {
            using var context = new Context();
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                CustomerName = customerName,
                PaymentMethod = paymentMethod,
                Amount = amount
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync();
            return payment;
        }

        public static async Task<Payment?> GetPaymentAsync(string customerName, Guid paymentId)
        {
            using var context = new Context();
            return await context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId && p.CustomerName == customerName);
        }
    }
}
