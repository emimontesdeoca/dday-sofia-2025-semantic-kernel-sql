using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernel.Models;
using System.ComponentModel;

namespace SemanticKernel.Plugins
{
    public class PizzaMenuPlugin
    {
        [KernelFunction("get_available_pizzas")]
        [Description("Retrieves the list of available pizzas with their details")]
        public async Task<List<Pizza>> GetAvailablePizzasAsync()
        {
            using var context = new Context();
            return await context.Pizzas.ToListAsync();
        }
    }
}
