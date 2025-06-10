using Microsoft.SemanticKernel;
using SemanticKernel.Services;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class PaymentStatusPlugin
    {
        [KernelFunction("check_payment_status")]
        [Description("Checks the status of a payment using customer name and payment ID")]
        public async Task<string> CheckPaymentStatusAsync(
            [Description("Customer name associated with the payment")] string customerName,
            [Description("Payment ID received after payment")] Guid paymentId)
        {
            var payment = await PaymentService.GetPaymentAsync(customerName, paymentId);

            return payment == null
                ? "No payment found with the provided details."
                : $"Payment ID: {payment.PaymentId}\n" +
                  $"Amount: {payment.Amount:C}\n" +
                  $"Method: {payment.PaymentMethod}\n" +
                  $"Date: {payment.PaymentDate:yyyy-MM-dd HH:mm}";
        }
    }
}
