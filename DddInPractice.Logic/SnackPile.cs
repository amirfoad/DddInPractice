﻿namespace DddInPractice.Logic;

public sealed class SnackPile : ValueObject<SnackPile>
{
    public Snack Snack { get; }
    public int Quantity { get; }
    public decimal Price { get; }


    private SnackPile()
    {
    }

    public SnackPile(Snack snack,
        int quantity,
        decimal price) : this()
    {
        if (Quantity < 0)
            throw new InvalidOperationException();

        if (Price < 0)
            throw new InvalidOperationException();

        if (Price % 0.01m > 0)
            throw new InvalidOperationException();
        
        Snack = snack;
        Quantity = quantity;
        Price = price;
    }

    public SnackPile SubstractOne() =>
        new SnackPile(Snack, Quantity - 1, Price);

    protected override bool EqualsCore(SnackPile other)
    {
        return Snack == other.Snack
               && Quantity == other.Quantity
               && Price == other.Price;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Snack.GetHashCode();
            hashCode = (hashCode * 397) ^ Quantity;
            hashCode = (hashCode * 397) ^ Price.GetHashCode();
            return hashCode;
        }
    }
}