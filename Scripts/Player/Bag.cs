using System;

public class Bag
{
    public int Coin { get; private set; }

    public void AddCoin(int numberCoin)
    {
        if (numberCoin < 0)
            throw new ArgumentOutOfRangeException(nameof(numberCoin));

        Coin += numberCoin;
    }
}