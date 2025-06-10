namespace SemanticKernel.Services
{
    using Microsoft.EntityFrameworkCore;
    using SemanticKernel.Models;
    using System;

    public static class CartService
    {
        public static async Task AddItemAsync(string customerName, string pizzaName)
        {
            using var context = new Context();
            context.CartItems.Add(new CartItem
            {
                CustomerName = customerName,
                PizzaName = pizzaName
            });
            await context.SaveChangesAsync();
        }

        public static async Task<bool> RemoveItemAsync(string customerName, string pizzaName)
        {
            using var context = new Context();
            var item = await context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.PizzaName == pizzaName &&
                    ci.CustomerName == customerName);

            if (item != null)
            {
                context.CartItems.Remove(item);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public static async Task<List<string>> GetCartItemsAsync(string customerName)
        {
            using var context = new Context();
            return await context.CartItems
                .Where(ci => ci.CustomerName == customerName)
                .Select(ci => ci.PizzaName)
                .ToListAsync();
        }

        public static async Task ClearCartAsync(string customerName)
        {
            using var context = new Context();
            await context.CartItems
                .Where(ci => ci.CustomerName == customerName)
                .ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }
    }
}
