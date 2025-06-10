using Microsoft.SemanticKernel;
using SemanticKernel.Services;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class ShoppingCartPlugin
    {
        [KernelFunction("add_to_cart")]
        [Description("Adds a pizza to the customer's shopping cart")]
        public async Task<string> AddToCartAsync(
            [Description("Customer name")] string customerName,
            [Description("The name of the pizza to add")] string pizzaName)
        {
            await CartService.AddItemAsync(customerName, pizzaName);
            return $"{pizzaName} added to {customerName}'s cart.";
        }

        [KernelFunction("remove_from_cart")]
        [Description("Removes a pizza from the customer's shopping cart")]
        public async Task<string> RemoveFromCartAsync(
            [Description("Customer name")] string customerName,
            [Description("The name of the pizza to remove")] string pizzaName)
        {
            bool removed = await CartService.RemoveItemAsync(customerName, pizzaName);
            return removed ? $"{pizzaName} removed from {customerName}'s cart." : "Item not found in cart.";
        }

        [KernelFunction("view_cart")]
        [Description("Displays the current contents of the customer's shopping cart")]
        public async Task<List<string>> ViewCartAsync(
            [Description("Customer name")] string customerName)
        {
            return await CartService.GetCartItemsAsync(customerName);
        }

        [KernelFunction("clear_cart")]
        [Description("Clears all items from the customer's shopping cart")]
        public async Task<string> ClearCartAsync(
            [Description("Customer name")] string customerName)
        {
            await CartService.ClearCartAsync(customerName);
            return $"{customerName}'s cart cleared.";
        }
    }
}
