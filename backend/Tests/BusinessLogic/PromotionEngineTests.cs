using System;
using Food_Creator.BusinessLogic;
using Food_Creator.Model.dto;
using Xunit;

public class PromotionEngineTests
{
    private readonly DishDto _dish = new() { Name = "Test", Price = 100 };

    /// <summary>
    /// 10 % rabatu obniża cenę do 90.
    /// </summary>
    [Fact]
    public void ApplyPromo_Percentage_Returns90()
    {
        var promo = new Promotion(PromoType.Percentage, 10);
        var price = PromotionEngine.ApplyPromotion(_dish, promo);
        Assert.Equal(90, price);
    }

    /// <summary>
    /// Rabat stały 5 zł obniża 100 → 95.
    /// </summary>
    [Fact]
    public void ApplyPromo_Fixed_Returns95()
    {
        var promo = new Promotion(PromoType.Fixed, 5);
        Assert.Equal(95, PromotionEngine.ApplyPromotion(_dish, promo));
    }

    /// <summary>
    /// 3 + 1 gratis (Y=1) obniża cenę o 1/3 → 66.66.
    /// </summary>
    [Fact]
    public void ApplyPromo_BuyXGetY_ReturnsDiscounted()
    {
        var promo   = new Promotion(PromoType.BuyXGetY, 0, X: 3, Y: 1);
        var price   = PromotionEngine.ApplyPromotion(_dish, promo);
        const float expected = 100f * (2f / 3f);           // 66,666…

        Assert.Equal(expected, price, 2);  
    }
    /// <summary>
    /// Udawana promocja BuyXGetY, gdzie Y >= X, daje darmowe danie.
    /// </summary>
    [Fact]
    public void FakePromo_BuyXGetY_ReturnsZero()
    {
        // Gdy Y >= X, promocja daje darmowe danie.
        var promo = new Promotion(PromoType.BuyXGetY, 0, X: 2, Y: 2);
        Assert.Equal(0, PromotionEngine.ApplyPromotion(_dish, promo));
    }

    /// <summary>
    /// Nieznany typ promocji → InvalidOperationException.
    /// </summary>
    [Fact]
    public void ApplyPromo_UnknownType_Throws()
    {
        const PromoType bad = (PromoType)123;
        Assert.Throws<InvalidOperationException>(() =>
            PromotionEngine.ApplyPromotion(_dish, new Promotion(bad, 0)));
    }
}