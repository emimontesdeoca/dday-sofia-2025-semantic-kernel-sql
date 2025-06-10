using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernel.Services;
using System;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class PaymentPlugin
    {
        [KernelFunction("process_payment")]
        [Description("Processes the payment for the customer's current order. Returns payment ID for reference.")]
        public async Task<string> ProcessPaymentAsync(
            [Description("Customer name for the order")] string name,
            [Description("Payment method (e.g., credit, paypal)")] string paymentMethod)
        {
            var validMethods = new[] { "credit", "paypal", "debit" };
            if (!validMethods.Contains(paymentMethod.ToLower()))
                return $"Error: Payment method '{paymentMethod}' is not supported.";

            decimal total = await CalculateTotalAsync(name);
            if (total == 0) return "Error: Cart is empty.";

            var payment = await PaymentService.AddPaymentAsync(name, paymentMethod, total);
            await CartService.ClearCartAsync(name);

            return $"Payment of {total:C} via {paymentMethod} processed successfully! " +
                   $"Your payment ID: {payment.PaymentId}";
        }

        private async Task<decimal> CalculateTotalAsync(string customerName)
        {
            using var context = new Context();
            var cartItems = await CartService.GetCartItemsAsync(customerName);
            var pizzas = await context.Pizzas
                .Where(p => cartItems.Contains(p.Name))
                .ToListAsync();
            return pizzas.Sum(p => p.Price);
        }
    }
}
