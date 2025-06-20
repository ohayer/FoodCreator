using System;
using System.Collections.Generic;
using Food_Creator.BusinessLogic;
using Food_Creator.Model;
using Food_Creator.Model.dto;
using Xunit;

/// <summary>
/// Happy-path: poprawne wyliczenie ceny.
/// </summary>
public class PriceCalculatorTests
{
    [Fact]
    public void ComputeDishCost_Valid_ReturnsTotalAndLines()
    {
        var ing = new[]
        {
            new Ingredient { IngredientId = 1, Name = "Cheese", Price = 1.5f },
            new Ingredient { IngredientId = 2, Name = "Tomato", Price = 0.5f }
        };
        var dto = new DishDto
        {
            DishIngredients = new List<DishIngredientDto>
            {
                new() { IngredientId = 1, Quantity = 2 },
                new() { IngredientId = 2, Quantity = 3 }
            }
        };

        var result = PriceCalculator.ComputeDishCost(dto, ing);

        Assert.Equal((decimal)(1.5f * 2 + 0.5f * 3), result.TotalPrice);
        Assert.Equal(2, result.Lines.Count);
    }

    /// <summary>
    /// Nieistniejący składnik → ArgumentException.
    /// </summary>
    [Fact]
    public void ComputeDishCost_MissingIngredient_Throws()
    {
        var dto = new DishDto
        {
            DishIngredients = new List<DishIngredientDto>
            {
                new() { IngredientId = 999, Quantity = 1 }
            }
        };

        Assert.Throws<ArgumentException>(() =>
            PriceCalculator.ComputeDishCost(dto, new List<Ingredient>()));
    }

    /// <summary>
    /// Quantity = 0 → ArgumentException.
    /// </summary>
    [Fact]
    public void ComputeDishCost_ZeroQuantity_Throws()
    {
        var dto = new DishDto
        {
            DishIngredients = new List<DishIngredientDto>
            {
                new() { IngredientId = 1, Quantity = 0 }
            }
        };
        var ing = new[] { new Ingredient { IngredientId = 1, Name = "X", Price = 1 } };

        Assert.Throws<ArgumentException>(() =>
            PriceCalculator.ComputeDishCost(dto, ing));
    }
}