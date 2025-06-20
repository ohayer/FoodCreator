using Food_Creator.Model.dto;

namespace Food_Creator.BusinessLogic;

public enum PromoType
{
    Percentage,
    Fixed,
    BuyXGetY
}

public record Promotion(PromoType Type, float Value, int X = 0, int Y = 0);

public static class PromotionEngine
{
    /// <summary>
    /// Zwraca nową cenę po zastosowaniu promocji.
    /// Przy BuyXGetY zwraca 0, jeśli Y ≥ X (całkowicie darmowe).
    /// Rzuca InvalidOperationException, gdy nieobsługiwany typ promo.
    /// </summary>
    public static float ApplyPromotion(DishDto dish, Promotion promo)
    {
        return promo.Type switch
        {
            PromoType.Percentage => dish.Price * (1f - promo.Value / 100f),
            PromoType.Fixed => MathF.Max(0, dish.Price - promo.Value),
            PromoType.BuyXGetY =>
                promo.Y >= promo.X ? 0 : dish.Price * ((float)(promo.X - promo.Y) / promo.X),
            _ => throw new InvalidOperationException("Unknown promo type")
        };
    }
}