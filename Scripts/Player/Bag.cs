using System;

public class Bag
{
    public int FirstAidKit { get; private set; }

    public void AddFirstAidKit(int numberOfFirstAidKit)
    {
        if (numberOfFirstAidKit < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfFirstAidKit));

        FirstAidKit += numberOfFirstAidKit;
    }
}