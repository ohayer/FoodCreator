using System.Collections.Generic;
using System.Linq;
using Food_Creator.Model;
using Food_Creator.Model.dto;

namespace Food_Creator.BusinessLogic;

public record PriceBreakdown(decimal TotalPrice, Dictionary<string, decimal> Lines);

public static class PriceCalculator
{
    /// <summary>
    /// Zwraca łączny koszt dania i szczegółowe koszty składników.
    /// Rzuca ArgumentException, jeżeli któregokolwiek składnika brakuje w bazie
    /// albo Quantity ≤ 0.
    /// </summary>
    public static PriceBreakdown ComputeDishCost(DishDto dishDto, IEnumerable<Ingredient> allIngredients)
    {
        if (dishDto.DishIngredients is null || !dishDto.DishIngredients.Any())
            throw new ArgumentException("Dish has no ingredients");

        var lines = new Dictionary<string, decimal>();

        foreach (var di in dishDto.DishIngredients)
        {
            if (di.Quantity <= 0)
                throw new ArgumentException($"Quantity for ingredient {di.IngredientId} must be > 0");

            var ing = allIngredients.FirstOrDefault(i => i.IngredientId == di.IngredientId)
                      ?? throw new ArgumentException($"Ingredient {di.IngredientId} does not exist");

            var linePrice = ing.Price * di.Quantity;
            lines.Add(ing.Name, (decimal)linePrice);
        }

        var total = lines.Values.Sum();
        return new PriceBreakdown(total, lines);
    }
}